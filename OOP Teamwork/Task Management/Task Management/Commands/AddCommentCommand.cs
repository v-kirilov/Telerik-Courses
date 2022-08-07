using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands
{
    public class AddCommentCommand : BaseCommand
    {
        public AddCommentCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (this.CommandParameters.Count != 3)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 3, Received: {this.CommandParameters.Count}");
            }

            // [0] - int taskId
            // [1] - string content
            // [2] - string nameAuthor

            int taskId = this.ParseIntParameter(this.CommandParameters[0], "taskId");
            ITask task = base.Repository.FindTaskById(taskId);

            string content = this.CommandParameters[1];
            string author = this.CommandParameters[2];

            var comment = CreateComment(content, author);
            task.AddComment(comment);

            return $"Comment with author: {author} was added to task with ID: {task.Id}";
        }

        public IComment CreateComment(string content, string author)
        {
            if (!base.Repository.Members.Any(x => x.Name == author))
            {
                throw new InvalidUserInputException("The author of the comment must be a member of the task management system.");
            }

            return new Comment(content, author);
        }
    }
}
