using Cosmetics.Models.Enums;

namespace Cosmetics.Models.Contracts
{
    public interface IShampoo : IProduct
    {
        int Millilitres { get; }

        UsageType Usage { get; }
    }
}