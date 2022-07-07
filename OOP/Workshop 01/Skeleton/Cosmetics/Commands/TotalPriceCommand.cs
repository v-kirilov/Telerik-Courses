using Cosmetics.Commands.Contracts;
using Cosmetics.Core.Contracts;
using Cosmetics.Models;

namespace Cosmetics.Commands
{
    public class TotalPriceCommand : ICommand
    {
        private readonly IRepository cosmeticsRepository;

        public TotalPriceCommand(IRepository cosmeticsRepository)
        {
            this.cosmeticsRepository = cosmeticsRepository;
        }

        public string Execute()
        {
            ShoppingCart cart = cosmeticsRepository.ShoppingCart;
            if (cart.Products.Count == 0)
            {
                return "No products in shopping cart!";
            }
            return $"${cart.TotalPrice():0.##} total price currently in the shopping cart!";
        }
    }
}
