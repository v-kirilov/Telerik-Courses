using Task_Management.Commands.Contracts;


namespace Task_Management.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand Create(string commandLine);
    }
}
