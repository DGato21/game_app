using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Domain.Entities.TurtleChallenge
{
    /// <summary>
    /// GameState class that can store the current state of an execution.
    /// Not used for now but created with the intent of saving gaming history (even for debugging purprose)
    /// </summary>
    public class GameState
    {
        public required int XMax { get; set; }
        public required int YMax { get; set; }
        public required Turtle Turtle { get; set; }
        public required Exit Exit { get; set; }
        public required IEnumerable<Mine> ListMine { get; set; }
        public required bool GameFinish { get; set; } = false;
        public int SPEC_CONSTRAINT_MAX_SIZE { get; set; } = 999999999;

        /// <summary>
        /// Next command to be execute on this state
        /// </summary>
        public ACommand? CommandToExecute { get; set; } = null;

        //Easy way to clone
        public GameState DeepClone()
        {
            string copyStr = JsonConvert.SerializeObject(this);

            return JsonConvert.DeserializeObject<GameState>(copyStr);
        }

        public override string ToString()
        {
            string output = string.Empty;

            if (this.CommandToExecute != null)
            {
                output += this.CommandToExecute.ToString();
            }

            output += $"- Turtle: {this.Turtle.Position.X},{this.Turtle.Position.Y} - {this.Turtle.Direction.ToString()}\n";

            output += $"- Exit: {this.Exit.Position.X},{this.Exit.Position.Y}\n";

            output += "- Mines:\n";
            int i = 1;
            foreach (var mine in ListMine) { output += $"--- Mine{i++}: {mine.Position.X},{mine.Position.Y}\n"; }

            return output;
        }
    }
}
