using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Core;
using Task_Management.Exceptions;

namespace Task_Management.Commands
{
    internal class ShowMembersActivityCommand : BaseCommand
    {
        public ShowMembersActivityCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            if (this.Repository.Members.Count != 0)
            {
                var sb = new StringBuilder();

                if (this.CommandParameters.Count != 1)
                {
                    throw new InvalidUserInputException($"Invalid number of arguments. Expected: 1, Received: {this.CommandParameters.Count}");
                }

                // Parameters:
                //  [0] - name of member which history we would like to view.

                foreach (var member in this.Repository.Members)
                {
                    sb.Append(member.ViewActivity());
                    sb.AppendLine("####################");
                }

                return sb.ToString();
            }
            else
            {
                return "There are no members created!";

            }

        }
    }
}
