namespace Domain.Entities.TurtleChallenge
{
    /// <summary>
    /// GameState class that can store the current state of an execution.
    /// Not used for now but created with the intent of saving gaming history (even for debugging purprose)
    /// </summary>
    public class GameState
    {
        public required AElement[][] GameBoard { get; set; }
        public required Turtle Turtle { get; set; }
        public required Exit Exit { get; set; }
        public required IEnumerable<Mine> ListMine { get; set; }

        /// <summary>
        /// Next command to be execute on this state
        /// </summary>
        public ACommand? CommandToExecute { get; set; } = null;
    }
}
