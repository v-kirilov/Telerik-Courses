using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands
{
    public class AssignTaskCommand : BaseCommand
    {
        public AssignTaskCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (this.CommandParameters.Count != 2)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 2, Received: {this.CommandParameters.Count}");
            }

            // [0] - int taskId
            // [1] - string nameAssignee

            int taskId = this.ParseIntParameter(this.CommandParameters[0], "taskId");
            ITask task = base.Repository.FindTaskById(taskId);

            string nameAssignee = this.CommandParameters[1];
            IMember assigne = base.Repository.FindMemberByName(nameAssignee);

            return AssignTask(task, taskId, assigne);
        }

        private string AssignTask(ITask task, int taskId, IMember assignee)
        {            
            if (task is IBug)
            {
                IBug bug = base.Repository.FindBugById(taskId);
                if (bug.Assignee != null)
                {
                    throw new InvalidUserInputException("You can't assign task that already has an assignee. Please unnassign the task first.");
                }
                bug.Assignee = assignee;
                return $"Task with ID {bug.Id} assigned to {assignee.Name}.";
            }
            else if (task is IStory)
            {
                IStory story = base.Repository.FindStoryById(taskId);
                if (story.Assignee != null)
                {
                    throw new InvalidUserInputException("You can't assign task that already has an assignee. Please unnassign the task first.");
                }
                story.Assignee = assignee;
                return $"Task with ID {story.Id} assigned to {assignee.Name}.";
            }
            else
            {
                throw new InvalidUserInputException("Feedback can't have an assignee.");
            }
        }
    }
}
