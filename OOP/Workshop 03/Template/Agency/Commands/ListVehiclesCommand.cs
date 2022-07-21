using Agency.Commands.Abstracts;
using Agency.Core.Contracts;
using System;
using System.Text;

namespace Agency.Commands
{
    public class ListVehiclesCommand : BaseCommand
    {
        public ListVehiclesCommand(IRepository repository)
            : base(repository)
        {
        }

        public override string Execute()
        {
            //TODO Implement command.
            if (this.Repository.Vehicles.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var vehicle in this.Repository.Vehicles)
                {
                    sb.Append(vehicle.ToString());
                    sb.AppendLine("####################");

                }
                return sb.ToString().Trim();
            }
            else
            {
                return "There are no registered vehicles.";
            }
        }
    }
}
