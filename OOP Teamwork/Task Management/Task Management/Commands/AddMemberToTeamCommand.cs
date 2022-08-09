using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models.Contracts;
using Task_Management.Models;
using System.Linq;

namespace Task_Management.Commands
{
    public class AddMemberToTeamCommand : BaseCommand
    {
        public AddMemberToTeamCommand(IList<string> commandParameters, IRepository repository)
           : base(commandParameters, repository)
        {
        }


        public override string Execute()
        {
            if (this.CommandParameters.Count != 2)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 2, Received: {this.CommandParameters.Count}");
            }
            // Parameters:
            //  [0] - name of team to assign the member to.
            //[1] - Name of Member
            string teamName = this.CommandParameters[0];
            string memberName = this.CommandParameters[1];

            if (!this.Repository.Members.Any(x => x.Name == memberName))
            {
                throw new InvalidUserInputException($"A Member with name:[{memberName}] does not exists.");
            }

            if (!this.Repository.Teams.Any(x => x.Name == teamName))
            {
                throw new InvalidUserInputException($"A Team with name:[{teamName}] does not exists.");
            }else
            {
                IMember member = this.Repository.Members.FirstOrDefault(x => x.Name == memberName);
                ITeam team = this.Repository.Teams.FirstOrDefault(x => x.Name == teamName);

                team.AddMember(member);
            }


            return $"Member with name: [{memberName}] has been added to team: [{teamName}]!";
        }
    }
}
