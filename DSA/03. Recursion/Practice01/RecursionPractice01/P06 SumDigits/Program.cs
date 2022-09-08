using System;

namespace P06_SumDigits
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            long sum = 0;
            long output = SumDigits(input, sum);
            Console.WriteLine(output);
        }

        private static long SumDigits(string input,long sum)
        {
            if (input.Length==0)
            {
                return sum;
            }
            int digit = int.Parse(input[0].ToString());
            sum += digit;
            return SumDigits(input.Substring(1),sum);
        }
    }
}
