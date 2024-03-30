namespace Domain.Entities.TurtleChallenge
{
    public abstract class ACommand
    {
        public abstract void ExecuteCommand (GameState gameState);
    }
}
