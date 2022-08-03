using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands
{
    internal class CreateBoardCommand : BaseCommand
    {
        public CreateBoardCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }


        public override string Execute()
        {
            if (this.CommandParameters.Count != 2)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 2, Received: {this.CommandParameters.Count}");
            }
            // Parameters:
            //[0] - Name of Board
            //  [1] - name of team to assign the board to.
            string boardName = this.CommandParameters[0];
            string teamName = this.CommandParameters[1];
            if (this.Repository.Teams.Any(x => x.Name == teamName))
            {
                
                ITeam team = this.Repository.Teams.FirstOrDefault(x => x.Name == teamName);
                if (team.Boards.Any(x => x.Name == boardName))
                {
                    throw new InvalidUserInputException($"A Board with name:[{boardName}] already exists. Please choose a different name.");
                }

                var board = new Board(boardName);
                team.AddBoard(board);

            }
            return $"Board with name: [{boardName}] has been added to team: [{teamName}]!";
        }
    }
}
