using System;
using System.Collections.Generic;

namespace P01_Primes_to_N
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());

            List<int> primeList = new List<int>();
            for (int i = 1; i <= num; i++)
            {
                primeList.Add(i);
            }

            for (int i = 1; i < primeList.Count; i++)
            {

                int candidate = primeList[i];
                int sqrt = (int)Math.Sqrt(primeList[i]);
                for (int j = 2; j <= sqrt; j++)
                {
                    if (candidate % j == 0)
                    {
                        primeList.Remove(primeList[i]);
                        i--;
                        break;
                    }
                }
            }
            Console.WriteLine(String.Join(" ", primeList));
        }
    }
}
