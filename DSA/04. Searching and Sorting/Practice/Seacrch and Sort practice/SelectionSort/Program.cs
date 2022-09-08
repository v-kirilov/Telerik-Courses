using System;
using System.Linq;

namespace SelectionSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int[] arr = new int[10];
            //for (int i = 0; i < arr.Length; i++)  //Fills the array with random numbers
            //{
            //    arr[i] = rnd.Next(0,100);
            //}

            arr = arr.Select(s => rnd.Next(0, 100)).ToArray();  // Also fills the array with random numbers but more elegant :)
            Console.WriteLine(string.Join(" ",arr));

            for (int i = 0; i < arr.Length; i++)
            {
                int tempIndex = i;
                for (int j = i+1; j < arr.Length; j++)
                {
                    int newNumber = arr[j];
                    if (arr[j] < arr[tempIndex])
                    {
                        tempIndex = j;
                    }
                }
                int tempNum = arr[i];
                arr[i] = arr[tempIndex];
                arr[tempIndex] = tempNum;
            }
            Console.WriteLine(string.Join(" ", arr));

        }
    }
}
