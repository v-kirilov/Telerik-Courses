using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Commands.Contracts;
using Task_Management.Core.Contracts;

namespace Task_Management.Core
{
    public class Engine :IEngine
    {
        private const string TerminationCommand = "exit";
        private const string EmptyCommandError = "Command cannot be empty. Please enter a valid command!";

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

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine();
                    Console.WriteLine(new String('-', result.Length));
                    Console.WriteLine(result.Trim());
                    Console.WriteLine(new String('-', result.Length));
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Gray;


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
