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

            var first3Numbers = numbers.Take(3);

            Console.WriteLine("First 3 numbers:");
            foreach (var n in first3Numbers)
            {
                Console.WriteLine(n);
            }
        }

        // This sample uses Skip to get all but the first four elements of the array.
        public static void Example03()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var allButFirst4Numbers = numbers.Skip(4);

            Console.WriteLine("All but first 4 numbers:");
            foreach (var n in allButFirst4Numbers)
            {
                Console.WriteLine(n);
            }
        }

        // This sample uses TakeWhile to return elements starting from the beginning of the array until a number is read whose value is not less than 6.
        public static void Example05()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var firstNumbersLessThan6 = numbers.TakeWhile(n => n < 6);

            Console.WriteLine("First numbers less than 6:");
            foreach (var num in firstNumbersLessThan6)
            {
                Console.WriteLine(num);
            }
        }

        // This sample uses TakeWhile to return elements starting from the beginning of the array until a number is hit that is less than its position in the array.
        public static void Example06()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var firstSmallNumbers = numbers.TakeWhile((n, index) => n >= index);

            Console.WriteLine("First numbers not less than their position:");
            foreach (var n in firstSmallNumbers)
            {
                Console.WriteLine(n);
            }
        }

        // This sample uses SkipWhile to get the elements of the array starting from the first element divisible by 3.
        public static void Example07()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            // In the lambda expression, 'n' is the input parameter that identifies each
            // element in the collection in succession. It is is inferred to be
            // of type int because numbers is an int array.
            var allButFirst3Numbers = numbers.SkipWhile(n => n % 3 != 0);

            Console.WriteLine("All elements starting from first element divisible by 3:");
            foreach (var n in allButFirst3Numbers)
            {
                Console.WriteLine(n);
            }
        }

        // This sample uses SkipWhile to get the elements of the array starting from the first element less than its position.
        public static void Example08()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var laterNumbers = numbers.SkipWhile((n, index) => n >= index);

            Console.WriteLine("All elements starting from first element less than its position:");
            foreach (var n in laterNumbers)
            {
                Console.WriteLine(n);
            }
        }
        // Run all examples
        public static void RunAllExamples()
        {
            Example01();
            Example03();
            Example05();
            Example06();
            Example07();
            Example08();
        }
    }
}
