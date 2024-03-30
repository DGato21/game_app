namespace Infrastructure.Crosscutting
{
    public static class SettingsFileLoader
    {
        public static IEnumerable<string> ReadSettingsFromTxt(string path)
        {
            if (!Path.Exists(path))
            {
                throw new Exception($"Invalid path: {path}");
            }

            IEnumerable<string> fileTxt = File.ReadLines(path);

            return fileTxt;
        }
    }
}
