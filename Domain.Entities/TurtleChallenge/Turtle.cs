using Domain.Entities.TurtleChallenge.Enumerator;

namespace Domain.Entities.TurtleChallenge
{
    public class Turtle : AElement
    {
        public static string[] IDENTIFIER = { "TURTLE", "T" };
        public Direction Direction { get; set; }

        public Turtle(int x, int y, string direction) : base(x, y)
        {
            var validDirection = Enum.TryParse<Direction>(direction, true, out Direction readDirection);

            if (!validDirection)
                throw new ArgumentException($"Invalid Direction provided: {direction}");

           this.Direction = readDirection;
        }

    }
}
