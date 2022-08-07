using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands
{
    public class ListAllTasksCommand : BaseCommand
    {
        public ListAllTasksCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (this.CommandParameters.Count != 1)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 1, Received: {this.CommandParameters.Count}");
            }

            // [0] - string filter: All or Exact Title

            string filter = this.CommandParameters[0];

            return ListAllTasks(filter);
        }

        public string ListAllTasks(string filter)
        {
            var allTasksList = base.Repository.Bugs.Concat<ITask>(base.Repository.Stories).Concat<ITask>(base.Repository.Feedbacks).OrderBy(x => x.Title).ToList();
            if (filter.ToLower() == "all")
            {
                return PrintTasks(allTasksList);
            }
            else
            {
                ValidateTitleFilter(filter, allTasksList);
                var allTasksListFiltered = allTasksList.Where(x => x.Title.ToLower() == filter.ToLower());
                return PrintTasks(allTasksListFiltered);
            }

        }

        private string PrintTasks(IEnumerable<ITask> tasks)
        {
            var sb = new StringBuilder();
            foreach (var task in tasks)
            {
                int counter = 1;
                sb.AppendLine($"{counter}: {task}");
                Console.WriteLine();
                counter++;
            }
            return sb.ToString().Trim();
        }

        private void ValidateTitleFilter(string filter, IList<ITask> allTasksList)
        {
            if (!allTasksList.Any(x => x.Title.ToLower() == filter.ToLower()))
            {
                throw new InvalidUserInputException($"Cannot filter tasks by title: {filter}. This title doesn't exist.");
            }
        }
    }
}
