using System;
using System.Collections.Generic;
using System.Linq;

namespace P02_Inflight_Entertainment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int flightLength = 100;
            Random random = new Random();
            int[] movies = new int[20]
                
                .Select(x=>random.Next(10,100))
                .OrderBy(x=>x)
                .ToArray();
            
            CheckMovies(movies, flightLength);
           
        }

        private static void CheckMovies(int[] movies, int flightLengt)
        {
            int middleIndex = movies.Length / 2;
            HashSet<int> set = new HashSet<int>();

            for (int i = middleIndex; i < movies.Length; i++)
            {
                set.Add(movies[i]);
            }


            for (int i = 0; i < middleIndex; i++)
            {
                int remainingTime = flightLengt - movies[i];
                    if (set.Contains(remainingTime))
                    {
                        Console.WriteLine(true);
                        return;
                    }
               
            }
            Console.WriteLine(false);
        }
    }
}
