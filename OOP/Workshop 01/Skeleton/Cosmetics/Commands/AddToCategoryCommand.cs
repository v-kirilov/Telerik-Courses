using Cosmetics.Commands.Contracts;
using Cosmetics.Core.Contracts;
using Cosmetics.Helpers;
using Cosmetics.Models;
using System.Collections.Generic;

namespace Cosmetics.Commands
{
    public class AddToCategoryCommand : ICommand
    {
        public const int ExpectedNumberOfArguments = 2;

        private readonly List<string> commandParameters;
        private readonly IRepository repository;

        public AddToCategoryCommand(List<string> commandParameters, IRepository repository)
        {
            this.commandParameters = commandParameters;
            this.repository = repository;
        }

        public string Execute()
        {
            ValidationHelpers.ValidateArgumentsCount(commandParameters, ExpectedNumberOfArguments);
            string categoryNameToAdd = commandParameters[0];
            string productToAdd = commandParameters[1];
            return this.AddToCategory(categoryNameToAdd, productToAdd);
        }

        private string AddToCategory(string categoryNameToAdd, string productToAdd)
        {
            Category category = this.repository.FindCategoryByName(categoryNameToAdd);
            Product product = this.repository.FindProductByName(productToAdd);

            category.AddProduct(product);

            return $"Product {productToAdd} added to category {categoryNameToAdd}!";
        }
    }
}
