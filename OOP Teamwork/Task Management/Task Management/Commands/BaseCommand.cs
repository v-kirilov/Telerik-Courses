using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Task_Management.Commands.Contracts;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models.Enums.Bug;
using Task_Management.Models.Enums.Story;
using Task_Management.Models.Enums.Feedback;

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

        protected Models.Enums.Bug.Priority ParseBugPriorityParameter(string value, string parameterName)
        {
            if (Enum.TryParse(value, true, out Models.Enums.Bug.Priority result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be either High, Medium or Low.");
        }

        protected Models.Enums.Story.Priority ParseStoryPriorityParameter(string value, string parameterName)
        {
            if (Enum.TryParse(value, true, out Models.Enums.Story.Priority result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be either High, Medium or Low.");
        }

        protected Severity ParseSeverityParameter(string value, string parameterName)
        {
            if (Enum.TryParse(value, true, out Severity result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be either Critical, Major or Minor.");
        }

        protected Size ParseSizeParameter(string value, string parameterName)
        {
            if (Enum.TryParse(value, true, out Size result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be either Large, Medium or Small.");
        }

        protected Models.Enums.Bug.Status ParseBugStatusParameter(string value, string parameterName)
        {
            if (Enum.TryParse(value, true, out Models.Enums.Bug.Status result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be either Active or Fixed.");
        }

        protected Models.Enums.Story.Status ParseStoryStatusParameter(string value, string parameterName)
        {
            if (Enum.TryParse(value, true, out Models.Enums.Story.Status result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be either Not Done, InProgress or Done.");
        }

        protected Models.Enums.Feedback.Status ParseFeedbackStatusParameter(string value, string parameterName)
        {
            if (Enum.TryParse(value, true, out Models.Enums.Feedback.Status result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be either New, Unscheduled, Scheduled, or Done.");
        }
    }
}
