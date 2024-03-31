namespace Infrastructure.Crosscutting
{
    public static class SettingsFileLoader
    {
        public static string ReadSettingsFromTxt(string path)
        {
            if (!Path.Exists(path))
            {
                throw new Exception($"Invalid path: {path}");
            }

            string fileTxt = File.ReadAllText(path);

            return fileTxt;
        }
    }
}
