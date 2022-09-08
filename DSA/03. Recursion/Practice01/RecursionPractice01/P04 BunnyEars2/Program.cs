using System;

namespace P04_BunnyEars2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int input = int.Parse(Console.ReadLine());
            Console.WriteLine(Ears(input));
        }

        private static int Ears(int input)
        {
            if (input == 0)
            {
                return 0;
            }
            else if (input % 2 == 0)
            {
                return 3 + Ears(input - 1);
            }
            else
            {
                return 2 + Ears(input - 1);
            }
        }
    }
}
