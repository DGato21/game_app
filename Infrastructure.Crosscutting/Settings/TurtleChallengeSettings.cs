using Infrastructure.Crosscutting.Settings.Base;

namespace Infrastructure.Crosscutting.Settings
{
    public class TurtleChallengeSettings : AGameSettings
    {
        public const string APP_SETTING_KEY = "TurtleChallengeSettings";

        public const string GAME_SETTINGS_FILE_KEY = "GameSettings";
        public const string MOVES_FILE_KEY = "CommandSettings";

        public IDictionary<string, string> Configurations { get; set; }
    }

    /// <summary>
    /// Settings definition to define the TurtleChallenge Game Board
    /// defined in a '.json' file with the specifications defined below
    /// Default: settings/turtleChallenge/game-settings.json
    /// </summary>
    public class TurtleGameBoardSettings
    {
        /// <summary>
        /// Board size
        /// The remaining elements should be defined to be inside the board (meaning min: 0x0; max: BoardSize)
        /// The input will follow the same pattern as the other elements: "XxY"
        /// </summary>
        public string boardSize { get; set; }

        /// <summary>
        /// Turtle element defined for the board.
        /// The input will follow the same pattern as the other elements: "XxY"
        /// </summary>
        public string turtle { get; set; }

        /// <summary>
        /// Exit element defined for the board.
        /// The input will follow the same pattern as the other elements: "XxY"
        /// </summary>
        public string exit { get; set; }

        /// <summary>
        /// Set of Mines defined for the board.
        /// The input will follow the same pattern as the other elements: "XxY"
        /// </summary>
        public string[] mines { get; set; }

        /// <summary>
        /// Turtle direction. It can be NORTH, SOUTH, EAST, WEST (or N,S,E,W)
        /// </summary>
        public string turtleDirection { get; set; }

        /// <summary>
        /// Board Maximum Size of X or Y
        /// </summary>
        public int? maxSize { get; set; }
    }

    /// <summary>
    /// Settings definition to define the TurtleChallenge Sequence of Moves to be executed
    /// defined in a '.json' file with the specifications defined below
    /// Default: settings/turtleChallenge/moves.json
    /// </summary>
    public class MovesSettings
    {
        /// <summary>
        /// Sequence of moves defined to be played
        /// Each Move is defined with a string separated by a whitespace (e.g. MOVE ROTATE ...)
        /// The Move can be the following: MOVE (or M); ROTATE (or R)
        /// </summary>
        public IEnumerable<string> moves { get; set; }
    }
}
