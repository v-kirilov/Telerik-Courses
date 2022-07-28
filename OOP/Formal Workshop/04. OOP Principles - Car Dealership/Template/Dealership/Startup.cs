using Dealership.Core;
using Dealership.Core.Contracts;

namespace Dealership
{
    public class Startup
    {
        public static void Main()
        {
            IRepository repository = new Repository();
            ICommandFactory commandFactory = new CommandFactory(repository);
            IEngine engine = new Core.Engine(commandFactory);
            engine.Start();
        }
    }
}
