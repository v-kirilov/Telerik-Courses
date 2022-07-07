using System;
using System.Collections.Generic;
using Cosmetics.Commands.Contracts;
using Cosmetics.Core.Contracts;
using Cosmetics.Helpers;

namespace Cosmetics.Commands
{
    public class CreateCategoryCommand : ICommand
    {
        public const int ExpectedNumberOfArguments = 1;

        private readonly List<string> commandParameters;
        private readonly IRepository repository;

        public CreateCategoryCommand(List<string> commandParameters, IRepository repository)
        {
            this.commandParameters = commandParameters;
            this.repository = repository;
        }

        public string Execute()
        {
            ValidationHelpers.ValidateArgumentsCount(commandParameters, ExpectedNumberOfArguments);
            string categoryName = commandParameters[0];
            return CreateCategory(categoryName);
        }

        private string CreateCategory(string categoryName)
        {
            if (repository.CategoryExist(categoryName))
            {
                throw new ArgumentException($"Category with name {categoryName} already exists!");
            }

            repository.CreateCategory(categoryName);

            return $"Category with name {categoryName} was created!";
        }
    }
}
