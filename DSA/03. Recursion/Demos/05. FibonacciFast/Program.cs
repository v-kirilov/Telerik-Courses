using System;
using System.Diagnostics;

namespace FibonacciFast
{
    class Program
    {
        static long recursiveCallsCounter = 0;
        static int[] fib = new int[1000];

        static void Main(string[] args)
        {
            var watch = new Stopwatch();
            watch.Start();
            int num = 45;
            int fib = Fibonacci(num);
            Console.WriteLine($"Fib of {num} = {fib}");
            Console.WriteLine($"Recursive calls: {recursiveCallsCounter}");
            Console.WriteLine($"Time elapsed: {watch.ElapsedMilliseconds} milliseconds");
        }

        static int Fibonacci(int n)
        {
            recursiveCallsCounter++;

            // Check if the current num is calculated
            if (fib[n] == 0)
            {
                // The value is still not calculated => to be calcualted now
                // Bottom case
                if ((n == 1) || (n == 2))
                {
                    fib[n] = 1;
                }
                else
                {
                    // Calculate value and store it
                    fib[n] = Fibonacci(n - 1) + Fibonacci(n - 2);
                }
            }
            return fib[n];
        }
    }
}
