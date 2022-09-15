using System;
using System.Linq;

namespace P02_Sequence
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int k = input[0];
            int n = input[1];
            long counter = n - 1;
            long insideCounter = 0;
            long mainNumber = k;
            long searced = k;

            if (n==1)
            {
                Console.WriteLine(k);
                Environment.Exit(0);
            }

            while (counter>0)
            {

                insideCounter++;
                if (insideCounter == 1)
                {
                    searced=mainNumber+1;
                }
                if (insideCounter == 2)
                {
                    searced = 2 * mainNumber + 1;
                }
                if (insideCounter==3)
                {
                    insideCounter = 0;
                    searced = mainNumber + 2;
                    mainNumber ++;
                }
                counter--;
            }

            Console.WriteLine(searced);
        }
    }
}
