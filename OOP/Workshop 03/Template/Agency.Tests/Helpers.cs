using Agency.Models.Contracts;
using Agency.Models;
using System.Collections.Generic;

namespace Agency.Tests
{
    public class Helpers
    {
        public static List<string> GetDummyList(int size)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < size; i++)
            {
                result.Add("str");
            }
            return result;
        }

        public static IVehicle GetTestVehicle()
        {
            return new Bus(
                    id: 1,
                    passengerCapacity: Bus.PassengerCapacityMinValue,
                    pricePerKilometer: Bus.PricePerKilometerMinValue,
                    hasFreeTv: true);
        }

        public static IJourney GetTestJourney()
        {
            return new Journey(
                    id: 1,
                    from: new string('x', Journey.StartLocationMinLength),
                    to: new string('x', Journey.DestinationMinLength),
                    distance: Journey.DistanceMinValue,
                    vehicle: Helpers.GetTestVehicle());
        }
    }
}
