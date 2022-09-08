using System;
using System.Linq;

namespace P02_ProblemSolving
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine()
                          .Split(' ')
                          .Select(int.Parse)
                          .ToArray();

            int rows = input[0];
            int cols = input[1];
            int[,] field = new int[rows, cols];


            for (int i = 0; i < rows; i++)
            {
                input = Console.ReadLine()
               .Split(' ')
               .Select(int.Parse)
               .ToArray();
                for (int j = 0; j < cols; j++)
                {

                    field[i, j] = input[j];
                }
            }

            int largestSurface = 0;
            bool[,] visited = new bool[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (!visited[i, j])
                    {
                        int currentSurface = GetSurfaceLength(field, visited, i, j);
                        if (currentSurface > largestSurface)
                        {
                            largestSurface = currentSurface;
                        }
                    }
                }
            }
            Console.WriteLine(largestSurface);
        }

        static int GetSurfaceLength(int[,] field, bool[,] visited, int row, int col)
        {
            int result = 1;
            visited[row, col] = true;
            int currentElement = field[row, col];
            //left
            if (col - 1 >= 0 && field[row, col - 1] == currentElement && !visited[row, col - 1])
            {
                result += GetSurfaceLength(field, visited, row, col - 1);
            }

            //right
            if (col + 1 < field.GetLength(1) && field[row, col + 1] == currentElement && !visited[row, col + 1])
            {
                result += GetSurfaceLength(field, visited, row, col + 1);
            }

            //up
            if (row - 1 >= 0 && field[row - 1, col] == currentElement && !visited[row - 1, col])
            {
                result += GetSurfaceLength(field, visited, row - 1, col);
            }

            //down
            if (row + 1 < field.GetLength(0) && field[row + 1, col] == currentElement && !visited[row + 1, col])
            {
                result += GetSurfaceLength(field, visited, row + 1, col);
            }

            return result;
        }
    }
}
