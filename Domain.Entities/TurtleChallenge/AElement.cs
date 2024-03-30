namespace Domain.Entities.TurtleChallenge
{
    public abstract class AElement
    {
        public Position Position { get; set; }
        public AElement(int x, int y) { this.Position = new Position(x, y); }
    }
}
