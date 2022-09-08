using System;

namespace P08_CountOccurrences2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long input = long.Parse(Console.ReadLine());
            int count = 0;
            bool isEight = false;
            int output = CountOccurrences(input, count,isEight);
            Console.WriteLine(output);
        }

        private static int CountOccurrences(long input, int count,bool isEight)
        {
            if (input == 0)
            {
                return count;
            }
            long seven = input % 10;
            if (seven == 8)
            {
                if (isEight)
                {
                    count++;
                }
                count++;
                isEight = true;
            }else
            {
                isEight = false;
            }
            return CountOccurrences(input / 10, count,isEight);
        }
    }
}
