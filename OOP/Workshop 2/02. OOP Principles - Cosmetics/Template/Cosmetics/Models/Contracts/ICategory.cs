namespace Cosmetics.Models.Contracts
{
    public interface ICategory
    {
        string Name { get; }

        void AddProduct(IProduct product);

        void RemoveProduct(IProduct product);

        string Print();
    }
}
