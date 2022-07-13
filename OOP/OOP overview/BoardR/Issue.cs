using System;
using System.Collections.Generic;
using System.Text;

namespace BoardR
{
    internal class Issue : BoardItem
    {
        private string description;

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
        public string Description
        {
            get { return this.description; }
            set
            {
                if (value == null)
                {
                    value = "No description";
                }
                this.description = value;
            }
        }


        public Issue(string title, string description, DateTime dueDate)
        : base(title, dueDate)
        {
            this.Description = description;

            this.eventList.Clear();
            EventLog createIssueEvent = new EventLog($"Created Issue: '{title}', [{this.status}|{dueDate:dd-MM-yyyy}]. Description: {description}");

            this.eventList.Add(createIssueEvent);
        }

        public override void AdvanceStatus()
        {
            if (this.status == StatusOfItem.Verified)
            {
                EventLog cantAdvance = new EventLog($"Issue status already Verified");
                this.eventList.Add(cantAdvance);
            }
            else
            {
                this.status = StatusOfItem.Verified;
                EventLog canAdvance = new EventLog($"Issue status set to Verified");
                this.eventList.Add(canAdvance);
            }
        }

        public override void RevertStatus()
        {
            if (this.status == StatusOfItem.Open)
            {
                EventLog canNotRevert = new EventLog($"Issue status already {this.status}");
                this.eventList.Add(canNotRevert);
            }
            else
            {
                this.status = StatusOfItem.Open;
                EventLog canRevert = new EventLog($"Issue status set to {this.status}");
                this.eventList.Add(canRevert);
            }
        }
    }
}
