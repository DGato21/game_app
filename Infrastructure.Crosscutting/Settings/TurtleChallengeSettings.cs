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

    public class TurtleGameBoardSettings
    {
        public string boardSize { get; set; }
        public string turtle { get; set; }
        public string exit { get; set; }
        public string[] mines { get; set; }
        public string turtleDirection { get; set; }
    }

    public class MovesSettings
    {
        public string moves { get; set; }
    }
}
