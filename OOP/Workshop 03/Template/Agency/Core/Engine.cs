using Agency.Commands.Contracts;
using Agency.Core.Contracts;

using System;

namespace Agency.Core
{
    public class Engine : IEngine
    {
        private const string TerminationCommand = "exit";
        private const string EmptyCommandError = "Command cannot be empty.";

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
                        Console.WriteLine(EmptyCommandError);
                        continue;
                    }

                    if (inputLine.ToLower() == TerminationCommand)
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
                        Console.WriteLine(ex);
                    }
                }
            }
        }
    }
}
