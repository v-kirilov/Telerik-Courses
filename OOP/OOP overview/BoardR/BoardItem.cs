using System;
using System.Collections.Generic;
using System.Text;

namespace BoardR
{
    internal class BoardItem
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }

        StatusOfItem CurrentStatus = StatusOfItem.Open;
        public StatusOfItem Status { get; set; }
        public enum StatusOfItem
        {
            Open , Todo , InProgress , Done , Verified
        }

        public BoardItem(string title, DateTime dueDate)
        {
            if (title.Length < 5 || title.Length > 30)
            {
                throw new ArgumentException("Lenght cannot be les than 5 or more than 30");
            }
            int result = DateTime.Compare(dueDate, DateTime.Now);  //Compare dates
            if (result<=0)
            {
                throw new ArgumentException("Date cannot be earlier or the same as the current date");
            }
            this.Title = title;
            this.DueDate = dueDate;
            this.Status=CurrentStatus;
            
        }

        public void AdvanceStatus()
        {
            if (this.CurrentStatus == StatusOfItem.Verified)
            {
                return;
            }
            this.CurrentStatus++;
            this.Status = CurrentStatus;

        }
        public void RevertStatus()
        {
            if (this.CurrentStatus==StatusOfItem.Open)
            {
                return;
            }
            this.CurrentStatus--;
            this.Status = CurrentStatus;
        }

        public string ViewInfo()
        {
            return $"'{this.Title}', [{this.Status}|{this.DueDate:dd/MM/yyyy}]";
        }


    }
}
