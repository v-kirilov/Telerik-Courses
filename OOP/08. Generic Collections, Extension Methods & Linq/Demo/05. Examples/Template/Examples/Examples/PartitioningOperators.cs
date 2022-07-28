using System;
using System.Linq;

namespace Linq.Examples
{
    public class PartitioningOperators
    {
        // This sample uses Take to get only the first 3 elements of the array.
        public static void Example01()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            //Your implementation goes here
        }

        // This sample uses Skip to get all but the first four elements of the array.
        public static void Example02()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            //Your implementation goes here
        }

        // This sample uses TakeWhile to return elements starting from the beginning of the array until a number is read whose value is not less than 6.
        public static void Example03()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            //Your implementation goes here
        }

        // This sample uses TakeWhile to return elements starting from the beginning of the array until a number is hit that is less than its position in the array.
        public static void Example04()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            //Your implementation goes here
        }

        // This sample uses SkipWhile to get the elements of the array starting from the first element divisible by 3.
        public static void Example05()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            //Your implementation goes here
        }

        // This sample uses SkipWhile to get the elements of the array starting from the first element less than its position.
        public static void Example06()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            //Your implementation goes here
        }
        // Run all examples
        public static void RunAllExamples()
        {
            Example01();
            Example02();
            Example03();
            Example04();
            Example05();
            Example06();
        }
    }
}
