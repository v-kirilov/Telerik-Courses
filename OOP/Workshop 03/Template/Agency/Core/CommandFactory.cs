using Agency.Commands;
using Agency.Commands.Contracts;
using Agency.Core.Contracts;

using System;
using System.Collections.Generic;

namespace Agency.Core
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
            // RemoveEmptyEntries makes sure no empty strings are added to the result of the split operation.
            string[] arguments = commandLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string commandName = this.ExtractCommandName(arguments);
            List<string> commandParameters = this.ExtractCommandParameters(arguments);

            switch (commandName.ToLower())
            {
                case "createairplane":
                    return new CreateAirplaneCommand(commandParameters, this.repository);
                case "createbus":
                    return new CreateBusCommand(commandParameters, this.repository);
                case "createtrain":
                    return new CreateTrainCommand(commandParameters, this.repository);
                case "createticket":
                    return new CreateTicketCommand(commandParameters, this.repository);
                case "createjourney":
                    return new CreateJourneyCommand(commandParameters, this.repository);
                case "listjourneys":
                    return new ListJourneysCommand(this.repository);
                case "listtickets":
                    return new ListTicketsCommand(this.repository);
                case "listvehicles":
                    return new ListVehiclesCommand(this.repository);
                default:
                    throw new InvalidOperationException($"Command with name: {commandName} doesn't exist!");
            }
        }

        // Receives a full line and extracts the command to be executed from it.
        // For example, if the input line is "FilterBy Assignee John", the method will return "FilterBy".
        private string ExtractCommandName(string[] arguments)
        {
            string commandName = arguments[0];
            return commandName;
        }

        // Receives a full line and extracts the parameters that are needed for the command to execute.
        // For example, if the input line is "FilterBy Assignee John",
        // the method will return a list of ["Assignee", "John"].
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
