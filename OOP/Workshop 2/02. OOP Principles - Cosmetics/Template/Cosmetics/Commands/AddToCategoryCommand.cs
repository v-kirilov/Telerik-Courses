using Cosmetics.Core.Contracts;
using Cosmetics.Helpers;
using Cosmetics.Models.Contracts;
using System.Collections.Generic;

namespace Cosmetics.Commands
{
    public class AddToCategoryCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 2;

        public AddToCategoryCommand(IList<string> parameters, IRepository repository)
            : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            ValidationHelper.ValidateArgumentsCount(this.CommandParameters, ExpectedNumberOfArguments);

            string categoryName = this.CommandParameters[0];
            string productNameToAdd = this.CommandParameters[1];

            return AddToCategory(categoryName, productNameToAdd);
        }

        private string AddToCategory(string categoryName, string productName)
        {
            ICategory category = this.Repository.FindCategoryByName(categoryName);
            IProduct product = this.Repository.FindProductByName(productName);

            category.AddProduct(product);

            return $"Product {productName} added to category {categoryName}!";
        }
    }
}
