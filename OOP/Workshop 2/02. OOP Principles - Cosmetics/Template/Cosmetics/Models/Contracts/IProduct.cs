using Cosmetics.Models.Enums;

namespace Cosmetics.Models.Contracts
{
    public interface IProduct
    {
        string Name { get; }

        string Brand { get; }

        decimal Price { get; }

        GenderType Gender { get; }

        string Print();
    }
}
