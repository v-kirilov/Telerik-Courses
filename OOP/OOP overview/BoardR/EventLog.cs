using System;
using System.Collections.Generic;
using System.Text;

namespace BoardR
{
    internal class EventLog
    {
        private string description;

        public string Description
        {
            get { return description; }
            private set
            {
                this.description = value;
            }
        }

        private DateTime dateTime;

        public DateTime DateTime
        {
            get { return dateTime; }
            private set
            { 
                this.dateTime = value;
            }
        }


        public EventLog(string description)
        {
            if (description == null)
            {
                throw new ArgumentNullException();
            }
            this.Description = description;
            this.DateTime = DateTime.Now;
        }
        public string ViewInfo()
        {
            return $"[{this.dateTime:yyyyMMdd|HH:mm:ss:FFFF}]{this.description}";
        }
    }
}
