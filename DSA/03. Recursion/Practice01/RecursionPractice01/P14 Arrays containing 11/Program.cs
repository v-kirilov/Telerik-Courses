using System;
using System.Linq;

namespace P14_Arrays_containing_11
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

        private static int IsPresent(int[] arr, int index)
        {
            if (arr.Length == 0)
            {
                return 0;
            }
            if (arr[index] == 11)
            {
                if (index == arr.Length - 1)
                {
                    return 1;
                }
                index++;
                return 1+IsPresent(arr,index);
            }
            else if (index == arr.Length - 1)
            {
                return 0;
            }
            index++;
            return IsPresent(arr, index);
        }
    }
}
