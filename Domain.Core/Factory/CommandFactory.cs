using Domain.Entities.TurtleChallenge;

namespace Domain.Core.Factory
{
    public static class CommandFactory
    {
        public static ACommand CommandFactoryCreator(string command)
        {
            if (CommandMove.IDENTIFIER.Contains(command))
                return new CommandMove();
            else if (CommandRotate.IDENTIFIER.Contains(command))
                return new CommandRotate();
            else
                throw new ArgumentException($"Invalid command: {command}");
        }
    }
}
