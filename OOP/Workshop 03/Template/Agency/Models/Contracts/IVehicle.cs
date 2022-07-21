namespace Agency.Models.Contracts
{
    public interface IVehicle : IHasId
    {
        int PassangerCapacity { get; }
        double PricePerKilometer { get; }
    }
}