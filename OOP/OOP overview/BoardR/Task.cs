using System;
using System.Collections.Generic;
using System.Text;

namespace BoardR
{
    internal class Task : BoardItem
    {
        private string assignee;
        public StatusOfItem Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        public string Assignee
        {
            get { return this.assignee; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                if (value.Length < 5 || value.Length > 30)
                {
                    throw new ArgumentException("Lenght cannot be les than 5 or more than 30");
                }
                if (this.assignee != null)
                {
                    EventLog newAssignee = new EventLog($"Assignee changed from {this.assignee} to {value}");
                    this.eventList.Add(newAssignee);
                }
                this.assignee = value;
            }
        }

        public Task(string title, string assignee, DateTime dueDate)
            : base(title, dueDate)
        {
            this.Assignee = assignee;
            this.Status = StatusOfItem.Todo;

            this.eventList.Clear();
            EventLog createTaskEvent = new EventLog($"Created Task: '{title}', [{this.status}|{dueDate:dd-MM-yyyy}]");

            this.eventList.Add(createTaskEvent);

        }

        public override void AdvanceStatus()
        {
            if (this.status == StatusOfItem.Verified)
            {
                EventLog canNotAdvance = new EventLog($"Task status already {this.status}");
                this.eventList.Add(canNotAdvance);
            }
            else
            {
                EventLog canAdvance = new EventLog($"Task changed from {this.status} to {++this.status}");
                this.eventList.Add(canAdvance);
                //this.status++;
            }
        }

        public override void RevertStatus()
        {
            if (this.status == StatusOfItem.Todo)
            {
                EventLog canNotRevert = new EventLog($"Task status already {this.status}");
                this.eventList.Add(canNotRevert);
            }
            else
            {
                EventLog canRevert = new EventLog($"Task changed from {this.status} to {--this.status}");
                this.eventList.Add(canRevert);
            }
        }
    }
}
