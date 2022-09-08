using System;

namespace P05_Triangle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int input = int.Parse(Console.ReadLine());
            Console.WriteLine(Triangle(input));

        }

        private static long Triangle(int input)
        {
            if (input == 0)
            {
                return 0;
            }
            return input + Triangle(input-1);
        }
    }
}
