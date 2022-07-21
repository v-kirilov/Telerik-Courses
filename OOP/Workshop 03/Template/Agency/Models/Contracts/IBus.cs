namespace Agency.Models.Contracts
{
    public interface IBus :IVehicle
    {
        bool HasFreeTv { get; }
    }
}