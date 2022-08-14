using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands
{
    public class ShowTeamsActivityCommand : BaseCommand
    {
        public ShowTeamsActivityCommand(IList<string> commandParameters, IRepository repository)
           : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            if (this.Repository.Members.Count != 0)
            {
                var sb = new StringBuilder();

                if (this.CommandParameters.Count != 1)
                {
                    throw new InvalidUserInputException($"Invalid number of arguments. Expected: 1, Received: {this.CommandParameters.Count}");
                }
                var teamName = this.CommandParameters[0];
                // Parameters:
                //  [0] - name of team which history we would like to view.
                if (this.Repository.Teams.Count == 0)
                {
                    return $"There are no teams created.";
                }
                if (!this.Repository.Teams.Any(x=>x.Name==teamName))
                {
                    return $"There are no teams with name: [{teamName}].";
                }  
                else
                {
                    var team = this.Repository.Teams.FirstOrDefault(x => x.Name == teamName);
                    if (team.Members.Count == 0 && team.Boards.Count == 0)
                    {
                        return $"There are no members and boards in team:[{teamName}]!";
                    }
                    if (team.Members.Count!=0)
                    {
                        foreach (var member in team.Members)
                        {
                            sb.AppendLine($"Member name: [{member.Name}]");
                            foreach (var eventLog in member.EventLogs)
                            {
                                sb.AppendLine(eventLog.ViewInfo());
                            }
                        }
                    }
                    if (team.Boards.Count!=0)
                    {
                        foreach (var board in team.Boards)
                        {
                            sb.AppendLine($"Baord name: [{board.Name}]");
                            foreach (var eventLog in board.EventLogs)
                            {
                                sb.AppendLine(eventLog.ViewInfo());
                            }
                        }
                    }
                }
                

                return sb.ToString();
            }
            else
            {
                return "There are no members created!";

            }

        }
    }
}
