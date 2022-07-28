using System;

namespace Boarder.Models
{
    public class Issue : BoardItem
    {
        public Issue(string title, string description, DateTime dueDate)
            : base(title, dueDate, Status.Open)
        {
            this.Description = description ?? "No desciption";

            this.AddEventLog($"Created Issue: {this.ViewInfo()}. Description: {this.Description}");
        }

        public string Description { get; }

        public override void AdvanceStatus()
        {
            if (this.Status != Status.Verified)
            {
                this.Status = Status.Verified;
                this.AddEventLog("Issue status set to Verified");
            } 
            else
            {
                this.AddEventLog("Issue status already Verified");
            }
        }

        public override void RevertStatus()
        {
            if (this.Status != Status.Open)
            {
                this.Status = Status.Open;
                this.AddEventLog("Issue status set to Open");
            }
            else
            {
                this.AddEventLog("Issue status already Open");
            }
        }

        public override string ViewInfo()
        {
            return $"Issue: {base.ViewInfo()} Description: {this.Description}";
        }
    }
}
