using System;
using System.Linq;

namespace P02_Forest_Road
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int cols = int.Parse(Console.ReadLine());
            int rows = 2 * cols - 1;
            
            string[,] road = new string[rows, cols];
            
            for (int row = 0; row < road.GetLength(0); row++)
            {
                for (int col = 0; col < road.GetLength(1); col++)
                {
                    if (row == col)
                    {
                        road[row, col] = "*";
                    }
                    else if (row >= road.GetLength(1))
                    {
                        road[row, col] = ".";
                        road[row,2*(road.GetLength(1)-1)-row] = "*";
                    }
                    else
                    {
                        road[row, col] = ".";
                    }
                }
                for (int i = 0; i < road.GetLength(1); i++)
                {
                    Console.Write(road[row, i]);
                }
                Console.WriteLine();

            }
            
        }
    }
}
