using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Commands;
using Task_Management.Commands.Contracts;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;

namespace Task_Management.Core
{
    public class CommandFactory : ICommandFactory
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

            switch (commandName.ToLower())

            {
                case "createteam":
                    return new CreateTeamCommand(commandParameters, this.repository);
                case "showallpeople":
                    return new ShowAllPeople(this.repository);
                default:
                    throw new InvalidUserInputException($"Command with name: { commandName } doesn't exist!");
            }
        }


        //Extracts command name from first element in the array.
        private string ExtractCommandName(string[] arguments)
        {
            string commandName = arguments[0];
            return commandName;
        }
        //Extracts the rest of the parameters 
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
