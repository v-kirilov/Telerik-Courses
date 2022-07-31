using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Contracts;

namespace Task_Management.Models
{
    public class Member : IMember
    {
        public const int NameMinLength = 5;
        public const int NameMaxLength = 15;
        public const string InvalidNameError = "Name must be between 5 and 15 characters long!";
        private readonly IList<ITask> tasks = new List<ITask>();
        private readonly IList<IEventLog> eventLogs = new List<IEventLog>();

        public Member(string name)
        {
            Validator.ValidateIntRange(name.Length, NameMinLength, NameMaxLength, InvalidNameError);

            //this.tasks = tasks;
            //this.eventLogs = eventLogs;
            this.Name = name;

            EventLogs createEvent = new EventLogs($"New member with name:{this.name} has been created.");
            this.eventLogs.Add(createEvent);
        }
        public string Name { get; }

        public IList<ITask> Tasks
        {
            get
            {
                return new List<ITask>(tasks);
            }
        }

        public IList<IEventLog> EventLogs
        {
            get
            {
                return new List<IEventLog>(eventLogs);
            }
        }


    }
}
