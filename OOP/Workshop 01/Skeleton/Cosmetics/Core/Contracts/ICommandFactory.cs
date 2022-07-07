using Cosmetics.Commands.Contracts;

namespace Cosmetics.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand Create(string commandLine);
    }
}
