using Domain.Core.Base;
using Domain.Core.Interfaces;
using Domain.Core.Validators;
using Domain.Entities.TurtleChallenge;
using Infrastructure.Crosscutting;
using Infrastructure.Crosscutting.Factory;
using Infrastructure.Crosscutting.Settings;

namespace Domain.Core
{
    public class TurtleGame : AGame, ITurtleGame
    {
        private readonly TurtleChallengeSettings configuration;
        public GameState gameState;
        public Queue<ACommand> commands;

        public TurtleGame(TurtleChallengeSettings configuration)
        {
            this.configuration = configuration;
            this.gameState = LoadGame();
            this.commands = LoadCommands();
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
            IEnumerable<string> gamesettingsTxt = SettingsFileLoader.ReadSettingsFromTxt(
                this.configuration.Configurations[TurtleChallengeSettings.GAME_SETTINGS_FILE_ID]);

            GameState gameState = null;

            return gameState;
        }

        private Queue<ACommand> LoadCommands()
        {
            IEnumerable<string> movesTxt = SettingsFileLoader.ReadSettingsFromTxt(
                this.configuration.Configurations[TurtleChallengeSettings.MOVES_FILE_ID]);

            IEnumerable<ACommand> commandsToRead = movesTxt.Select(CommandFactory.CommandFactoryCreator);

            foreach(ACommand command in commandsToRead) { this.commands.Enqueue(command); }

            return this.commands;
        }

        
    }
}
