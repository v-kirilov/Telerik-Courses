using Cosmetics.Commands.Contracts;
using Cosmetics.Core.Contracts;
using Cosmetics.Helpers;
using Cosmetics.Models;
using System.Collections.Generic;

namespace Cosmetics.Commands
{
    public class AddToShoppingCartCommand : ICommand
    {
        public const int ExpectedNumberOfArguments = 1;

        private readonly List<string> commandParameters;
        private readonly IRepository repository;

        public AddToShoppingCartCommand(List<string> commandParameters, IRepository repository)
        {
            this.commandParameters = commandParameters;
            this.repository = repository;
        }

        public string Execute()
        {
            ValidationHelpers.ValidateArgumentsCount(commandParameters, ExpectedNumberOfArguments);
            string productToAddToCart = commandParameters[0];
            return AddToShoppingCart(productToAddToCart);
        }

        private string AddToShoppingCart(string productName)
        {
            Product product = repository.FindProductByName(productName);
            repository.ShoppingCart.AddProduct(product);

            return $"Product {productName} was added to the shopping cart!";
        }
    }
}
