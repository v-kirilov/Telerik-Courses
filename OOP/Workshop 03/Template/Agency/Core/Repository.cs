using Agency.Core.Contracts;
using Agency.Exceptions;
using Agency.Models;
using Agency.Models.Contracts;

using System;
using System.Collections.Generic;

namespace Agency.Core
{
    public class Repository : IRepository
    {
        private int nextId;
        private readonly List<IVehicle> vehicles = new List<IVehicle>();
        private readonly List<IJourney> journeys = new List<IJourney>();
        private readonly List<ITicket> tickets = new List<ITicket>();

        public Repository()
        {
            this.nextId = 0;
        }

        public IList<IVehicle> Vehicles
        {
            get
            {
                var copy = new List<IVehicle>(this.vehicles);
                return copy;
            }
        }
        public IList<IJourney> Journeys
        {
            get
            {
                var copy = new List<IJourney>(this.journeys);
                return copy;
            }
        }
        public IList<ITicket> Tickets
        {
            get
            {
                var copy = new List<ITicket>(this.tickets);
                return copy;
            }
        }

        public IBus CreateBus(int passengerCapacity, double pricePerKilometer, bool hasFreeTv)
        {
            var bus = new Bus(++nextId, passengerCapacity, pricePerKilometer, hasFreeTv);
            this.vehicles.Add(bus);
            return bus;
        }

        public IAirplane CreateAirplane(int passengerCapacity, double pricePerKilometer, bool isLowCost)
        {
            Airplane airplane = new Airplane(++nextId, passengerCapacity, pricePerKilometer, isLowCost);
            this.vehicles.Add(airplane);
            return airplane;
        }

        public ITrain CreateTrain(int passengerCapacity, double pricePerKilometer, int carts)
        {
            var train = new Train(++nextId, passengerCapacity, pricePerKilometer, carts);
            this.vehicles.Add(train);
            return train;
        }

        public IJourney CreateJourney(string startLocation, string destination, int distance, IVehicle vehicle)
        {
            IJourney journey = new Journey(++nextId, startLocation, destination, distance, vehicle);
            this.journeys.Add(journey);
            return journey;
        }

        public ITicket CreateTicket(IJourney journey, double administrativeCosts)
        {
            ITicket ticket = new Ticket(++nextId,journey, administrativeCosts);
            this.tickets.Add(ticket);
            return ticket;

        }

        public IVehicle FindVehicleById(int id)
        {
            foreach (var vehicle in this.vehicles)
            {
                if (vehicle.Id == id)
                {
                    return vehicle;
                }
            }

            throw new EntityNotFoundException($"Vehicle with id: {id} was not found!");
        }

        public IJourney FindJourneyById(int id)
        {
            foreach (var journey in this.journeys)
            {
                if (journey.Id==id)
                {
                    return journey;
                }
            }
            throw new EntityNotFoundException($"Journey with id: {id} was not found!");

        }

        public ITicket FindTicketById(int id)
        {
            foreach (var ticket in this.tickets)
            {
                if (ticket.Id==id)
                {
                    return ticket;
                }
            }
            throw new EntityNotFoundException($"Ticket with id: {id} was not found!");

        }

    }
}
