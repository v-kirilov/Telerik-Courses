using Dealership.Commands.Contracts;

namespace Dealership.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand Create(string commandLine);
    }
}
