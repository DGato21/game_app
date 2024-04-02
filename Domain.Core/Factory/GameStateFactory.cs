using Domain.Entities.TurtleChallenge;
using Domain.Entities.TurtleChallenge.Enumerator;
using Infrastructure.Crosscutting.Settings;

namespace Domain.Core.Factory
{
    public static class GameStateFactory
    {
        public static GameState Create(TurtleGameBoardSettings settings)
        {
            GameState? gameState = null;
            var size = getPosition(settings.boardSize);

            gameState = new GameState()
            {
                XMax = size.X,
                YMax = size.Y,
                Turtle = new Turtle(getPosition(settings.turtle).X, getPosition(settings.turtle).Y, settings.turtleDirection),
                Exit = new Exit(getPosition(settings.exit).X, getPosition(settings.exit).Y),
                ListMine = settings.mines.Select(x => new Mine(getPosition(x).X, getPosition(x).Y)),
                CommandToExecute = null,
                GameFinish = false,

                SPEC_CONSTRAINT_MAX_SIZE = settings.maxSize.GetValueOrDefault()
            };

            return gameState;
        }

        private static Position getPosition(string positionStr)
        {
            List<int> tmp = positionStr.Split("x").Select(x=> int.Parse(x)).ToList();
            return new Position(tmp[0], tmp[1]);
        }
    }
}
