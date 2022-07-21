namespace Agency.Models.Contracts
{
    public interface ITrain : IVehicle
    {
        int Carts { get; }
    }
}