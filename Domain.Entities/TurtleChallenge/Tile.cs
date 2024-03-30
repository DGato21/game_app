namespace Domain.Entities.TurtleChallenge
{
    /// <summary>
    /// Represents an empty tile/blank tile
    /// </summary>
    public class Tile : AElement
    {
        public static string[] IDENTIFIER = { "", " ", "-" };

        public Tile(int x, int y) : base(x, y) { }
    }
}
