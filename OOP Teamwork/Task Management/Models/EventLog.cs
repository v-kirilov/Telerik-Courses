using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Contracts;

namespace Task_Management.Models
{
    public class EventLog : IEventLog
    {
        public const string NullOrEmptyDescriptionError = "Please provide non-empty value for description";
        public EventLog(string description)
        {
            Validator.ValidateStringIsNullOrEmpty(description,EventLog.NullOrEmptyDescriptionError);
            this.Description = description;

            this.Time = DateTime.Now;
        }
        public string Description { get; }

        public DateTime Time { get; }

        public string ViewInfo()
        {
            return $"[{this.Time.ToString("yyyyMMdd|HH:mm:ss.ffff")}]{this.Description}";
        }
    }
}
