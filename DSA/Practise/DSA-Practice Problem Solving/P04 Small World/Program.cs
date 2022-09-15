using System;
using System.Collections.Generic;
using System.Linq;

namespace P04_Small_World
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int row = input[0];
            int col = input[1];

            int[,] matrix = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                string cmdArgs = Console.ReadLine();
                for (int j = 0; j < cmdArgs.Length; j++)
                {
                    matrix[i, j] = int.Parse(cmdArgs[j].ToString());
                }
            }
            bool[,] visited = new bool[row, col];
            List<int> results = new List<int>();

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (!visited[i, j] && matrix[i, j] == 1)
                    {
                        int currentSurface = GetSurfaceLength(matrix, visited, i, j);
                        results.Add(currentSurface);
                    }
                }
            }

            results = results.OrderByDescending(x => x).ToList();
            foreach (var paths in results)
            {
                Console.WriteLine(paths);
            }
        }

        private static int GetSurfaceLength(int[,] matrix, bool[,] visited, int row, int col)
        {
            int result = 1;
            visited[row, col] = true;
            int currentElement = matrix[row, col];
            //left
            if (col - 1 >= 0 && matrix[row, col - 1] == currentElement && !visited[row, col - 1])
            {
                result += GetSurfaceLength(matrix, visited, row, col - 1);
            }
            //right
            if (col + 1 < matrix.GetLength(1) && matrix[row, col + 1] == currentElement && !visited[row, col + 1])
            {
                result += GetSurfaceLength(matrix, visited, row, col + 1);
            }

            //up
            if (row - 1 >= 0 && matrix[row - 1, col] == currentElement && !visited[row - 1, col])
            {
                result += GetSurfaceLength(matrix, visited, row - 1, col);
            }

            //down
            if (row + 1 < matrix.GetLength(0) && matrix[row + 1, col] == currentElement && !visited[row + 1, col])
            {
                result += GetSurfaceLength(matrix, visited, row + 1, col);
            }

            return result;
        }
    }
}
