using Agency.Core;
using Agency.Core.Contracts;

namespace Agency
{
    class Startup
    {
        static void Main()
        {
            IRepository repository = new Repository();
            ICommandFactory commandFactory = new CommandFactory(repository);
            IEngine engine = new Engine(commandFactory);
            engine.Start();
        }
    }
}
