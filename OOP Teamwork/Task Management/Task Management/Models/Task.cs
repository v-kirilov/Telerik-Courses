using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Models.Contracts;

namespace Task_Management.Models
{
    public abstract class Task : ITask
    {
        private const int TitleMinLength = 10;
        private const int TitleMaxLength = 50;
        private const string InvalidTitleError = "Title must be between {0} and {1} characters.";
        private const string NullOrEmptyTitleError = "Please provide non-empty value for title.";

        private const int DescriptionMinLength = 10;
        private const int DescriptionMaxLength = 500;
        private const string InvalidDescriptionError = "Description must be between {0} and {1} characters.";
        private const string NullOrEmptyDescriptionError = "Please provide non-empty value for description.";

        private const string NoCommentsHeader = "  No Comments";
        private const string CommentsHeader = "  Comments";
        private const string CommentsFooter = "  Comments End";
        private const string CommentDelimiter = "--------------------";

        private string title;
        private string description;
        protected readonly IList<IEventLog> history;
        protected readonly IList<IComment> comments;

        protected Task(int id, string title, string description)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;

            this.history = new List<IEventLog>();
            this.comments = new List<IComment>();
        }

        public int Id { get; }

        public virtual string Title
        {
            get
            {
                return this.title;
            }
            private set
            {
                Validator.ValidateStringIsNullOrEmpty(value, Task.NullOrEmptyTitleError);
                var errorMsg = string.Format(Task.InvalidTitleError, Task.TitleMinLength, Task.TitleMaxLength);
                Validator.ValidateIntRange(value.Length, Task.TitleMinLength, Task.TitleMaxLength, errorMsg);

                this.title = value;
            }
        }

        public virtual string Description
        {
            get
            {
                return this.description;
            }
            private set
            {
                Validator.ValidateStringIsNullOrEmpty(value, Task.NullOrEmptyDescriptionError);
                var errorMsg = string.Format(Task.InvalidDescriptionError, Task.DescriptionMinLength, Task.DescriptionMaxLength);
                Validator.ValidateIntRange(value.Length, Task.DescriptionMinLength, Task.DescriptionMaxLength, errorMsg);

                this.description = value;
            }
        }

        public IList<IEventLog> History
        {
            get
            {
                return new List<IEventLog>(this.history);
            }
        }

        public IList<IComment> Comments
        {
            get
            {
                return new List<IComment>(this.comments);
            }
        }

        public void AddComment(IComment comment)
        {
            this.comments.Add(comment);
        }

        public void RemoveComment(IComment comment)
        {
            this.comments.Remove(comment);
        }

        protected void AddEventLog(string desc)
        {
            this.history.Add(new EventLog(desc));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Task type: {this.GetTaskType()}");
            sb.AppendLine($" ID: {this.Id}");
            sb.AppendLine($" Title {this.Title}");
            sb.AppendLine($" Description: {this.Description}");

            return sb.ToString().Trim();
        }

        protected abstract string GetTaskType();

        protected string PrintComments()
        {
            var sb = new StringBuilder();

            if (this.Comments.Count <= 0)
            {
                sb.AppendLine($"{Task.NoCommentsHeader}");
            }
            else
            {
                sb.AppendLine(Task.CommentsHeader);

                foreach (var comment in this.Comments)
                {
                    sb.AppendLine(comment.ToString());
                    sb.AppendLine(Task.CommentDelimiter);
                }

                sb.AppendLine(Task.CommentsFooter);
            }
            return sb.ToString().Trim();
        }

        public string ViewHistory()
        {
            return string.Join(Environment.NewLine, this.history.Select(e => e.ViewInfo()));
        }
    }
}
