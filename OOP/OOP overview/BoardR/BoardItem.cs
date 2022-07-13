using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BoardR
{
    internal abstract class BoardItem
    {
        private string title;
        protected StatusOfItem status = StatusOfItem.Open;
        private DateTime dueDate;
        protected List<EventLog> eventList = new List<EventLog>();

        private string oldTitle = String.Empty;
        private DateTime oldDate;


        public List<EventLog> EventList
        {
            get
            {
                List<EventLog> copyList = new List<EventLog>(this.eventList); //Encapsulate the list
                return copyList;
            }

        }
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (value.Length < 5 || value.Length > 30)
                {
                    throw new ArgumentException("Lenght cannot be les than 5 or more than 30");
                }
                if (oldTitle != String.Empty)
                {
                    EventLog newEvent = new EventLog($"Title changed from '{this.oldTitle}' to '{value}'");
                    this.eventList.Add(newEvent);
                    this.title = value;
                    this.oldTitle = value;
                } else
                {
                    this.title = value;
                }
            }
        }
        public DateTime DueDate
        {
            get
            {
                return this.dueDate;
            }
            set
            {
                //Compare dates
                if ((value <= DateTime.Now))
                {
                    throw new ArgumentException("Date cannot be earlier or the same as the current date");
                }
                if (this.dueDate != value && this.dueDate != DateTime.MinValue)
                {
                    EventLog newDateEvent = new EventLog($"DueDate changed from '{this.oldDate:dd-MM-yyyy}' to '{value:dd-MM-yyyy}'");
                    this.eventList.Add(newDateEvent);
                }
                this.dueDate = value;
                this.oldDate = value;

            }
        }

        private StatusOfItem Status
        {
            get
            {
                return this.status;
            }
        }


        public BoardItem(string title, DateTime dueDate)
        {

            this.Title = title;
            this.oldTitle = title;
            this.DueDate = dueDate;

            EventLog newTitleEvent = new EventLog($"Item created: '{title}', [{this.status} | {this.dueDate:dd-MM-yyyy}]");
            this.eventList.Add(newTitleEvent);
        }

        public abstract void AdvanceStatus();
       

        public abstract void RevertStatus();
       
        
        public string ViewInfo()
        {
            return $"'{this.Title}', [{this.Status}|{this.DueDate:dd/MM/yyyy}]";
        }

        public string ViewHistory()
        {
            this.eventList.OrderBy(p => p.DateTime);

            StringBuilder sb = new StringBuilder();
            foreach (EventLog eventLog in this.eventList)
            {
                sb.AppendLine(eventLog.ViewInfo());
            }
            return sb.ToString();
        }
    }
}
