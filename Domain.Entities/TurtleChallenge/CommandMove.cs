namespace Domain.Entities.TurtleChallenge
{
    public class CommandMove : ACommand
    {
        public static string[] IDENTIFIER = { "MOVE", "M" };

        public override void ExecuteCommand(GameState gameState)
        {
            var turtle = gameState.Turtle;

            switch(turtle.Direction)
            {
                case Enumerator.Direction.North:
                    turtle.Position.Y -= 1;
                    break;
                case Enumerator.Direction.South:
                    turtle.Position.Y += 1;
                    break;
                case Enumerator.Direction.East:
                    turtle.Position.X -= 1;
                    break;
                case Enumerator.Direction.West:
                    turtle.Position.X += 1;
                    break;
            }
        }
    }
}
