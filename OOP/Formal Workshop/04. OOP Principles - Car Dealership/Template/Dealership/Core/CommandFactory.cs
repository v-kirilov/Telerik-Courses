using Dealership.Commands;
using Dealership.Commands.Contracts;
using Dealership.Core.Contracts;
using Dealership.Exceptions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dealership.Core
{
    public class CommandFactory : ICommandFactory
    {
        private const char SplitCommandSymbol = ' ';
        private const string CommentOpenSymbol = "{{";
        private const string CommentCloseSymbol = "}}";

        private readonly IRepository repository;

        public CommandFactory(IRepository repository)
        {
            this.repository = repository;
        }

        public ICommand Create(string commandLine)
        {
            string commandName = this.ExtractCommandName(commandLine);
            List<string> commandParameters = this.ExtractCommandParameters(commandLine);

            ICommand command;
            switch (commandName.ToLower())
            {
                case "registeruser":
                    command = new RegisterUserCommand(commandParameters, this.repository);
                    break;
                case "login":
                    command = new LoginCommand(commandParameters, this.repository);
                    break;
                case "logout":
                    command = new LogoutCommand(this.repository);
                    break;
                case "addvehicle":
                    command = new AddVehicleCommand(commandParameters, this.repository);
                    break;
                case "removevehicle":
                    command = new RemoveVehicleCommand(commandParameters, this.repository);
                    break;
                case "addcomment":
                    command = new AddCommentCommand(commandParameters, this.repository);
                    break;
                case "removecomment":
                    command = new RemoveCommentCommand(commandParameters, this.repository);
                    break;
                case "showusers":
                    command = new ShowUsersCommand(this.repository);
                    break;
                    //ToDo
                    throw new NotImplementedException();
                case "showvehicles":
                    command = new ShowVehiclesCommand(commandParameters, this.repository);
                    break;
                default:
                    throw new InvalidUserInputException($"Command with name: {commandName} doesn't exist!");
            }
            return command;
        }

        // Receives a full line and extracts the command to be executed from it.
        // For example, if the input line is "FilterBy Assignee John", the method will return "FilterBy".
        private string ExtractCommandName(string commandLine)
        {
            string commandName = commandLine.Split(SplitCommandSymbol)[0];
            return commandName;
        }

        // Receives a full line and extracts the parameters that are needed for the command to execute.
        // For example, if the input line is "FilterBy Assignee John",
        // the method will return a list of ["Assignee", "John"].
        private List<string> ExtractCommandParameters(string commandLine)
        {
            List<string> parameters = new List<string>();

            var indexOfOpenComment = commandLine.IndexOf(CommentOpenSymbol);
            var indexOfCloseComment = commandLine.IndexOf(CommentCloseSymbol);
            if (indexOfOpenComment >= 0)
            {
                var commentStartIndex = indexOfOpenComment + CommentOpenSymbol.Length;
                var commentLength = indexOfCloseComment - CommentCloseSymbol.Length - indexOfOpenComment;
                string commentParameter = commandLine.Substring(commentStartIndex, commentLength);
                parameters.Add(commentParameter);

                Regex regex = new Regex("{{.+(?=}})}}");
                commandLine = regex.Replace(commandLine, string.Empty);
            }

            var indexOfFirstSeparator = commandLine.IndexOf(SplitCommandSymbol);
            parameters.AddRange(commandLine.Substring(indexOfFirstSeparator + 1).Split(new[] { SplitCommandSymbol }, StringSplitOptions.RemoveEmptyEntries));

            return parameters;
        }
    }
}
