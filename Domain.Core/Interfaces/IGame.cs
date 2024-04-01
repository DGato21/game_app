namespace Domain.Core.Interfaces
{
    public interface IGame
    {
        public abstract void Start();
        public string GetInstanceId();
    }
}
