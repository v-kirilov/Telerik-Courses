using System;
using System.Linq;

namespace P03_Matrix_Max_Sum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            long[] fillMatrix = Console.ReadLine()
                .Split(' ')
                .Select(long.Parse)
                .ToArray();
            long[,] matrix = new long[num, fillMatrix.Length];

            for (int i = 0; i < num; i++)
            {

                for (int j = 0; j < fillMatrix.Length; j++)
                {
                    matrix[i, j] = fillMatrix[j];
                }
                if (i == num - 1)
                {
                    break;
                }
                fillMatrix = Console.ReadLine()
                .Split(' ')
                .Select(long.Parse)
                .ToArray();
            }


            int[] coordinates = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();
            long maxSum = long.MinValue;

            int rowStart = 0;
            int colStart = 0;
            int colEnd = 0;

            for (int i = 0; i < coordinates.Length; i += 2)
            {
                long sum = 0;
                rowStart = coordinates[i];
                colEnd = coordinates[i + 1];

                //Get first value !!!

                //if (rowStart < 0)
                //{
                //    sum += matrix[Math.Abs(rowStart) - 1, matrix.GetLength(1) - 1];
                //}
                //else
                //{
                //    sum += matrix[rowStart - 1, 0];
                //}

                //Get row values now.
                if (rowStart < 0)
                {
                    colStart = matrix.GetLength(1) - 1;
                    rowStart = (-1 * rowStart) - 1;  //Get correct index for row
                    while (colStart >= Math.Abs(colEnd) - 1)  //To get correct index of colEnd substract 1.
                    {
                        sum += matrix[rowStart, colStart];
                        colStart--;
                    }
                }
                else
                {
                    colStart = 0;
                    rowStart--; //Get correct index for row
                    while (colStart <= Math.Abs(colEnd) - 1) //To get correct index of colEnd substract 1.
                    {
                        sum += matrix[rowStart, colStart];
                        colStart++;
                    }
                }

                if (colEnd > 0)
                {
                    colEnd--;   //Get correct index for col
                    while (rowStart > 0)
                    {
                        rowStart--;
                        sum += matrix[rowStart, colEnd];
                    }
                }
                else if (colEnd < 0)
                {
                    colEnd = (colEnd * -1) - 1;  //Get correct index for col
                    while (rowStart<matrix.GetLength(0)-1)
                    {
                        rowStart++;
                        sum += matrix[rowStart, colEnd];
                    }
                }


                if (sum > maxSum)
                {
                    maxSum = sum;
                }

            }

            Console.WriteLine(maxSum);


        }
    }
}
