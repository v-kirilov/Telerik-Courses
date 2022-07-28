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
            //Your implementation goes here
        }

        // This sample uses All to determine whether an array contains only odd numbers.
        public static void Example02()
        {
            int[] numbers = { 1, 11, 3, 19, 41, 65, 19 };
            //Your implementation goes here
        }

        // Run all examples
        public static void RunAllExamples()
        {
            Example01();
            Example02();
        }
    }
}
