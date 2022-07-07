using Cosmetics.Commands.Contracts;
using Cosmetics.Core.Contracts;
using System;

namespace Cosmetics.Core
{
    public sealed class Engine : IEngine
    {
        private const string TerminationCommand = "Exit";
        private const string EmptyCommand = "Command cannot be empty.";

        private readonly ICommandFactory commandFactory;

        public Engine(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    string inputLine = Console.ReadLine().Trim();

                    if (inputLine == string.Empty)
                    {
                        Console.WriteLine(EmptyCommand);
                        continue;
                    }

                    if (inputLine.Equals(TerminationCommand, StringComparison.InvariantCultureIgnoreCase))
                    {
                        break;
                    }

                    ICommand command = this.commandFactory.Create(inputLine);
                    string result = command.Execute();
                    Console.WriteLine(result.Trim());

                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(ex.Message))
                    {
                        Console.WriteLine(ex.Message);
                    }
                    else
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }
    }
}
