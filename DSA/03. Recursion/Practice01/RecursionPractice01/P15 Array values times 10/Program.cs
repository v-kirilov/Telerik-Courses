using System;
using System.Linq;

namespace P15_Array_values_times_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[] separator = { ',', '.' };
            int[] arr = Console.ReadLine()
                .Split(separator)
                .Select(int.Parse)
                .ToArray();
            int index = int.Parse(Console.ReadLine());
            Console.WriteLine(IsPresent(arr, index));

        }

        private static string IsPresent(int[] arr, int index)
        {
            if (arr.Length <= 1)
            {
                return "false";
            }
            if (index == arr.Length - 1)
            {
                return "false";
            }
            if (10 * arr[index] == arr[index + 1])
            {
                return "true";

            }

            index++;
            return IsPresent(arr, index);
        }
    }
}
