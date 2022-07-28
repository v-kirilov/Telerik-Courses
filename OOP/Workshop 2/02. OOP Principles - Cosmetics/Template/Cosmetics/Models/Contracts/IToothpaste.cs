namespace Cosmetics.Models.Contracts
{
    public interface IToothpaste : IProduct
    {
        string Ingredients { get; }
    }
}