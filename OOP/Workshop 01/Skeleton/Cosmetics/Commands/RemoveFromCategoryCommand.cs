using System.Collections.Generic;
using Cosmetics.Commands.Contracts;
using Cosmetics.Core.Contracts;
using Cosmetics.Helpers;
using Cosmetics.Models;

namespace Cosmetics.Commands
{
    public class RemoveFromCategoryCommand : ICommand
    {
        public const int ExpectedNumberOfArguments = 2;

        private readonly List<string> commandParameters;
        private readonly IRepository repository;

        public RemoveFromCategoryCommand(List<string> commandParameters, IRepository repository)
        {
            this.commandParameters = commandParameters;
            this.repository = repository;
        }

        public string Execute()
        {
            ValidationHelpers.ValidateArgumentsCount(commandParameters, ExpectedNumberOfArguments);
            string categoryNameToRemove = commandParameters[0];
            string productToRemove = commandParameters[1];
            return RemoveCategory(categoryNameToRemove, productToRemove);
        }

        private string RemoveCategory(string categoryNameToRemove, string productToRemove)
        {
            Category category = repository.FindCategoryByName(categoryNameToRemove);
            Product product = repository.FindProductByName(productToRemove);

            category.RemoveProduct(product);

            return $"Product {productToRemove} removed from category {categoryNameToRemove}!";
        }
    }
}
