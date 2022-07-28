using Cosmetics.Core.Contracts;
using Cosmetics.Helpers;
using Cosmetics.Models.Contracts;
using System.Collections.Generic;

namespace Cosmetics.Commands
{
    public class RemoveFromCategoryCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 2;

        public RemoveFromCategoryCommand(IList<string> parameters, IRepository repository)
            : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            ValidationHelper.ValidateArgumentsCount(this.CommandParameters, ExpectedNumberOfArguments);

            string categoryName = this.CommandParameters[0];
            string productNameToRemove = this.CommandParameters[1];

            return RemoveFromCategory(categoryName, productNameToRemove);
        }

        private string RemoveFromCategory(string categoryName, string productName)
        {
            ICategory category = this.Repository.FindCategoryByName(categoryName);
            IProduct product = this.Repository.FindProductByName(productName);

            category.RemoveProduct(product);

            return string.Format($"Product {productName} removed from category {categoryName}!");
        }
    }
}
