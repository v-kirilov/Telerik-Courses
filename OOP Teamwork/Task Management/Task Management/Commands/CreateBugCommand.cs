using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Bug;

namespace Task_Management.Commands
{
    public class CreateBugCommand : BaseCommand
    {
        public CreateBugCommand(IList<string> commandParameters, IRepository repository)
            : base (commandParameters, repository)
        {
        }
        public override string Execute()
        {
            if (this.CommandParameters.Count != 7)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 7, Received: {this.CommandParameters.Count}");
            }

            // Parameters:
            //  [0] - string title
            //  [1] - string description
            //  [2] - string steps
            //  [3] - Priority priority
            //  [4] - Severity severity
            //  [5] - Assignee assignee - string name
            //  [6] - Board BoardName = string name

            string title = this.CommandParameters[0];
            string description = this.CommandParameters[1];
            string steps = this.CommandParameters[2];
            Priority priority = this.ParseBugPriorityParameter(this.CommandParameters[3], "Priority");
            Severity severity = this.ParseSeverityParameter(this.CommandParameters[4], "Severity");

            string nameMember = this.CommandParameters[5];
            IMember assignee = base.Repository.FindMemberByName(nameMember);

            string nameBoard = this.CommandParameters[6];
            IBoard board = base.Repository.FindBoardByName(nameBoard);

            IsMemberInTeamWithTheGivenBoard(assignee, board);

            var bug = base.Repository.CreateBug(title, description, steps, priority, severity, assignee);
            assignee.AddTask(bug);
            board.AddTask(bug);

            return $"Bug with ID: {bug.Id} and title: {bug.Title} was created.";
        }

        public void IsMemberInTeamWithTheGivenBoard(IMember assignee, IBoard board)
        {
            var teamOfTheMember = TeamOfTheGivenMember(assignee);
            if (!teamOfTheMember.Boards.Contains(board))
            {
                throw new EntityNotFoundException($"The team: {teamOfTheMember.Name} of the member: {assignee.Name} doesn't contain the given board: {board.Name}");
            } 
        }

        private ITeam TeamOfTheGivenMember(IMember assignee)
        {
            if (base.Repository.Teams.Any(x => x.Members.Contains(assignee)))
            {
                var team = base.Repository.Teams.First(x => x.Members.Contains(assignee));
                return team;
            }

            throw new EntityNotFoundException($"The given member:{assignee.Name} isn't in a team and you can't assign a task to him.");
        }
    }
}
