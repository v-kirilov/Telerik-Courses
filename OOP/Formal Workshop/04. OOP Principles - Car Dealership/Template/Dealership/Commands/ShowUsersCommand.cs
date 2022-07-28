
using Dealership.Core.Contracts;
using Dealership.Exceptions;
using Dealership.Models.Contracts;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Dealership.Commands
{
    public class ShowUsersCommand : BaseCommand
    {

        //ToDo
        public ShowUsersCommand(IRepository repository)
         : base( repository)
        {
        }
        protected override bool RequireLogin
        {
            get { return true; }
        }

        protected override string ExecuteCommand()
        {
            if (this.Repository.Users.Count > 0)
            {
                var sb = new StringBuilder();
                int count = 1;
                if (this.Repository.LoggedUser.Role != Models.Role.Admin)
                {
                    throw new AuthorizationException("You are not an admin!");
                }
               
                sb.AppendLine($"--USERS--");
                foreach (var user in this.Repository.Users)
                {
                    sb.AppendLine(($"{count}. {user}"));
                    count++;
                }

                return sb.ToString().Trim();
            }
            else
            {
                return "There are no users.";
            }
        }
    }
}
