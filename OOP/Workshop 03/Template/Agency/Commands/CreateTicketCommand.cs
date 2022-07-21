using Agency.Commands.Abstracts;
using Agency.Core.Contracts;
using System;
using System.Collections.Generic;

namespace Agency.Commands
{
    public class CreateTicketCommand : BaseCommand
    {
        public CreateTicketCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }
        
        public override string Execute()
        {
            // Parameters:

            //  [0] - journey
            //  [1] - administrative costs
            int journeyId = this.ParseIntParameter(this.CommandParameters[0], "journeyId");
            double administrativeCosts = this.ParseDoubleParameter(this.CommandParameters[1], "administrativeCosts");
            var journey = base.Repository.FindJourneyById(journeyId);
            var ticket = base.Repository.CreateTicket(journey, administrativeCosts);

            return $"Ticket with ID {ticket.Id} was created.";

        }
    }
}
