namespace Agency.Models.Contracts
{
    public interface ITicket : IHasId
    {
        double AdministrativeCosts { get; }
        IJourney Journey { get; }
        double CalculatePrice();
    }
}