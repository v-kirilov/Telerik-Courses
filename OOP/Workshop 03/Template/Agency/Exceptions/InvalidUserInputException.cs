using System;
namespace Agency.Exceptions
{
    public class InvalidUserInputException : ApplicationException
    {
        public InvalidUserInputException(string message)
            : base(message)
        {
        }
        public static void ValidatePrice(double price)
        {
            if (price < 0)
            {
                throw new InvalidUserInputException($"Value of 'costs' must be a positive number. Actual value: {price}.");
            }
        }

        public static void ValidatePassengers(int PassengerCapacityMinValue, int PassengerCapacityMaxValue, int value)
        {
            if (value < 1 || value > 800)
            {

                throw new InvalidUserInputException($"A vehicle with less than {PassengerCapacityMinValue} passengers or more than {PassengerCapacityMaxValue} passengers cannot exist!");
            }
        }






    }
}
