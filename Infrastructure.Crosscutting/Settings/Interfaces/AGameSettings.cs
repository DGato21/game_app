namespace Infrastructure.Crosscutting.Settings.Interfaces
{
    public abstract class AGameSettings
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Version { get; set; }
        public required string ConfigurationsFolder { get; set; }
    }
}
