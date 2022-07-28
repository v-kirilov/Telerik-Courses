using System;
using System.Linq;

namespace Linq.Examples
{
    public static class Quantifiers
    {
        // This sample uses Any to determine if any of the words in the array contain the substring 'ei'.
        public static void Example01()
        {
            string[] words = { "believe", "relief", "receipt", "field" };

            bool iAfterE = words.Any(w => w.Contains("ei"));

            //DONE fixed typo in writeline
            Console.WriteLine("There is a word in the list that contains 'ei': {0}", iAfterE);
        }

        // This sample uses All to determine whether an array contains only odd numbers.
        public static void Example02()
        {
            int[] numbers = { 1, 11, 3, 19, 41, 65, 19 };

            bool onlyOdd = numbers.All(n => n % 2 == 1);

            Console.WriteLine("The list contains only odd numbers: {0}", onlyOdd);
        }

        // Run all examples
        public static void RunAllExamples()
        {
            Example01();
            Example02();
        }
    }
}
