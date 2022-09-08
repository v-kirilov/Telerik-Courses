using System;
using System.Linq;



namespace SoloLearn
{
    class Program
    {
        const int MATRIX_MIN_SIZE = 2;
        const int MATRIX_MAX_SIZE = 5;
        const int REPEAT_COUNT = 5;

        static void Main(string[] zephyr_koo)
        {
            Enumerable.Range(1, REPEAT_COUNT).ToList().ForEach(n => Run(n));
        }

        static void Run(int nth)
        {
            var matrix = GetRandomSquareMatrix(); // new int[,] { { 1, 2, 9 }, { 5, 3, 8 }, { 4, 6, 7 } };

            Console.WriteLine($"Matrix {nth}");
            Console.WriteLine("========");
            Console.WriteLine();
            DisplayMatrix(matrix);
            Console.WriteLine();
            DisplayPathInfo(matrix);
            Console.WriteLine(Environment.NewLine);
        }

        static int[,] GetRandomSquareMatrix()
        {
            var random = new Random();
            var size = random.Next(MATRIX_MIN_SIZE, MATRIX_MAX_SIZE + 1);
            var matrix = new int[size, size];

            FillUpMatrix(matrix);

            return matrix;
        }

        static void FillUpMatrix(int[,] matrix)
        {
            var size = matrix.GetLength(0);

            var scrambledNumbers = Enumerable.Range(1, matrix.Length)
                                    .OrderBy(n => Guid.NewGuid())
                                    .ToList();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = scrambledNumbers[i * size + j];
                }
            }
        }

        static void DisplayMatrix(int[,] matrix)
        {
            var size = matrix.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(matrix[i, j].ToString().PadLeft(3, ' '));
                }

                Console.WriteLine();
            }
        }

        static void DisplayPathInfo(int[,] matrix)
        {
            var size = matrix.GetLength(0);
            var lookup = Enumerable.Range(1, matrix.Length).ToDictionary(k => k, v => 0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (IsNextNumberAdjacent(i, j, matrix))
                    {
                        lookup[matrix[i, j]]++;
                    }
                }
            }

            int startIndex, length;

            GetLongestPath(string.Join(string.Empty, lookup.Values), out startIndex, out length);

            int startNum = lookup.Keys.ElementAt(startIndex);

            Console.Write($"Longest path ({length + 1}) : ");
            Console.WriteLine(string.Join("-", Enumerable.Range(0, length + 1).Select(n => startNum + n)));
        }

        static bool IsNextNumberAdjacent(int x, int y, int[,] matrix)
        {
            var size = matrix.GetLength(0);

            return matrix[x, y] + 1 == matrix[Math.Max(0, x - 1), y] || // left
                   matrix[x, y] + 1 == matrix[x, Math.Max(0, y - 1)] || // up
                   matrix[x, y] + 1 == matrix[Math.Min(x + 1, size - 1), y] || // right
                   matrix[x, y] + 1 == matrix[x, Math.Min(y + 1, size - 1)];   // down
        }

        static void GetLongestPath(
            string walkPath,
            out int startIndex,
            out int pathLength)
        {
            pathLength = walkPath.Split('0').Select(l => l.Count()).Max();
            startIndex = walkPath.IndexOf(string.Concat(Enumerable.Repeat('1', pathLength)));
        }
    }
}