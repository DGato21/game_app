using Domain.Core.Base;
using Domain.Core.Factory;
using Domain.Core.Interfaces;
using Domain.Core.Validators;
using Domain.Entities.TurtleChallenge;
using Infrastructure.Crosscutting;
using Infrastructure.Crosscutting.Settings;

using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Domain.Core
{
    public class TurtleGame : AGame, ITurtleGame
    {
        private readonly TurtleChallengeSettings configuration;

        public GameState gameState;
        public Queue<Queue<ACommand>> sequences;

        private readonly GameState initialState;

        public TurtleGame(IOptions<TurtleChallengeSettings> configuration) : base()
        {
            this.configuration = configuration.Value;
            this.gameState = LoadGame();
            this.sequences = LoadCommands();

            this.initialState = this.gameState.DeepClone();
        }

        public async override void Start()
        {
            //Validate First
            var valid = ValidateState();

            if (valid)
            {
                Console.WriteLine(GetOutputMessage("Start Game"));

                Console.WriteLine(GetOutputMessage($"Initial State of Game:\n{gameState.ToString()}"));

                if (this.sequences.Any())
                {
                    int seqNr = 0;

                    while (this.sequences.Any())
                    {
                        Console.WriteLine(GetOutputMessage($"------------ Sequence {seqNr} START ------------"));
                        var commands = this.sequences.Dequeue();

                        while (commands.Any())
                        {
                            var command = commands.Dequeue();
                            var commandSuccess = ExecuteCommand(command);

                            if (!commandSuccess)
                                break;

                            if (this.gameState.Turtle.Position.Equals(this.gameState.Exit.Position))
                            {
                                Console.WriteLine(GetOutputMessage($"Success!"));
                                break;
                            }
                        }

                        Console.WriteLine(GetOutputMessage($"------------ Sequence {seqNr} END ------------"));

                        seqNr++;
                        ResetGameState();
                    }


                }
            }

            Console.WriteLine(GetOutputMessage("End of Game"));
        }

        private void ResetGameState() 
        {
            this.gameState = this.initialState.DeepClone();
            Console.WriteLine(GetOutputMessage("Reset Board to start new sequence."));
        }

        public bool ExecuteCommand(ACommand command)
        {
            //Copy of GameState
            GameState newGameState = gameState;
            gameState.CommandToExecute = command;

            //Validate First
            var valid = ValidateState();

            if (valid)
            {
                //Proceed if valid
                gameState.CommandToExecute.ExecuteCommand(newGameState);

                //If everything okay
                this.gameState = newGameState;

                Console.WriteLine(GetOutputMessage(this.gameState.ToString()));
            }

            return valid;
        }

        private GameState LoadGame()
        {
            string gamesettingsTxt = SettingsFileLoader.ReadSettingsFromTxt(
                this.configuration.Configurations[TurtleChallengeSettings.GAME_SETTINGS_FILE_KEY]);

            TurtleGameBoardSettings settings = JsonConvert.DeserializeObject<TurtleGameBoardSettings>(gamesettingsTxt);

            GameState gameState = GameStateFactory.Create(settings);

            return gameState;
        }

        private Queue<Queue<ACommand>> LoadCommands()
        {
            string movesTxt = SettingsFileLoader.ReadSettingsFromTxt(
                this.configuration.Configurations[TurtleChallengeSettings.MOVES_FILE_KEY]);

            MovesSettings settings = JsonConvert.DeserializeObject<MovesSettings>(movesTxt);

            if (this.sequences == null) { this.sequences = new Queue<Queue<ACommand>>(); }

            foreach (var sequence in settings.moves)
            {
                Queue<ACommand> newSequence = new Queue<ACommand>();
                IEnumerable<ACommand> sequenceCommand = sequence.Split(" ").Select(CommandFactory.CommandFactoryCreator);
                foreach (ACommand command in sequenceCommand) { newSequence.Enqueue(command); }
                this.sequences.Enqueue(newSequence);
            }

            return this.sequences;
        }

        private string GetOutputMessage(string action) => $"{this.GetType()} [{this.GetInstanceId()}]: \n -> {action}\n";
    
        private bool ValidateState()
        {
            bool validState = false;

            //Validator only for basic rules
            TurtleGameValidator validator = new TurtleGameValidator();
            var result = validator.Validate(this.gameState);
            if (!result.IsValid)
            {
                Console.Write(GetOutputMessage($"Invalid State in initialization or after command:\n{string.Join(";", result.Errors.Select(x => x.ErrorMessage))}"));
                
                if (this.gameState.CommandToExecute != null)
                {
                    Console.Write($"Invalid Command is: {this.gameState.CommandToExecute.ToString()}");
                }
            }
            else
            {
                if (this.gameState.ListMine.Where(x=> x.Position.Equals(this.gameState.Turtle.Position)).Any())
                {
                    Console.Write(GetOutputMessage($"Mine hit!"));
                }
                else
                {
                    validState = true;
                }
            }
            return validState;
        }
    }
}
