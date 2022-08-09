using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands
{
    class ListTasksWithAssigneeCommand : BaseCommand
    {
        public ListTasksWithAssigneeCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < 1)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 1, Received: {this.CommandParameters.Count}");
            }

            // [0] - string filter by: status or assignee
            // IF
            // [1] - string "and"
            // [2] - string filter2 - assignee

            string filter = this.CommandParameters[0];
            string filter2 = string.Empty;

            if (this.CommandParameters.Count > 1)
            {
                if (this.CommandParameters[1].ToLower() == "and")
                {
                    filter2 = this.CommandParameters[2];
                }
            }
            

            return ListTasksWithAssignee(filter, filter2);

        }

        public string ListTasksWithAssignee(string filter, string filter2)
        {
            var sb = new StringBuilder();
            IList<IBug> bugListWithAssginee = new List<IBug>();
            IList<IStory> storyListWithAssginee = new List<IStory>();
            IList<ITask> combinedListWithAssginee = new List<ITask>();

            bool isBugListMissingFilter = false;
            bool isStoryListMissingFilter = false;

            // ONE FILTER
            if (filter2 == string.Empty)
            {
                // Bugs
                
                if (base.Repository.Bugs.Any(x => x.Status.ToString().ToLower() == filter.ToLower() && x.Assignee != null))
                {
                    bugListWithAssginee = base.Repository.Bugs.Where(x => x.Status.ToString().ToLower() == filter.ToLower()).ToList();
                }
                else if (base.Repository.Bugs.Any(x => x.Assignee.Name.ToLower() == filter.ToLower() && x.Assignee != null))
                {
                    bugListWithAssginee = base.Repository.Bugs.Where(x => x.Assignee.Name.ToString().ToLower() == filter.ToLower()).ToList();
                }
                else if (filter.ToLower() == "all")
                {
                    bugListWithAssginee = base.Repository.Bugs.ToList();
                }
                else
                {
                    isBugListMissingFilter = true;
                }

                // Stories
                if (base.Repository.Stories.Any(x => x.Status.ToString().ToLower() == filter.ToLower() && x.Assignee != null))
                {
                    storyListWithAssginee = base.Repository.Stories.Where(x => x.Status.ToString().ToLower() == filter.ToLower()).ToList();
                }
                else if (base.Repository.Stories.Any(x => x.Assignee.Name.ToLower() == filter.ToLower() && x.Assignee != null))
                {
                    storyListWithAssginee = base.Repository.Stories.Where(x => x.Status.ToString().ToLower() == filter.ToLower()).ToList();
                }
                else if (filter.ToLower() == "all")
                {
                    storyListWithAssginee = base.Repository.Stories.ToList();
                }
                else
                {
                    isStoryListMissingFilter = true;
                }

                if (isBugListMissingFilter && isStoryListMissingFilter)
                {
                    throw new InvalidUserInputException("Can't find tasks with assignee which match the given filter");
                }

                combinedListWithAssginee = bugListWithAssginee.Concat<ITask>(storyListWithAssginee).OrderBy(x => x.Title).ToList();
            }
            // WITH TWO FILTERS
            else 
            {
                if (!base.Repository.Bugs.Any(x => x.Status.ToString().ToLower() == filter.ToLower() && x.Assignee != null) &&
                    base.Repository.Bugs.Any(x => x.Assignee.Name.ToLower() == filter2.ToLower() && x.Assignee != null))
                {
                    isBugListMissingFilter = true;
                }
                bugListWithAssginee = base.Repository.Bugs.Where(x => x.Status.ToString().ToLower() == filter.ToLower() && x.Assignee != null && x.Assignee.Name.ToLower() == filter2.ToLower()).ToList();

                if (!base.Repository.Stories.Any(x => x.Status.ToString().ToLower() == filter.ToLower() && x.Assignee != null) &&
                    base.Repository.Stories.Any(x => x.Assignee.Name.ToLower() == filter2.ToLower() && x.Assignee != null))
                {
                    isStoryListMissingFilter = true;
                }
                storyListWithAssginee = base.Repository.Stories.Where(x => x.Status.ToString().ToLower() == filter.ToLower() && x.Assignee != null && x.Assignee.Name.ToLower() == filter2.ToLower()).ToList();

                if (isBugListMissingFilter && isStoryListMissingFilter)
                {
                    throw new InvalidUserInputException("Can't find tasks with assignee which match the status and assignee filters");
                }

                combinedListWithAssginee = bugListWithAssginee.Concat<ITask>(storyListWithAssginee).OrderBy(x => x.Title).ToList();
            }

            foreach (var task in combinedListWithAssginee)
            {
                int counter = 1;
                sb.AppendLine($"{counter} {task}");
                counter++;
            }

            return sb.ToString().Trim();
        }
    }
}
