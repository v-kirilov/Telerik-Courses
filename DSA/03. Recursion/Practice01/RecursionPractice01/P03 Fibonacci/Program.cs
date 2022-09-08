using System;

namespace P03_Fibonacci
{
    internal class Program
    {
        static long[] foundFibs = new long[1000];
        static void Main(string[] args)
        {
            int input = int.Parse(Console.ReadLine());
            Console.WriteLine(Fib(input));
        }

        private static long Fib(int input)
        {

            //Slow way!

            //if (input == 1 || input == 2)
            //{
            //    return 1;
            //}

            //return Fib(input - 1) + Fib(input - 2);

            //Fast Way!
            if (input==0)
            {
                return 0;
            }
            if (foundFibs[input] == 0)
            {
                if (input == 1 || input == 2)
                {
                    foundFibs[input] = 1;
                }
                else
                {
                    foundFibs[input] = Fib(input - 1) + Fib(input - 2);
                }
            }
            return foundFibs[input];
        }
    }
}
