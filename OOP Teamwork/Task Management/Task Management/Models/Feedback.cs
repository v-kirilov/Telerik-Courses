using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Feedback;

namespace Task_Management.Models
{
    public class Feedback : Task, IFeedback
    {
        public const int RatingMinValue = 1;
        public const int RatingMaxValue = 10;
        public const string InvalidRatingError = "Rating must be between {0} and {1}";

        public const Status InitialStatus = Status.New;

        private int rating;
        private Status status;
        public Feedback(int id, string title, string description, int rating)
            : base (id, title, description)
        {
            var errorMsg = string.Format(Feedback.InvalidRatingError, Feedback.RatingMinValue, Feedback.RatingMaxValue);
            Validator.ValidateIntRange(rating, Feedback.RatingMinValue, Feedback.RatingMaxValue, errorMsg);

            this.rating = rating;
            this.status = Feedback.InitialStatus;

            this.AddEventLog($"New feedback with ID: {this.Id} and title: {this.Title} was created.");
        }
        public int Rating
        {
            get
            {
                return this.rating;
            }
            set
            {
                var errorMsg = string.Format(Feedback.InvalidRatingError, Feedback.RatingMinValue, Feedback.RatingMaxValue);
                Validator.ValidateIntRange(value, Feedback.RatingMinValue, Feedback.RatingMaxValue, errorMsg);

                if (value != this.Rating)
                {
                    this.AddEventLog($"Feedback with ID: {this.Id} - rating changed from: {this.Rating} to: {value}.");
                    this.rating = value;
                }
                else
                {
                    this.AddEventLog($"Feedback with ID: {this.Id} - rating already at {this.Status}.");
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
                    this.AddEventLog($"Feedback with ID: {this.Id} - status changed from: {this.Status} to: {value}.");
                    this.status = value;
                }
                else
                {
                    this.AddEventLog($"Feedback with ID: {this.Id} - status already at {this.Status}.");
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine($" Rating: {this.Rating}");
            sb.AppendLine($" Status: {this.Status}");
            sb.AppendLine($"{base.PrintComments()}");

            return sb.ToString().Trim();
        }

        protected override string GetTaskType()
        {
            return "Feedback";
        }
    }
}
