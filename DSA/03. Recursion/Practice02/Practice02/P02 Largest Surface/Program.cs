using System;
using System.Collections.Generic;
using System.Linq;

namespace P02_Largest_Surface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine()
               .Split(' ')
               .Select(int.Parse)
               .ToArray();
            int rows = arr[0];
            int cols = arr[1];
            int[,] matrix = new int[rows, cols];
            int startRow = 0;
            int startCol = 0;

            for (int i = 0; i < rows; i++)
            {
                int[] valuesArr = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = valuesArr[j];
                }
            }
            List<int> coordinates = new List<int>();
            coordinates.Add(startRow);
            coordinates.Add(startCol);

            int[,] cache = new int[rows, cols];
            CheckMatrix(matrix, cache, coordinates);

        }
        private static void CheckMatrix(int[,] matrix, int[,] cache, List<int> coordinates)
        {
            int currentNumber = matrix[coordinates[0], coordinates[1]];
            cache[coordinates[0], coordinates[1]] = 1;
            CheckNext
            Stack<List<int>> stack = new Stack<List<int>>();
            stack.Push(coordinates);


        }

        private static List<int> MovePosition(int[,] matrix, List<int> coordinates)
        {
            int currentRow = coordinates[0];
            int currentCol = coordinates[1];
            int maxRows = matrix.GetLength(0) - 1;
            int maxCols = matrix.GetLength(1) - 1;
            if (currentRow == maxRows && currentCol == maxCols)  //If we are at the end of the matrix we stop
            {
                return coordinates;
            }
            else if (currentRow != maxRows && currentCol == maxCols) //If we are at the end of the Column we must change row and start at column 0
            {
                coordinates[0]++;
                coordinates[1] = 0;
                return coordinates;
            }
            else  //If we are inside we just continue on 
            {
                coordinates[0]++;
                coordinates[1]++;
                return coordinates;

            }
        }
        //private static string Position(int[,] matrix, int currentRow, int currentCol)
        //{
        //    int maxRows = matrix.GetLength(0) - 1;
        //    int maxCols = matrix.GetLength(1) - 1;
        //    if (currentRow == maxRows && currentCol == maxCols)  //If we are at the end of the matrix we stop
        //    {
        //        return "end";
        //    }
        //    else if (currentRow != maxRows && currentCol == maxCols) //If we are at the end of the Column we must change row and starto at column 0
        //    {
        //        return "endofcol";
        //    }else  //If we are inside we just continue
        //    {
        //        return "continue";
        //    }
        //}

    }
}
