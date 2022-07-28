using System;

namespace Boarder.Models
{
    public class Task : BoardItem
    {
        private string assignee;

        public Task(string title, string assignee, DateTime dueDate)
            : base(title, dueDate, Status.Todo)
        {
            this.EnsureValidAssignee(assignee);
            this.assignee = assignee;
            this.AddEventLog($"Created Task: {this.ViewInfo()}");
        }

        public string Assignee
        {
            get
            {
                return this.assignee;
            }
            set
            {
                this.EnsureValidAssignee(value);
                this.AddEventLog($"Assignee changed from {this.assignee} to {value}");
                this.assignee = value;
            }
        }

        public override void AdvanceStatus()
        {
            if (this.Status != Status.Verified)
            {
                var prev = this.Status;
                this.Status++;
                this.AddEventLog($"Task changed from {prev} to {this.Status}");
            }
            else
            {
                this.AddEventLog("Task status already Verified");
            }
        }

        public override void RevertStatus()
        {
            if (this.Status != Status.Todo)
            {
                var prev = this.Status;
                this.Status--;
                this.AddEventLog($"Task changed from {prev} to {this.Status}");
            }
            else
            {
                this.AddEventLog("Task status already Todo");
            }
        }

        public override string ViewInfo()
        {
            return $"Task: {base.ViewInfo()} Assignee: {this.Assignee}";
        }

        private void EnsureValidAssignee(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Please provide a non-null or empty value");
            }

            if (value.Length < 5 || value.Length > 30)
            {
                throw new ArgumentException("Please provide a value in range [5..30]");
            }
        }
    }
}
