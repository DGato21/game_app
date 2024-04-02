using Domain.Core.Interfaces;

namespace Domain.Core.Base
{
    public abstract class AGame : IGame
    {
        private readonly Guid guid;

        public AGame()
        {
            this.guid = Guid.NewGuid();
        }

        public abstract void Start();
        public abstract void WriteOutputMessage(string action, string messageEnv = "DEBUG", bool breakLine = true);

        public string GetInstanceId()
        {
            return this.guid.ToString();
        }


    }
}
