using System;

namespace P09_Power_N
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int baseNum = int.Parse(Console.ReadLine());
            int power = int.Parse(Console.ReadLine());
            Console.WriteLine(PowerN(baseNum,power));
        }

        private static long PowerN(int baseNum, int power)
        {
            if (power == 0)
            {
                return 1;
            }
            return baseNum * (PowerN(baseNum,power-1));
        }
    }
}
