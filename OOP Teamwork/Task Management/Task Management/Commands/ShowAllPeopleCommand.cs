using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Core;

namespace Task_Management.Commands
{
    internal class ShowAllPeopleCommand : BaseCommand
    {
        public ShowAllPeopleCommand(IRepository repository)
            : base(null, repository)
        {

        }

        public override string Execute()
        {
            if (this.Repository.Members.Count != 0)
            {
                var sb = new StringBuilder();

                foreach (var member in this.Repository.Members)
                {
                    sb.AppendLine(member.ToString());
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
