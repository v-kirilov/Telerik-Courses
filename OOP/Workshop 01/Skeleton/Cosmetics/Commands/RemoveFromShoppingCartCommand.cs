using Cosmetics.Commands.Contracts;
using Cosmetics.Core.Contracts;
using Cosmetics.Helpers;
using Cosmetics.Models;
using System.Collections.Generic;

namespace Cosmetics.Commands
{
    public class RemoveFromShoppingCartCommand : ICommand
    {
        public const int ExpectedNumberOfArguments = 1;

        private readonly List<string> commandParameters;
        private readonly IRepository repository;

        public RemoveFromShoppingCartCommand(List<string> commandParameters, IRepository repository)
        {
            this.commandParameters = commandParameters;
            this.repository = repository;
        }

        public string Execute()
        {
            ValidationHelpers.ValidateArgumentsCount(commandParameters, ExpectedNumberOfArguments);
            string productToRemoveFromCart = commandParameters[0];
            return RemoveFromShoppingCart(productToRemoveFromCart);
        }

        private string RemoveFromShoppingCart(string productName)
        {
            Product product = repository.FindProductByName(productName);

            repository.ShoppingCart.RemoveProduct(product);

            return $"Product {productName} was removed from the shopping cart!";
        }
    }
}
