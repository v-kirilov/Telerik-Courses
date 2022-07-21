using System.Collections.Generic;
using Agency.Models.Contracts;

namespace Agency.Core.Contracts
{
    public interface IRepository
    {
        IList<IVehicle> Vehicles { get; }
        IList<IJourney> Journeys { get; }
        IList<ITicket> Tickets { get; }
        IBus CreateBus(int passengerCapacity, double pricePerKilometer, bool hasFreeTv);
        IAirplane CreateAirplane(int passengerCapacity, double pricePerKilometer, bool isLowCost);
        ITrain CreateTrain(int passengerCapacity, double pricePerKilometer, int carts);
        IJourney CreateJourney(string startingLocation, string destination, int distance, IVehicle vehicle);
        ITicket CreateTicket(IJourney journey, double administrativeCosts);
        IVehicle FindVehicleById(int id);
        IJourney FindJourneyById(int id);
        ITicket FindTicketById(int id);
    }
}
