using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands
{
    public class UnassignTaskCommand : BaseCommand
    {
        public UnassignTaskCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (this.CommandParameters.Count != 1)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 1, Received: {this.CommandParameters.Count}");
            }

            // [0] - int taskId

            int taskId = this.ParseIntParameter(this.CommandParameters[0], "taskId");
            ITask task = base.Repository.FindTaskById(taskId);

            return UnassignTask(task, taskId);
        }

        private string UnassignTask(ITask task, int taskId)
        {
            if (task is IBug)
            {
                IBug bug = base.Repository.FindBugById(taskId);
                if (bug.Assignee == null)
                {
                    throw new InvalidUserInputException("You can't unassign task that is already unassigned.");
                }
                bug.Assignee = null;
                return $"Task with ID {bug.Id} was unassigned";
            }
            else if (task is IStory)
            {
                IStory story = base.Repository.FindStoryById(taskId);
                if (story.Assignee == null)
                {
                    throw new InvalidUserInputException("You can't unassign task that is already unassigned.");
                }
                story.Assignee = null;
                return $"Task with ID {story.Id} was unassigned.";
            }
            else
            {
                throw new InvalidUserInputException("Feedback can't be unassigned.");
            }
        }
    }
}
