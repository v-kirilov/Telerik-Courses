using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Feedback;

namespace Task_Management.Commands
{
    public class ChangeFeedbackCommand : BaseCommand
    {
        public ChangeFeedbackCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (this.CommandParameters.Count != 3)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 3, Received: {this.CommandParameters.Count}");
            }

            // [0] - int FeedbackId
            // [1] - string type of change - Rating, Status
            // [2] - string value

            int feedbackId = this.ParseIntParameter(this.CommandParameters[0], "StoryID");
            IFeedback feedback = base.Repository.FindFeedbackById(feedbackId);

            string typeOfChange = this.CommandParameters[1];
            string value = this.CommandParameters[2];

            return ChangeFeedbackByGivenType(feedback, typeOfChange, value);
        }

        public string ChangeFeedbackByGivenType(IFeedback feedback, string typeOfChange, string value)
        {
            switch (typeOfChange.ToLower())
            {
                case "rating":
                    int newFeedbackRating = this.ParseIntParameter(value, "Rating");
                    var oldRating = feedback.Rating;
                    feedback.Rating = newFeedbackRating;
                    if (feedback.Rating == oldRating)
                    {
                        return $"Feedback with ID: {feedback.Id} is already: {feedback.Rating}";
                    }
                    return $"Changed the rating of feedback with ID: {feedback.Id} from {oldRating} to {feedback.Rating}";

                case "status":
                    Status newFeedbackStatus = this.ParseFeedbackStatusParameter(value, "Status");
                    var oldStatus = feedback.Status;
                    feedback.Status = newFeedbackStatus;
                    if (feedback.Status == oldStatus)
                    {
                        return $"Feedback with ID: {feedback.Id} is already: {feedback.Status}";
                    }
                    return $"Changed the status of feedback with ID: {feedback.Id} from {oldStatus} to {feedback.Status}";


                default:
                    throw new EntityNotFoundException("Wrong type of change. Should be either Rating or Status");
            }
        }
    }
}
