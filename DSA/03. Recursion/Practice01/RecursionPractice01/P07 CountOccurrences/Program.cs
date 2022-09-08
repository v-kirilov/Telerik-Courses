using System;

namespace P07_CountOccurrences
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long input = long.Parse(Console.ReadLine());
            int count = 0;
            int output = CountOccurrences(input, count);
            Console.WriteLine(output);
        }

        private static int CountOccurrences(long input, int count)
        {
            if (input==0)
            {
                return count;
            }
            long seven = input % 10;
            if (seven==7)
            {
                count++;
            }
            return CountOccurrences(input/10, count);
        }
    }
}
