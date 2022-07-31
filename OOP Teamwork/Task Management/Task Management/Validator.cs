using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Exceptions;

namespace Task_Management
{
    public static class Validator
    {
        public static void ValidateIntRange(int value, int min, int max, string message)
        {
            if (value < min || value > max)
            {
                throw new InvalidUserInputException(message);
            }
        }
        public static void ValidateDecimalRange(decimal value, decimal min, decimal max, string message)
        {
            if (value < min || value > max)
            {
                throw new InvalidUserInputException(message);
            }
        }







    }


}
