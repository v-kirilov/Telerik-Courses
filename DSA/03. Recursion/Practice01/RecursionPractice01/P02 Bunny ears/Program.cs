using System;

namespace P02_Bunny_ears
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
            return 2+Ears(input-1);
        }
    }
}
