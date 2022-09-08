using System;
using System.Diagnostics;

namespace FibonacciSlow
{
    class Program
    {
        static long recursiveCallsCounter = 0;

        static void Main()
        {
            var watch = new Stopwatch();
            watch.Start();
            int num = 45;
            int fib = FibonacciSlow(num);
            Console.WriteLine($"Fib of {num} = {fib}");
            Console.WriteLine($"Recursive calls: {recursiveCallsCounter}");
            Console.WriteLine($"Time elapsed: {(int)watch.Elapsed.TotalSeconds} seconds ({watch.ElapsedMilliseconds} milliseconds)");
        }

        static int FibonacciSlow(int n)
        {
            recursiveCallsCounter++;

            // Bottom case
            if ((n == 1 ) || (n == 2))
            {
                return 1;
            }

            return FibonacciSlow(n - 1) + FibonacciSlow(n - 2);
        }
    }
}
