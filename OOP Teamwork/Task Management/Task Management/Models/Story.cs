using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Story;

namespace Task_Management.Models
{
    public class Story : Task, IStory
    {
        public const Status InitialStatus = Status.NotDone;

        private Priority priority;
        private Size size;
        private Status status;
        private IMember assignee;

        public Story(int id, string title, string description, Priority priority, Size size, IMember assignee)
            : base(id, title, description)
        {
            this.priority = priority;
            this.size = size;
            this.status = Story.InitialStatus;
            this.assignee = assignee;

            this.AddEventLog($"New story with ID: {this.Id} and title: {this.Title} was created.");
        }
        public Priority Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                if (value != this.Priority)
                {
                    this.AddEventLog($"Story with ID: {this.Id} - priority changed from: {this.Priority} to: {value}.");
                    this.priority = value;
                }
                else
                {
                    this.AddEventLog($"Story with ID: {this.Id} - priority already at {this.Priority}.");
                }
            }
        }

        public Size Size
        {
            get
            {
                return this.size;
            }
            set
            {
                if (value != this.Size)
                {
                    this.AddEventLog($"Story with ID: {this.Id} - size changed from: {this.Size} to: {value}.");
                    this.size = value;
                }
                else
                {
                    this.AddEventLog($"Story with ID: {this.Id} - size already at {this.Size}.");
                }
            }
        }

        public Status Status
        {
            get
            {
                return this.status;
            }
            set
            {
                if (value != this.Status)
                {
                    this.AddEventLog($"Story with ID: {this.Id} - status changed from: {this.Status} to: {value}.");
                    this.status = value;
                }
                else
                {
                    this.AddEventLog($"Story with ID: {this.Id} - status already at {this.Status}.");
                }
            }
        }

        public IMember Assignee
        {
            get
            {
                return this.Assignee;
            }
            set
            {
                if (value != this.Assignee)
                {
                    this.AddEventLog($"Story with ID: {this.Id} - assignee changed from: {this.Assignee} to: {value}.");
                    this.assignee = value;
                }
                else
                {
                    this.AddEventLog($"Story with ID: {this.Id} - assignee is already: {this.Assignee}.");
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine($" Priority: {this.Priority}");
            sb.AppendLine($" Size: {this.Size}");
            sb.AppendLine($" Status: {this.Status}");
            sb.AppendLine($" Assignee: {this.Assignee}");
            sb.AppendLine($"{base.PrintComments()}");

            return sb.ToString().Trim();
        }

        protected override string GetTaskType()
        {
            return "Story";
        }
    }
}
