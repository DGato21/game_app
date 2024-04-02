namespace Infrastructure.Crosscutting.Settings.Base
{
    public abstract class AGameSettings
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string ConfigurationsFolder { get; set; }
        /// <summary>
        /// Values: RELEASE; DEBUG
        /// In Release it will output only the result expected for the specification
        /// In Debug it will show more information
        /// </summary>
        public string Environment { get; set; }
    }
}
