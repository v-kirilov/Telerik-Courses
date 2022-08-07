using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Bug;

namespace Task_Management.Commands
{
    public class ChangeBugCommand : BaseCommand
    {
        public ChangeBugCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (this.CommandParameters.Count != 3)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 3, Received: {this.CommandParameters.Count}");
            }

            // [0] - int BugId
            // [1] - string type of change - Priority, Severity, Status
            // [2] - string value

            int bugId = this.ParseIntParameter(this.CommandParameters[0], "BugID");
            IBug bug = base.Repository.FindBugById(bugId);

            string typeOfChange = this.CommandParameters[1];
            string value = this.CommandParameters[2];

            return ChangeBugByGivenType(bug, typeOfChange, value);
        }

        public string ChangeBugByGivenType(IBug bug, string typeOfChange, string value)
        {
            switch (typeOfChange.ToLower())
            {
                case "priority":
                    Priority newBugPriority = this.ParseBugPriorityParameter(value, "Priority");
                    var oldPriority = bug.Priority;
                    bug.Priority = newBugPriority;
                    return $"Changed the priority of bug with ID: {bug.Id} from {oldPriority} to {bug.Priority}";

                case "severity":
                    Severity newBugSeverity = this.ParseSeverityParameter(value, "Severity");
                    var oldSeverity = bug.Severity;
                    bug.Severity= newBugSeverity;
                    return $"Changed the severity of bug with ID: {bug.Id} from {oldSeverity} to {bug.Severity}";

                case "status":
                    Status newBugStatus = this.ParseBugStatusParameter(value, "Status");
                    var oldStatus = bug.Status;
                    bug.Status = newBugStatus;
                    return $"Changed the status of bug with ID: {bug.Id} from {oldStatus} to {bug.Status}";


                default:
                    throw new EntityNotFoundException("Wrong type of change. Should be either Priority, Severity or Status");
            }
        }
    }
}
