using Domain.Entities.TurtleChallenge.Enumerator;

namespace Domain.Entities.TurtleChallenge
{
    public class CommandRotate : ACommand
    {
        public static string[] IDENTIFIER = { "ROTATE", "R" };

        //Direction State Order (ROTATE -> Rotate 90º to the Right)
        private static readonly List<Direction> ROTATIONS_ORDER =
            new List<Direction>() { Direction.North, Direction.West, Direction.South, Direction.East };

        public override void ExecuteCommand(GameState gameState)
        {
            var turtle = gameState.Turtle;

            int currentpositionIndex = ROTATIONS_ORDER.IndexOf(turtle.Direction);
            int nextIndex = currentpositionIndex + 1;
            if (nextIndex + 1 >= ROTATIONS_ORDER.Count) { nextIndex = 0; }

            turtle.Direction = ROTATIONS_ORDER[nextIndex];
        }
    }
}
