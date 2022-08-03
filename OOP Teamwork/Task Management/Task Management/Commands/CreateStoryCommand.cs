using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Story;

namespace Task_Management.Commands
{
    public class CreateStoryCommand : BaseCommand
    {
        public CreateStoryCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }
        public override string Execute()
        {
            if (this.CommandParameters.Count != 6)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 6, Received: {this.CommandParameters.Count}");
            }

            // Parameters:
            //  [0] - string title
            //  [1] - string description
            //  [2] - Priority priority
            //  [3] - Size size
            //  [4] - Assignee assignee - string name
            //  [5] - Board BoardName = string name

            string title = this.CommandParameters[0];
            string description = this.CommandParameters[1];
            Priority priority = this.ParseStoryPriorityParameter(this.CommandParameters[2], "Priority");
            Size size = this.ParseSizeParameter(this.CommandParameters[3], "Size");

            string nameMember = this.CommandParameters[4];
            IMember assignee = base.Repository.FindMemberByName(nameMember);

            string nameBoard = this.CommandParameters[5];
            IBoard board = base.Repository.FindBoardByName(nameBoard);

            IsMemberInTeamWithTheGivenBoard(assignee, board);

            var story = base.Repository.CreateStory(title, description,priority, size, assignee);
            assignee.AddTask(story);
            board.AddTask(story);

            return $"Story with ID: {story.Id} and title: {story.Title} was created.";
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
