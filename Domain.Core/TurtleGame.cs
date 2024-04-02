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
                WriteOutputMessage("Start Game");

                WriteOutputMessage($"Initial State of Game:\n{gameState.ToString()}");

                if (this.sequences.Any())
                {
                    int seqNr = 0;

                    while (this.sequences.Any())
                    {
                        WriteOutputMessage($"------------ Sequence {seqNr} START ------------");
                        WriteOutputMessage($"Sequence {seqNr}: ", "RELEASE", false);
                        var commands = this.sequences.Dequeue();

                        int totalCommands = commands.Count;
                        int commandNr = 0;

                        while (commands.Any())
                        {
                            var command = commands.Dequeue();
                            var commandSuccess = ExecuteCommand(command);

                            if (!commandSuccess)
                            {
                                this.gameState.GameFinish = true;
                                break;
                            }
                            else if (this.gameState.Turtle.Position.Equals(this.gameState.Exit.Position))
                            {
                                this.gameState.GameFinish = true;
                                WriteOutputMessage($"Success!", "RELEASE");
                                break;
                            }
                            else if (commandNr+1 >= totalCommands) //Means last command
                            {
                                this.gameState.GameFinish = true;
                                WriteOutputMessage($"Still in danger!", "RELEASE");
                            }
                            commandNr = commandNr + 1;
                        }

                        WriteOutputMessage($"------------ Sequence {seqNr} END ------------");

                        seqNr++;
                        ResetGameState();
                    }


                }
            }

            WriteOutputMessage("End of Game");
        }

        private void ResetGameState() 
        {
            this.gameState = this.initialState.DeepClone();
            WriteOutputMessage("Reset Board to start new sequence.");
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

                WriteOutputMessage(this.gameState.ToString());
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
    
        private bool ValidateState()
        {
            bool validState = false;

            //Validator only for basic rules
            TurtleGameValidator validator = new TurtleGameValidator();
            var result = validator.Validate(this.gameState);
            if (!result.IsValid)
            {
                WriteOutputMessage($"Invalid State in initialization or after command:{string.Join(";\n", result.Errors.Select(x => x.ErrorMessage))}", "RELEASE");
                
                if (this.gameState.CommandToExecute != null)
                {
                    WriteOutputMessage($"Invalid Command is: {this.gameState.CommandToExecute.ToString()}");
                }
            }
            else
            {
                if (this.gameState.ListMine.Where(x=> x.Position.Equals(this.gameState.Turtle.Position)).Any())
                {
                    WriteOutputMessage($"Mine hit!", "RELEASE");
                }
                else
                {
                    validState = true;
                }
            }
            return validState;
        }

        public override void WriteOutputMessage(string action, string messageEnv = "DEBUG", bool breakLine = true)
        {
            if (this.configuration.Environment.Equals("DEBUG", StringComparison.InvariantCultureIgnoreCase) 
                || this.configuration.Environment.Equals(messageEnv, StringComparison.InvariantCultureIgnoreCase))
            {
                //string output = $"{this.GetType()} [{this.GetInstanceId()}]: \n -> {action}\n";
                string output = $"{action}";

                if (breakLine) 
                    Console.WriteLine(output);
                else
                    Console.Write(output);
            }
        }
    }
}
