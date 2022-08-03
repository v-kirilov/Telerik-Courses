using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands
{
    public class CreateFeedbackCommand : BaseCommand
    {
        public CreateFeedbackCommand(IList<string> commandParameters, IRepository repository)
            : base (commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (this.CommandParameters.Count != 4)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 4, Received: {this.CommandParameters.Count}");
            }

            // Parameters:
            //  [0] - string title
            //  [1] - string description
            //  [2] - int rating
            //  [3] - Board BoardName = string name

            string title = this.CommandParameters[0];
            string description = this.CommandParameters[1];
            int rating = this.ParseIntParameter(this.CommandParameters[2], "Rating");

            string nameBoard = this.CommandParameters[3];
            IBoard board = base.Repository.FindBoardByName(nameBoard);

            var feedback = base.Repository.CreateFeedback(title, description, rating);
            board.AddTask(feedback);

            return $"Feedback with ID: {feedback.Id} and title: {feedback.Title} was created.";
        }
    }
}
