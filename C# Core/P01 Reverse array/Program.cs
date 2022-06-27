using System;
using System.Linq;

namespace P01_Reverse_array
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();
            arr = arr.Reverse().ToArray();
            Console.WriteLine(String.Join(", ",arr));
        }
    }
}
