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
                if (value==null)
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
    }
}
