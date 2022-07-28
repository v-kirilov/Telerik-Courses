namespace Dealership.Models.Contracts
{
    public interface ITruck : IVehicle
    {
        int WeightCapacity { get; }
    }
}
