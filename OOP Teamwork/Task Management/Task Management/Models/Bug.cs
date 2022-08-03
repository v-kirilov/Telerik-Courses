using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Bug;

namespace Task_Management.Models
{
    public class Bug : Task, IBug
    {
        public const int StepsMinValue = 10;
        public const int StepsMaxValue = 500;
        public const string InvalidStepsError = "Steps must be between {0} and {1} characters.";
        public const string NullOrEmptyStepsError = "Please provide non-empty value for steps.";

        public const Status InitialStatus = Status.Active;

        private string steps;
        private Priority priority;
        private Severity severity;
        private Status status;
        private IMember assignee;

        public Bug(int id, string title, string description, string steps, Priority priority, Severity severity, IMember assignee)
            : base(id, title, description)
        {
            this.Steps = steps;
            this.priority = priority;
            this.severity = severity;
            this.status = Bug.InitialStatus;
            this.assignee = assignee;

            this.AddEventLog($"New bug with ID: {this.Id} and title: {this.Title} was created.");
        }

        public string Steps
        {
            get
            {
                return this.steps;
            }
            private set
            {
                Validator.ValidateStringIsNullOrEmpty(value, Bug.NullOrEmptyStepsError);
                var errorMsg = string.Format(Bug.InvalidStepsError, Bug.StepsMinValue, Bug.StepsMaxValue);
                Validator.ValidateIntRange(value.Length, Bug.StepsMinValue, Bug.StepsMaxValue, errorMsg);

                this.steps = value;
            }
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
                    this.AddEventLog($"Bug with ID: {this.Id} - priority changed from: {this.Priority} to: {value}.");
                    this.priority = value;
                }
                else
                {
                    this.AddEventLog($"Bug with ID: {this.Id} - priority already at {this.Priority}.");
                }
            }
        }

        public Severity Severity
        {
            get
            {
                return this.severity;
            }
            set
            {
                if (value != this.Severity)
                {
                    this.AddEventLog($"Bug with ID: {this.Id} - severity changed from: {this.Severity} to: {value}.");
                    this.severity = value;
                }
                else
                {
                    this.AddEventLog($"Bug with ID: {this.Id} - severity already at {this.Severity}.");
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
                    this.AddEventLog($"Bug with ID: {this.Id} - status changed from: {this.Status} to: {value}.");
                    this.status = value;
                }
                else
                {
                    this.AddEventLog($"Bug with ID: {this.Id} - status already at {this.Status}.");
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
                    this.AddEventLog($"Bug with ID: {this.Id} - assignee changed from: {this.Assignee} to: {value}.");
                    this.assignee = value;
                }
                else 
                {
                    this.AddEventLog($"Bug with ID: {this.Id} - assignee is already: {this.Assignee}.");
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine($" Priority: {this.Priority}");
            sb.AppendLine($" Severity: {this.Severity}");
            sb.AppendLine($" Status: {this.Status}");
            sb.AppendLine($" Assignee: {this.Assignee}");
            sb.AppendLine($"{base.PrintComments()}");

            return sb.ToString().Trim();
        }

        protected override string GetTaskType()
        {
            return "Bug";
        }
    }
}
