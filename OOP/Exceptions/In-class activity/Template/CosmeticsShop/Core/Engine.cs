using CosmeticsShop.Commands;
using CosmeticsShop.Models;
using System;
using System.Collections.Generic;

namespace CosmeticsShop.Core
{
    public class Engine
    {
        private const string TERMINATION_COMMAND = "exit";

        private readonly CommandFactory commandFactory;
        private readonly CosmeticsRepository productRepository;

        public Engine()
        {
            this.commandFactory = new CommandFactory();
            this.productRepository = new CosmeticsRepository();
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    string commandLine = Console.ReadLine();
                    if (commandLine==String.Empty)
                    {
                        Console.WriteLine("Command cannot be empty.");
                        continue;
                    }
                    if (commandLine.ToLower() == TERMINATION_COMMAND)
                    {
                        break;
                    }

                    this.ProcessCommand(commandLine);

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

        private void ProcessCommand(string commandLine)
        {
            string commandName = this.ParseCommand(commandLine);
            List<string> parameters = this.ParseParameters(commandLine);
            ICommand command = this.commandFactory.CreateCommand(commandName, this.productRepository);
            string result = command.Execute(parameters);
            Console.WriteLine(result);
        }

        private string ParseCommand(string commandLine)
        {
            string commandName = commandLine.Split(" ")[0];
            return commandName;
        }

        private List<string> ParseParameters(string commandLine)
        {
            string[] commandParts = commandLine.Split(" ");
            List<string> parameters = new List<string>();
            for (int i = 1; i < commandParts.Length; i++)
            {
                parameters.Add(commandParts[i]);
            }
            return parameters;
        }
    }
}
