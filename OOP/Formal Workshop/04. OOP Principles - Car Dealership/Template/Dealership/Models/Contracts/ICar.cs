namespace Dealership.Models.Contracts
{
    public interface ICar : IVehicle
    {
        int Seats { get; }
    }
}
