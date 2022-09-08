using System;

namespace P04_TopScores
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int highestPossibleScore = 100;
            int[] unsortedScores = new int[] { 37, 89, 41, 65, 91, 53 };
            int[] sortedScores = SortScores(unsortedScores, highestPossibleScore);
            Console.WriteLine($"sortedScores: {string.Join(", ", sortedScores)}");

            Console.WriteLine();
        }

        private static int[] SortScores(int[] unsortedScores, int highestPossibleScore)
        {
            int[] scoreCount = new int[highestPossibleScore+1];  //Обърнат масив с броя повторения на индекса на числото което имаме , примерно 37 се повтаря само веднъж   //scoreCount[37] = 1;
            int[] sortedScores = new int[unsortedScores.Length]; //Сортирания лист е с размерите на несортирания

            foreach (var score in unsortedScores)
            {
                scoreCount[score]++;  //Пълним обратния масива 
            }
            int startIndex = 0; //Това е индекса за сортирания масив

            for (int i = 0; i <= highestPossibleScore; i++)
            {
                int repeat = scoreCount[i]; // Повторенията на даден score , ако е 0 то няма изобщо да влезем в другия for цикъл
                for (int j = 0; j < repeat; j++)
                {
                    sortedScores[startIndex]=i;
                    startIndex++;
                }
            }
            return sortedScores;
        }
    }
}
