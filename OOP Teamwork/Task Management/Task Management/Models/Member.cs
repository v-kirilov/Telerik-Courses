using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Exceptions;
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

            EventLog createEvent = new EventLog($"New member with name: [{this.Name}] has been created.");
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

        public void AddTask(ITask task)
        {
            if (this.Tasks.Contains(task))
            {
                throw new InvalidUserInputException($"Task already in member: {this.Name} tasks");
            }
            this.tasks.Add(task);
            this.eventLogs.Add(new EventLog($"Task with ID: {task.Id} and title: {task.Title} was assigned to member: {this.Name}"));
        }

        public void RemoveTask(ITask task)
        {
            if (this.Tasks.Contains(task))
            {
                this.tasks.Remove(task);
                this.eventLogs.Add(new EventLog($"Task with ID: {task.Id} and title: {task.Title} was unassigned from member: {this.Name}"));
            }
            throw new InvalidUserInputException($"Task to remove not found in member: {this.Name} tasks");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.Name}");

            return sb.ToString();
        }

        public string ViewActivity()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Curent member: {this.Name}");
            foreach (var ev  in eventLogs)
            {
                sb.AppendLine(ev.ViewInfo());
            }

            return sb.ToString();
        }
    }
}

