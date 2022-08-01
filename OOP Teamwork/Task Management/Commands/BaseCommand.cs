using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Task_Management.Commands.Contracts;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;

namespace Task_Management.Commands
{
    public abstract class BaseCommand : ICommand
    {

        public BaseCommand(IList<string> commandParameters,IRepository repository)
        {
            this.Repository = repository;
            this.CommandParameters = commandParameters;
        }

        public IRepository Repository { get; }
        public IList<string> CommandParameters { get;}

        //public string Execute()  //IF we need additional condition we can use this method before passing to the Execute() Method
        //{

        //    return this.ExecuteCommand();
        //}

        public abstract string Execute();


        protected int ParseIntParameter(string value, string parameterName)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be an integer number.");
        }

        protected decimal ParseDecimalParameter(string value, string parameterName)
        {
            if (decimal.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be a real number.");
        }

        protected bool ParseBoolParameter(string value, string parameterName)
        {
            if (bool.TryParse(value, out bool result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be either true or false.");
        }
    }
}
