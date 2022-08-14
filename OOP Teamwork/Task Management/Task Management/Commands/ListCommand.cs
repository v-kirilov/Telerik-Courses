using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands
{
    public class ListCommand : BaseCommand
    {
        public ListCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < 3)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 3, Received: {this.CommandParameters.Count}");
            }

            // [0] - string type of Task : Bugs, Stories, Feedbacks
            // [1] - string filter by: status or assignee
            // IF
                // [2] - string "and"
                // [3] - string filter2 - assignee
                // [4] - string sortby: title/priority/severity/size/rating (depending on the task type)
            // ELSE
            // [2] - string sort by : title/priority/severity/size/rating (depending on the task type)

            string typeOfTask = this.CommandParameters[0];
            string filter = this.CommandParameters[1];
            string filter2 = string.Empty;
            string sortby = string.Empty;

            if (this.CommandParameters.Count > 2)
            {
                if (this.CommandParameters[2].ToLower() == "and")
                {
                    filter2 = this.CommandParameters[3];
                    sortby = this.CommandParameters[4];
                }
                else
                {
                    sortby = this.CommandParameters[2];
                }
            }            
            

            return ListTask(typeOfTask, filter, filter2, sortby);

        }

        public string ListTask(string typeOfTask, string filter, string filter2, string sortby)
        {
            switch (typeOfTask.ToLower())
            {
                case "bugs":
                    return ListBugs(filter, filter2, sortby);
                case "stories":
                    return ListStories(filter, filter2, sortby);
                case "feedbacks":
                    return ListFeedbacks(filter, filter2, sortby);
                default:
                    throw new InvalidUserInputException("You can list only bugs, stories or feedbacks");
            }
        }

        public string ListBugs(string filter, string filter2, string sortby)
        {
            var listBugs = base.Repository.Bugs;

            var listBugFiltered = new List<IBug>();
            if (filter2 == string.Empty)
            {
                if (listBugs.Any(x => x.Status.ToString().ToLower() == filter.ToLower()))
                {
                    listBugFiltered = listBugs.Where(x => x.Status.ToString().ToLower() == filter.ToLower()).ToList();
                }
                else if (listBugs.Any(x => x.Assignee.Name.ToLower() == filter.ToLower()))
                {
                    listBugFiltered = listBugs.Where(x => x.Assignee.Name.ToString().ToLower() == filter.ToLower()).ToList();
                }
                else if (filter.ToLower() == "all")
                {
                    listBugFiltered = listBugs.ToList();
                }
                else
                {
                    throw new InvalidUserInputException("No status or assignee matches the filter");
                }
            }
            else
            {
                if (!listBugs.Any(x => x.Status.ToString().ToLower() == filter.ToLower()))
                {
                    throw new InvalidUserInputException("The first filter does not match any of the status types");
                }
                listBugFiltered = listBugs.Where(x => x.Status.ToString().ToLower() == filter.ToLower()).ToList();

                if (!listBugFiltered.Any(x => x.Assignee.Name.ToLower() == filter2.ToLower()))
                {
                    throw new InvalidUserInputException("The second filter does not match any of the assignees names");
                }
                listBugFiltered = listBugFiltered.Where(x => x.Assignee.Name.ToLower() == filter2.ToLower()).ToList();
            }

            var listBugFilteredAndSorted = new List<IBug>();

            switch (sortby.ToLower())
            {
                case "title":
                    listBugFilteredAndSorted = listBugFiltered.OrderBy(x => x.Title).ToList();
                    break;
                case "priority":
                    listBugFilteredAndSorted = listBugFiltered.OrderBy(x => x.Priority).ToList();
                    break;
                case "severity":
                    listBugFilteredAndSorted = listBugFiltered.OrderBy(x => x.Severity).ToList();
                    break;
                default:
                    throw new InvalidUserInputException("You can sort bug only by Title, Priority or Severity");
            }

            var sb = new StringBuilder();
            int counter = 1;
            foreach (var bug in listBugFilteredAndSorted)
            {                
                sb.AppendLine($"{counter}: {bug}");
                sb.AppendLine($"  History of the bug:");
                sb.AppendLine($"  {bug.ViewHistory()}");
                counter++;
            }

            return sb.ToString().Trim();
        }

        public string ListStories(string filter, string filter2, string sortby)
        {
            var listStories = base.Repository.Stories;

            var listStoriesFiltered = new List<IStory>();
            if (filter2 == string.Empty)
            {
                if (listStories.Any(x => x.Status.ToString().ToLower() == filter.ToLower()))
                {
                    listStoriesFiltered = listStories.Where(x => x.Status.ToString().ToLower() == filter.ToLower()).ToList();
                }
                else if (listStories.Any(x => x.Assignee.Name.ToLower() == filter.ToLower()))
                {
                    listStoriesFiltered = listStories.Where(x => x.Assignee.Name.ToString().ToLower() == filter.ToLower()).ToList();
                }
                else if (filter.ToLower() == "all")
                {
                    listStoriesFiltered = listStories.ToList();
                }
                else
                {
                    throw new InvalidUserInputException("No status or assignee matches the filter");
                }
            }
            else
            {
                if (!listStories.Any(x => x.Status.ToString().ToLower() == filter.ToLower()))
                {
                    throw new InvalidUserInputException("The first filter does not match any of the status types");
                }
                listStoriesFiltered = listStories.Where(x => x.Status.ToString().ToLower() == filter.ToLower()).ToList();

                if (!listStoriesFiltered.Any(x => x.Assignee.Name.ToLower() == filter2.ToLower()))
                {
                    throw new InvalidUserInputException("The second filter does not match any of the assignees names");
                }
                listStoriesFiltered = listStoriesFiltered.Where(x => x.Assignee.Name.ToLower() == filter2.ToLower()).ToList();
            }

            var listStoriesFilteredAndSorted = new List<IStory>();

            switch (sortby.ToLower())
            {
                case "title":
                    listStoriesFilteredAndSorted = listStoriesFiltered.OrderBy(x => x.Title).ToList();
                    break;
                case "priority":
                    listStoriesFilteredAndSorted = listStoriesFiltered.OrderBy(x => x.Priority).ToList();
                    break;
                case "size":
                    listStoriesFilteredAndSorted = listStoriesFiltered.OrderBy(x => x.Size).ToList();
                    break;
                default:
                    throw new InvalidUserInputException("You can sort bug only by Title, Priority or Size");
            }

            var sb = new StringBuilder();
            int counter = 1;
            foreach (var story in listStoriesFilteredAndSorted)
            {
                sb.AppendLine($"{counter}: {story}");
                sb.AppendLine($"  History of the story:");
                sb.AppendLine($"  {story.ViewHistory()}");
                counter++;
            }

            return sb.ToString().Trim();
        }

        public string ListFeedbacks(string filter, string filter2, string sortby)
        {
            var listFeedbacks = base.Repository.Feedbacks;

            var listFeedbacksFiltered = new List<IFeedback>();
            if (filter2 == string.Empty)
            {
                if (listFeedbacks.Any(x => x.Status.ToString().ToLower() == filter.ToLower()))
                {
                    listFeedbacksFiltered = listFeedbacks.Where(x => x.Status.ToString().ToLower() == filter.ToLower()).ToList();
                }
                else if (filter.ToLower() == "all")
                {
                    listFeedbacksFiltered = listFeedbacks.ToList();
                }
                else
                {
                    throw new InvalidUserInputException("No status matches the filter");
                }
            }
            else
            {
                throw new InvalidUserInputException("You can filter feedback only by status.");
            }

            var listFeedbacksFilteredAndSorted = new List<IFeedback>();

            switch (sortby.ToLower())
            {
                case "title":
                    listFeedbacksFilteredAndSorted = listFeedbacksFiltered.OrderBy(x => x.Title).ToList();
                    break;
                case "rating":
                    listFeedbacksFilteredAndSorted = listFeedbacksFiltered.OrderBy(x => x.Rating).ToList();
                    break;
                default:
                    throw new InvalidUserInputException("You can sort bug only by Title or Rating.");
            }

            var sb = new StringBuilder();
            int counter = 1;
            foreach (var story in listFeedbacksFilteredAndSorted)
            {
                sb.AppendLine($"{counter}: {story}");
                sb.AppendLine($"  History of the story:");
                sb.AppendLine($"  {story.ViewHistory()}");
                counter++;
            }

            return sb.ToString().Trim();
        }
    }
}
