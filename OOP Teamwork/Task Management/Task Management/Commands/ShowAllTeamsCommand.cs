using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Core;

namespace Task_Management.Commands
{
    internal class ShowAllTeams : BaseCommand
    {
        public ShowAllTeams(IRepository repository)
            : base(null, repository)
        {

        }

        public override string Execute()
        {
            if (this.Repository.Teams.Count != 0)
            {
                var sb = new StringBuilder();

                foreach (var team in this.Repository.Teams)
                {
                    sb.Append(team.ToString());
                    sb.AppendLine("####################");
                }

                return sb.ToString();
            }
            else
            {
                return "There are no teams created!";

            }

        }
    }
}
