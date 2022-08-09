using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands
{
    public class ShowAllTeamMembersCommand : BaseCommand
    {
        public ShowAllTeamMembersCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (this.CommandParameters.Count != 1)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 1, Received: {this.CommandParameters.Count}");
            }

            // Parameters:
            //  [0] - name of team , must be unique

            if (this.Repository.Teams.Count != 0)
            {
                var sb = new StringBuilder();

                var teamName = CommandParameters[0];
                ITeam team = this.Repository.FindTeamByName(teamName);

                foreach (var member in team.Members)
                {
                    sb.Append(member);

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
