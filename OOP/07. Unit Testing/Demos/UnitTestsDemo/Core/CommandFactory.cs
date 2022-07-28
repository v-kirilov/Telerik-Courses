using System;

namespace UnitTestsDemo.Core
{
    public class CommandFactory
    {
        public ICommand Create(string commandName)
        {
            ICommand result;
            switch (commandName)
            {
                case "credit":
                    result = new CreditCommand();
                    break;
                case "debit":
                    result = new DebitCommand();
                    break;
                default:
                    throw new ArgumentException("Command not found");
            };
            return result;
        }
    }
}
