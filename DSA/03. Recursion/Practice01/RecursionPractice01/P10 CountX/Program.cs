using System;

namespace P10_CountX
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Console.WriteLine(Count(input));
        }

        private static int Count(string input)
        {
            if (input.Length == 0)
            {
                return 0;
            }
            if (input[0]=='x')
            {
                return 1 + Count(input.Substring(1));
            }
            return Count(input.Substring(1));
        }
    }
}
