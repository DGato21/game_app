namespace Domain.Core.Interfaces
{
    public interface IGame
    {
        public abstract void Start();
        public string GetInstanceId();
        public abstract void WriteOutputMessage(string action, string messageEnv = "DEBUG", bool breakLine = true);
    }
}
