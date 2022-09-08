using System;
using System.Linq;

namespace P13._Array_with_6
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
            Console.WriteLine(IsPresent(arr,index));
            
        }

        private static string IsPresent(int[] arr, int index)
        {
            if (arr.Length ==0)
            {
                return "false";
            }
            if (arr[index]==6)
            {
                return "true";
            }else if (index == arr.Length - 1)
            {
                return "false";
            }
            index++;
            return IsPresent(arr, index);
        }
    }
}
