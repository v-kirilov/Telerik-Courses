using System;
using System.Linq;

namespace P01_Scrooge_McDuck
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
            int startCol = 0;
            int startRow = 0;

            for (int i = 0; i < rows; i++)
            {
                int[] coinsArr = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();
                for (int j = 0; j < cols; j++)
                {
                    if (coinsArr[j] == 0)
                    {
                        startRow = i;
                        startCol = j;
                    }
                    matrix[i, j] = coinsArr[j];
                }
            }

            int finalCoins = SearchCoins(matrix, startRow, startCol);

            Console.WriteLine(finalCoins);

        }

        private static int SearchCoins(int[,] matrix, int startRow, int startCol)
        {
            string position = Position(matrix, startRow, startCol);
            string move = Move(matrix, startRow, startCol, position);
            if (move == "exit")
            {
                return 0;
            }
            if (move == "left")
            {
                startCol--;
                matrix[startRow, startCol]--;
                return 1 + SearchCoins(matrix, startRow, startCol);
            }
            else if (move == "right")
            {
                startCol++;
                matrix[startRow, startCol]--;
                return 1 + SearchCoins(matrix, startRow, startCol);
            }
            else if (move == "up")
            {
                startRow--;
                matrix[startRow, startCol]--;
                return 1 + SearchCoins(matrix, startRow, startCol);
            }
            else //Moving down  move=="down"
            {
                startRow++;
                matrix[startRow, startCol]--;
                return 1 + SearchCoins(matrix, startRow, startCol);
            }

        }

        private static string Position(int[,] matrix, int currentRow, int currentCol)
        {
            if (currentRow == 0 && currentCol == 0)
            {
                return "topleft";
            }
            else if (currentRow == 0 && currentCol == matrix.GetLength(1) - 1)
            {
                return "topright";

            }
            else if (currentRow == matrix.GetLength(0) - 1 && currentCol == 0)
            {
                return "bottomleft";
            }
            else if (currentRow == matrix.GetLength(0) - 1 && currentCol == matrix.GetLength(1) - 1)
            {
                return "bottomright";
            }
            else if (currentCol == 0 && currentRow != 0 && currentRow != matrix.GetLength(0) - 1)
            {
                return "leftinside";
            }
            else if (currentCol == matrix.GetLength(1) - 1 && currentRow != 0 && currentRow != matrix.GetLength(0) - 1)
            {
                return "rightinside";
            }
            else if (currentRow == 0 && currentCol != 0 && currentCol != matrix.GetLength(1) - 1)
            {
                return "topinside";
            }
            else if (currentRow == matrix.GetLength(0) - 1 && currentCol != 0 && currentCol != matrix.GetLength(1) - 1)
            {
                return "bottominside";
            }
            else
            {
                return "inside";
            }
        }

        private static string Move(int[,] matrix, int currentRow, int CurrentCol, string position)
        {
            if (position == "topleft")
            {
                if (matrix[currentRow + 1, CurrentCol] == 0 && matrix[currentRow, CurrentCol + 1] == 0) //Both are 0 so we exit.
                {
                    return "exit";
                }
                else if (matrix[currentRow, CurrentCol + 1] >= matrix[currentRow + 1, CurrentCol]) //Right is bigger or equal, so we move right
                {
                    return "right";
                }
                else
                {
                    return "down";  //Down is bigger
                }
            }
            else if (position == "topright")
            {
                if (matrix[currentRow + 1, CurrentCol] == 0 && matrix[currentRow, CurrentCol - 1] == 0) //Both are 0 so we exit.
                {
                    return "exit";
                }

                else if (matrix[currentRow, CurrentCol - 1] >= matrix[currentRow + 1, CurrentCol]) //Left is bigger or equal so we move left
                {
                    return "left";
                }
                else
                {
                    return "down"; //Down is bigger
                }
            }
            else if (position == "bottomleft")
            {
                if (matrix[currentRow - 1, CurrentCol] == 0 && matrix[currentRow, CurrentCol + 1] == 0) //Both are 0 so we exit.
                {
                    return "exit";
                }

                else if (matrix[currentRow, CurrentCol + 1] >= matrix[currentRow - 1, CurrentCol]) //Right is bigger or equal so we move right
                {
                    return "right";
                }
                else
                {
                    return "up"; //Up is bigger
                }
            }
            else if (position == "bottomright")
            {
                if (matrix[currentRow, CurrentCol - 1] == 0 && matrix[currentRow - 1, CurrentCol] == 0) //Both are 0 so we exit.
                {
                    return "exit";
                }

                else if (matrix[currentRow, CurrentCol - 1] >= matrix[currentRow - 1, CurrentCol]) //Left is bigger or equal so we move left
                {
                    return "left";
                }
                else
                {
                    return "up"; //Up is bigger
                }
            }
            else if (position == "leftinside")
            {
                int maxRowCoin = Math.Max(matrix[currentRow - 1, CurrentCol], matrix[currentRow + 1, CurrentCol]);
                int maxCoin = Math.Max(maxRowCoin, matrix[currentRow, CurrentCol + 1]);
                if (maxCoin == 0) //All three are 0 so we exit.
                {
                    return "exit";
                }
                else if (matrix[currentRow, CurrentCol + 1] == maxCoin) //Right is bigger or equal so we move right
                {
                    return "right";
                }
                else if (matrix[currentRow - 1, CurrentCol] == maxCoin) //Up is biggest or equal so we move right
                {
                    return "up";
                }
                else
                {
                    return "down"; //Down is biggest or equal
                }
            }
            else if (position == "rightinside")
            {
                int maxRowCoin = Math.Max(matrix[currentRow - 1, CurrentCol], matrix[currentRow + 1, CurrentCol]);
                int maxCoin = Math.Max(maxRowCoin, matrix[currentRow, CurrentCol - 1]);
                if (maxCoin == 0) //All three are 0 so we exit.
                {
                    return "exit";
                }
                else if (matrix[currentRow, CurrentCol - 1] == maxCoin) //Left is bigger or equal so we move right
                {
                    return "left";
                }
                else if (matrix[currentRow - 1, CurrentCol] == maxCoin) //Up is biggest or equal so we move right
                {
                    return "up";
                }
                else
                {
                    return "down"; //Down is biggest or equal
                }
            }
            else if (position == "topinside")
            {
                int maxRowCoin = Math.Max(matrix[currentRow, CurrentCol - 1], matrix[currentRow, CurrentCol + 1]);
                int maxCoin = Math.Max(maxRowCoin, matrix[currentRow + 1, CurrentCol]);
                if (maxCoin == 0) //All three are 0 so we exit.
                {
                    return "exit";
                }
                else if (matrix[currentRow, CurrentCol - 1] == maxCoin) //Left is bigger or equal so we move right
                {
                    return "left";
                }
                else if (matrix[currentRow , CurrentCol+1] == maxCoin) //Right is biggest or equal so we move right
                {
                    return "right";
                }
                else
                {
                    return "down"; //Down is biggest or equal
                }
            }
            else if (position == "bottominside")
            {
                int maxRowCoin = Math.Max(matrix[currentRow, CurrentCol - 1], matrix[currentRow, CurrentCol + 1]);
                int maxCoin = Math.Max(maxRowCoin, matrix[currentRow - 1, CurrentCol]);
                if (maxCoin == 0) //All three are 0 so we exit.
                {
                    return "exit";
                }
                else if (matrix[currentRow, CurrentCol - 1] == maxCoin) //Left is bigger or equal so we move right
                {
                    return "left";
                }
                else if (matrix[currentRow, CurrentCol + 1] == maxCoin) //Right is biggest or equal so we move right
                {
                    return "right";
                }
                else
                {
                    return "up"; //Down is biggest or equal
                }
            }
            else //We are inside!
            {
                int maxRowCoin = Math.Max(matrix[currentRow - 1, CurrentCol], matrix[currentRow + 1, CurrentCol]);
                int maxColCoin = Math.Max(matrix[currentRow, CurrentCol - 1], matrix[currentRow, CurrentCol + 1]);
                int maxCoin = Math.Max(maxRowCoin, maxColCoin);
                if (maxCoin == 0) // All are empty!
                {
                    return "exit";
                }
                if (matrix[currentRow, CurrentCol - 1] == maxCoin) //Left is max coin 
                {
                    return "left";
                }
                else if (matrix[currentRow, CurrentCol + 1] == maxCoin) //Right is max coin 
                {
                    return "right";
                }
                else if (matrix[currentRow - 1, CurrentCol] == maxCoin) //Up is max coin
                {
                    return "up";
                }
                else  //Down is max coin
                {
                    return "down";
                }
            }
        }
    }
}
