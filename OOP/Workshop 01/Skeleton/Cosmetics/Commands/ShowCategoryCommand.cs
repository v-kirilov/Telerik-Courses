using System.Collections.Generic;
using Cosmetics.Commands.Contracts;
using Cosmetics.Core.Contracts;
using Cosmetics.Helpers;
using Cosmetics.Models;

namespace Cosmetics.Commands
{
    public class ShowCategoryCommand : ICommand
    {
        public const int ExpectedNumberOfArguments = 1;

        private readonly List<string> commandParameters;
        private readonly IRepository repository;

        public ShowCategoryCommand(List<string> commandParameters, IRepository repository)
        {
            this.commandParameters = commandParameters;
            this.repository = repository;
        }

        public string Execute()
        {
            ValidationHelpers.ValidateArgumentsCount(commandParameters, ExpectedNumberOfArguments);
            string categoryToShow = commandParameters[0];
            return ShowCategory(categoryToShow);
        }

        private string ShowCategory(string categoryToShow)
        {
            Category category = repository.FindCategoryByName(categoryToShow);

            return category.Print();
        }
    }
}
