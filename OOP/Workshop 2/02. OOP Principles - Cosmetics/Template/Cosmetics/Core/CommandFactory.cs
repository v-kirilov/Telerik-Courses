using Cosmetics.Commands;
using Cosmetics.Commands.Contracts;
using Cosmetics.Core.Contracts;
using System;
using System.Collections.Generic;

namespace Cosmetics.Core
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
            string commandName = ExtractCommandName(commandLine);
            List<string> commandParameters = this.ExtractCommandParameters(commandLine);

            switch (commandName.ToLower())
            {
                case "createcategory":
                    return new CreateCategoryCommand(commandParameters, repository);
                case "createshampoo":
                    return new CreateShampooCommand(commandParameters, repository);
                case "createtoothpaste":
                   return new CreateToothpasteCommand(commandParameters, repository);
                case "createcream":
                    return new CreateCreamCommand(commandParameters, repository);
                case "addtocategory":
                    return new AddToCategoryCommand(commandParameters, repository);
                case "removefromcategory":
                    return new RemoveFromCategoryCommand(commandParameters, repository);
                case "addtoshoppingcart":
                    return new AddToShoppingCartCommand(commandParameters, repository);
                case "removefromshoppingcart":
                    return new RemoveFromShoppingCartCommand(commandParameters, repository);
                case "showcategory":
                    return new ShowCategoryCommand(commandParameters, repository);
                case "totalprice":
                    return new TotalPriceCommand(repository);
                default:
                    throw new ArgumentException($"Command with name: {commandName} doesn't exist!");
            }
        }

        // Receives a full line and extracts the command to be executed from it.
        // For example, if the input line is "FilterBy Assignee John", the method will return "FilterBy".
        private string ExtractCommandName(string commandLine)
        {
            return commandLine.Split(" ")[0];
        }

        // Receives a full line and extracts the parameters that are needed for the command to execute.
        // For example, if the input line is "FilterBy Assignee John",
        // the method will return a list of ["Assignee", "John"].
        private List<string> ExtractCommandParameters(string commandLine)
        {
            string[] commandTokens = commandLine.Split(" ");
            List<string> parameters = new List<string>();
            for (int i = 1; i < commandTokens.Length; i++)
            {
                parameters.Add(commandTokens[i]);
            }
            return parameters;
        }

    }
}
