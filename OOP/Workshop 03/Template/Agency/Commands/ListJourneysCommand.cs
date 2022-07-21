using Agency.Commands.Abstracts;
using Agency.Core.Contracts;
using Agency.Models.Contracts;

using System.Text;

namespace Agency.Commands
{
    public class ListJourneysCommand : BaseCommand
    {
        public ListJourneysCommand(IRepository repository)
            : base(repository)
        {
        }

        public override string Execute()
        {
            if (this.Repository.Journeys.Count > 0)
            {
                var sb = new StringBuilder();

                foreach (IJourney journey in this.Repository.Journeys)
                {
                    sb.Append(journey.ToString());
                    sb.AppendLine("####################");
                }

                return sb.ToString().Trim();
            }
            else
            {
                return "There are no registered journeys.";
            }
        }
    }
}
