using Domain.Entities.TurtleChallenge;

namespace Infrastructure.Crosscutting.Factory
{
    public class ElementFactory
    {
        public static AElement ElementFactoryCreator(string element, int positionX, int positionY, string? direction = null)
        {
            if (Exit.IDENTIFIER.Contains(element))
                return new Exit(positionX, positionY);
            else if (Mine.IDENTIFIER.Contains(element))
                return new Mine(positionX, positionY);
            else if (Tile.IDENTIFIER.Contains(element))
                return new Tile(positionX, positionY);
            else if (Turtle.IDENTIFIER.Contains(element))
                return new Turtle(positionX, positionY, direction);
            else
                throw new ArgumentException($"Invalid element: {element}");
        }
    }
}
