using System;
using System.Numerics;
using System.Diagnostics;

namespace P04_Zig_Zag
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(' ');

            int n = int.Parse(input[0]);
            int m = int.Parse(input[1]);

            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();


            //int[,] matrix = new int[n, m];
            long sum = 0;
            long number = 0;

            //matrix[0, 0] = 1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    number = 0;
                    //matrix[i, j] = matrix[i, j - 1] + 3;

                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                        {
                            number = (i + j) * 3 + 1;
                            if (i > 0 && i < n-1 && j > 0 && j < m-1)
                            {
                                sum += number * 2;
                            }
                            else
                            {
                                sum += number;
                            }
                        }
                    }
                    else if (i % 2 != 0)
                    {
                        if (j % 2 != 0)
                        {
                            number = (i + j) * 3 + 1;
                            if (i > 0 && i < n-1 && j > 0 && j < m-1)
                            {
                                sum += number * 2;

                            }
                            else
                            {
                                sum += number;
                            }
                        }

                    }
                }
            }


            Console.WriteLine(sum);
        }
    }
}
