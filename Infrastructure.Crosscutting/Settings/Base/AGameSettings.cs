namespace Infrastructure.Crosscutting.Settings.Base
{
    public abstract class AGameSettings
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string ConfigurationsFolder { get; set; }
    }
}
