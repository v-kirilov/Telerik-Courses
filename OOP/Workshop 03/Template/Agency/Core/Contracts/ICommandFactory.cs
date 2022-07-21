using Agency.Commands.Contracts;

namespace Agency.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand Create(string commandLine);
    }
}
