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
                case "createmember":
                    return new CreateMemberCommand(commandParameters, this.repository);
                case "showallpeople":
                    return new ShowAllPeopleCommand(this.repository);
                case "showactivity":
                    return new ShowMembersActivityCommand(commandParameters,this.repository);
                case "showallteams":
                    return new ShowAllTeams(this.repository);
                case "createboard":
                    return new CreateBoardCommand(commandParameters,this.repository);
                case "createbug":
                    return new CreateBugCommand(commandParameters, this.repository);
                case "createstory":
                    return new CreateStoryCommand(commandParameters, this.repository);
                case "createfeedback":
                    return new CreateFeedbackCommand(commandParameters, this.repository);
                case "addmembertoteam":
                    return new AddMemberToTeamCommand(commandParameters, this.repository);
                case "showteamsactivity":
                    return new ShowTeamsActivityCommand(commandParameters, this.repository);
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
