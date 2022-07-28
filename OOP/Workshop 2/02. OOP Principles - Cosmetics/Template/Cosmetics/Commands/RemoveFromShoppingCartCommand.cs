using Cosmetics.Core.Contracts;
using Cosmetics.Helpers;
using Cosmetics.Models.Contracts;
using System.Collections.Generic;

namespace Cosmetics.Commands
{
    public class RemoveFromShoppingCartCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 1;

        public RemoveFromShoppingCartCommand(IList<string> parameters, IRepository repository)
            : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            ValidationHelper.ValidateArgumentsCount(this.CommandParameters, ExpectedNumberOfArguments);
            
            string productToRemove = this.CommandParameters[0];

            return RemoveFromShoppingCart(productToRemove);
        }

        private string RemoveFromShoppingCart(string productName)
        {
            IShoppingCart shoppingCart = this.Repository.ShoppingCart;
            IProduct product = this.Repository.FindProductByName(productName);

            shoppingCart.RemoveProduct(product);

            return $"Product {productName} was removed from the shopping cart!";
        }
    }
}
