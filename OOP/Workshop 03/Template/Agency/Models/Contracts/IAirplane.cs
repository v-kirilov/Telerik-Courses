namespace Agency.Models.Contracts
{
    public interface IAirplane : IVehicle
    {
        bool IsLowCost { get; }
    }
}