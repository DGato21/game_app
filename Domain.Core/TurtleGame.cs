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
        public Queue<ACommand> commands;

        public TurtleGame(IOptions<TurtleChallengeSettings> configuration)
        {
            this.configuration = configuration.Value;
            this.gameState = LoadGame();
            this.commands = LoadCommands();
        }

        public async override void Start()
        {
            if (this.commands.Any())
            {
                while (this.commands.Any()) { ExecuteCommand(); }
            }

            Console.WriteLine("Game End");
        }

        public void ExecuteCommand()
        {
            //Copy of GameState
            GameState newGameState = gameState;
            gameState.CommandToExecute = this.commands.Dequeue();

            //Validate First
            TurtleGameValidator validator = new TurtleGameValidator();
            var result = validator.Validate(gameState);

            //Proceed if valid
            gameState.CommandToExecute.ExecuteCommand(newGameState);

            //If everything okay
            this.gameState = newGameState;
        }

        private GameState LoadGame()
        {
            string gamesettingsTxt = SettingsFileLoader.ReadSettingsFromTxt(
                this.configuration.Configurations[TurtleChallengeSettings.GAME_SETTINGS_FILE_KEY]);

            TurtleGameBoardSettings settings = JsonConvert.DeserializeObject<TurtleGameBoardSettings>(gamesettingsTxt);

            GameState gameState = GameStateFactory.Create(settings);

            return gameState;
        }

        private Queue<ACommand> LoadCommands()
        {
            string movesTxt = SettingsFileLoader.ReadSettingsFromTxt(
                this.configuration.Configurations[TurtleChallengeSettings.MOVES_FILE_KEY]);

            MovesSettings settings = JsonConvert.DeserializeObject<MovesSettings>(movesTxt);

            IEnumerable<ACommand> commandsToRead = settings.moves.Split(" ").Select(CommandFactory.CommandFactoryCreator);

            if (this.commands == null) { this.commands = new Queue<ACommand>(); }

            foreach(ACommand command in commandsToRead) { this.commands.Enqueue(command); }

            return this.commands;
        }
    }
}
