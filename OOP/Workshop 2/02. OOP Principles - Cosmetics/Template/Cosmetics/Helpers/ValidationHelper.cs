using System;
using System.Collections.Generic;

namespace Cosmetics.Helpers
{
    public class ValidationHelper
    {
        private const string InvalidNumberOfArguments = "Invalid number of arguments. Expected: {0}; received: {1}.";
        private const string InvalidLengthErrorMessage = "{0} should be between {1} and {2} symbols.";
        private const string NegativeNumberErrorMessage = "{0} cannot be negative.";

        public static void ValidateIntRange(int minLength, int maxLength, int actualLength, string field)
        {
            if (actualLength < minLength || actualLength > maxLength)
            {
                throw new ArgumentOutOfRangeException(string.Format(InvalidLengthErrorMessage, field, minLength, maxLength));
            }
        }

        public static void ValidateStringLength(string stringToValidate, int minLength, int maxLength)
        {
            ValidateIntRange(minLength, maxLength, stringToValidate.Length, stringToValidate);
        }

        public static void ValidateArgumentsCount(IList<string> list, int expectedNumberOfParameters)
        {
            if (list.Count != expectedNumberOfParameters)
            {
                throw new ArgumentException(string.Format(InvalidNumberOfArguments, expectedNumberOfParameters, list.Count));
            }

        }

        public static void ValidateNonNegative(decimal value, string field)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(string.Format(NegativeNumberErrorMessage, field));
            }
        }
    }
}
