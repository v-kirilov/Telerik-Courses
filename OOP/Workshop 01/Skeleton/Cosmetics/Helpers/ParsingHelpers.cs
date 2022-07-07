using System;
using Cosmetics.Models;

namespace Cosmetics.Helpers
{
    public class ParsingHelpers
    {
        public static double TryParseDouble(string valueToParse, string errorMessage)
        {
            try
            {
                return Double.Parse(valueToParse);
            }
            catch
            {
                throw new ArgumentException(errorMessage);
            }
        }

        public static GenderType TryParseGender(string valueToParse, string errorMessage)
        {
            try
            {
                return (GenderType)Enum.Parse(typeof(GenderType), valueToParse);
            }
            catch
            {
                throw new ArgumentException(string.Format(errorMessage, valueToParse));
            }
        }
    }
}
