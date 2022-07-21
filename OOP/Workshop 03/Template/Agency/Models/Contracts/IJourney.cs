namespace Agency.Models.Contracts
{
    public interface IJourney : IHasId
    {
        string StartLocation { get; }
        string Destination { get; }
        int Distance { get; }
        IVehicle Vehicle { get; }
        double CalculateTravelCosts();
    }
}