namespace Infrastructure.Crosscutting.Settings.Base
{
    public class ApplicationSettings
    {
        public IEnumerable<string> GameList { get; set; }
        public TurtleChallengeSettings TurtleChallengeSettings { get; set; }
    }
}
