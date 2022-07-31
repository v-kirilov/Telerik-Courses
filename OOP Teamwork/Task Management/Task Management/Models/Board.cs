using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Contracts;

namespace Task_Management.Models
{
    public class Board : IBoard
    {
        public const int NameMinLength = 5;
        public const int NameMaxLength = 10;
        public const string InvalidNameError = "Name must be between 5 and 10 characters long!";

        public string Name { get; }
        private readonly IList<ITask> tasks = new List<ITask>();
        private readonly IList<IEventLog> eventLogs = new List<IEventLog>();

        public Board(string name)
        {
            Validator.ValidateIntRange(name.Length, NameMinLength, NameMaxLength, InvalidNameError);

            this.Name = name;
            //this.tasks = tasks;
            //this.eventLogs = eventLogs;

            EventLog boardCreate = new EventLog($"A new board with namd:{this.Name} has been created!");
            this.eventLogs.Add(boardCreate);
        }

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
