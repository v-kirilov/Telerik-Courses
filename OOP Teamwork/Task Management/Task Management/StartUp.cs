using System;
using Task_Management.Core;
using Task_Management.Core.Contracts;
using Task_Management.UX;

namespace Task_Management
{
    internal class StartUp
    {
        static void Main(string[] args)
{
            UXClass.WelcomeMessage();
            IRepository repository = new Repository();
            ICommandFactory commandFactory = new CommandFactory(repository);
            IEngine engine = new Core.Engine(commandFactory);
            engine.Start();
        }
    }
}
