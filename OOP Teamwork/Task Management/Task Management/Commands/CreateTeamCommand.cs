using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;

namespace Task_Management.Commands
{
    public class CreateTeamCommand : BaseCommand
    {
        public CreateTeamCommand(IList<string> commandParameters, IRepository repository)
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
            string teamName = this.CommandParameters[0];
            var team = this.Repository.CreateTeam(teamName);
            return $"Team with name: [{teamName}] was created!";
        }
    }
}
