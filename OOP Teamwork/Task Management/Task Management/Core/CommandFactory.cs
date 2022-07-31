using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;

namespace Task_Management.Core
{
    internal class CommandFactory : ICommandFactory
    {
        private readonly IRepository repository;
        public CommandFactory(IRepository repository)
        {
            this.repository = repository;
        }

        public ICommand Create(string commandLine)
        {
            string[] arguments = commandLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string commandName = this.ExtractCommandName(arguments);
            List<string> commandParameters = this.ExtractCommandParameters(arguments);

            switch (commandLine.ToLower())
            {
                case "":
                //return new "Commandname" (commandParameters, this.repository)

                default:
                    throw new InvalidUserInputException($"Command with name: { commandName } doesn't exist!");
            }
        }



        private string ExtractCommandName(string[] arguments)
        {
            string commandName = arguments[0];
            return commandName;
        }

        private List<String> ExtractCommandParameters(string[] arguments)
        {
            List<string> commandParameters = new List<string>();

            for (int i = 1; i < arguments.Length; i++)
            {
                commandParameters.Add(arguments[i]);
            }

            return commandParameters;
        }
    }

}
