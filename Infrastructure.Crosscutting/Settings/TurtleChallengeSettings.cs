using Infrastructure.Crosscutting.Settings.Interfaces;

namespace Infrastructure.Crosscutting.Settings
{
    public class TurtleChallengeSettings : AGameSettings
    {
        public const string GAME_SETTINGS_FILE_ID = "game-settings.json";
        public const string MOVES_FILE_ID = "moves.json";

        public IDictionary<string, string> Configurations { get; set; }
    }
}
