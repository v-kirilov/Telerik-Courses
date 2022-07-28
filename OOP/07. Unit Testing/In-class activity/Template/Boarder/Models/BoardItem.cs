using System;
using System.Collections.Generic;
using System.Linq;

namespace Boarder.Models
{
    public abstract class BoardItem
    {
        private const string DateFormat = "dd-MM-yyyy";

        private readonly List<EventLog> history = new List<EventLog>();
        private string title;
        private DateTime dueDate;

        protected BoardItem(string title, DateTime dueDate, Status initialStatus)
        {
            this.EnsureValidTitle(title);
            this.EnsureValidDate(dueDate);

            this.dueDate = dueDate;
            this.title = title;
            this.Status = initialStatus;
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.EnsureValidTitle(value);
                this.AddEventLog($"Title changed from '{this.title}' to '{value}'");

                this.title = value;
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
                this.EnsureValidDate(value);
                this.AddEventLog($"DueDate changed from '{this.dueDate.ToString(DateFormat)}' to '{value.ToString(DateFormat)}'");

                this.dueDate = value;
            }
        }

        public Status Status { get; protected set; }

        public abstract void RevertStatus();

        public abstract void AdvanceStatus();

        public virtual string ViewInfo()
        {
            return $"'{this.Title}', [{this.Status}|{this.DueDate.ToString(DateFormat)}]";
        }

        public string ViewHistory()
        {
            return string.Join(Environment.NewLine, this.history.Select(e => e.ViewInfo()));
        }

        protected void AddEventLog(string desc)
        {
            this.history.Add(new EventLog(desc));
        }

        private void EnsureValidTitle(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Please provide a non-empty name");
            }

            if (value.Length < 5 || value.Length > 30)
            {
                throw new ArgumentException("Please provide a name with length between 5 and 30 chars");
            }
        }

        private void EnsureValidDate(DateTime value)
        {
            if (value < DateTime.Now)
            {
                throw new ArgumentException("DueDate can't be in the past");
            }
        }
    }
}
