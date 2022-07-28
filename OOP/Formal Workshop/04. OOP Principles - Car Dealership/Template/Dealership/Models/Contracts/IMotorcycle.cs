namespace Dealership.Models.Contracts
{
    public interface IMotorcycle : IVehicle
    {
        string Category { get; }
    }
}
