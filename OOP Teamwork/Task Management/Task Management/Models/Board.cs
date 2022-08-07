using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Exceptions;
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

        public void AddTask(ITask task)
        {
            if (this.Tasks.Contains(task))
            {
                throw new InvalidUserInputException($"Task already in board: {this.Name} tasks");
            }
            this.tasks.Add(task);
            this.eventLogs.Add(new EventLog($"Task with ID: {task.Id} and title: {task.Title} was added to board: {this.Name}"));
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

        public string ViewActivity()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Curent member: {this.Name}");
            foreach (var ev in eventLogs)
            {
                sb.AppendLine(ev.ViewInfo());
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string nameRow = $"║ Board name:[{this.Name}] ║";
            string topRow = "╔";
            topRow += new string('═', nameRow.Length - 2);
            topRow += '╗';

            string botRow = "╚";
            botRow += new string('═', nameRow.Length - 2);
            botRow += '╝';

            sb.AppendLine(topRow);
            sb.AppendLine(nameRow);
            sb.AppendLine(botRow);

            return sb.ToString();
        }
    }
}
