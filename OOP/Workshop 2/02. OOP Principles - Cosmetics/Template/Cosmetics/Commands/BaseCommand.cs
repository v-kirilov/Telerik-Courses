using Cosmetics.Commands.Contracts;
using Cosmetics.Core.Contracts;
using Cosmetics.Models.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Cosmetics.Commands
{
    public abstract class BaseCommand : ICommand
    {
        protected BaseCommand(IRepository repository)
            : this(new List<string>(), repository)
        {
        }

        protected BaseCommand(IList<string> parameters, IRepository repository)
        {
            this.CommandParameters = parameters;
            this.Repository = repository;
        }

        public abstract string Execute();

        protected IRepository Repository { get; }

        protected IList<string> CommandParameters { get; }

        protected int ParseIntParameter(string value, string parameterName)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            throw new ArgumentException($"Invalid value for {parameterName}. Should be an integer number.");
        }

        protected decimal ParseDecimalParameter(string value, string parameterName)
        {
            if (decimal.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }
            throw new ArgumentException($"Invalid value for {parameterName}. Should be a real number.");
        }

        protected GenderType ParseGenderType(string value)
        {
            if (Enum.TryParse(value, true, out GenderType result))
            {
                return result;
            }
            throw new ArgumentException($"None of the enums in GenderType match the value {value}.");
        }

    }
}
