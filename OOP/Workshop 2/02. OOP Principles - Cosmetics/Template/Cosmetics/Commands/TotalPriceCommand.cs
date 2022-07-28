using Cosmetics.Core.Contracts;
using Cosmetics.Models.Contracts;

namespace Cosmetics.Commands
{
    public class TotalPriceCommand : BaseCommand
    {
        public TotalPriceCommand(IRepository repository)
            : base (repository)
        {
        }

        public override string Execute()
        {
            IShoppingCart shoppingCart = this.Repository.ShoppingCart;
            if (shoppingCart.ProductList.Count == 0)
            {
                return "No product in shopping cart.";
            }
            return $"${shoppingCart.TotalPrice():0.##} total price currently in the shopping cart.";
        }
    }
}
