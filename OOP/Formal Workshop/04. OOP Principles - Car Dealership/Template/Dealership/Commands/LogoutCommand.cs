using Dealership.Core.Contracts;

namespace Dealership.Commands
{
    public class LogoutCommand : BaseCommand
    {
        public LogoutCommand(IRepository repository)
            : base(repository)
        {
        }

        protected override bool RequireLogin
        {
            get { return true; }
        }

        protected override string ExecuteCommand()
        {
            this.Repository.LogOutUser();
            return "You logged out!";
        }
    }
}
