using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands
{
    internal class ShowBoardsActivityCommand : BaseCommand
    {

        public ShowBoardsActivityCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (this.Repository.Teams.Count != 0)
            {
                var sb = new StringBuilder();

                if (this.CommandParameters.Count != 1)
                {
                    throw new InvalidUserInputException($"Invalid number of arguments. Expected: 1, Received: {this.CommandParameters.Count}");
                }

                // Parameters:
                //  [0] - name of board which is unique.

                string boardName = CommandParameters[0];
                IBoard board = this.Repository.FindBoardByName(boardName);
                sb.AppendLine(board.ViewActivity());
                
                return sb.ToString();
            }
            else
            {
                return "There are no teams created!";

            }

        }
    }
}
