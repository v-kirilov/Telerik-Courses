using System;
using System.Linq;

namespace BubbleSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[] arr = new int[10].Select(x=>random.Next(0,100)).ToArray();
            bool isSorted = false;
            while (!isSorted)
            {
                isSorted = true;
                for (int i = 0; i < arr.Length-1; i++)
                {
                    
                    if (arr[i+1] < arr[i])
                    {
                        int temp = arr[i];
                        arr[i] = arr[i+1];
                        arr[i+1] = temp;
                        isSorted = false;
                    }
                }

            }
            Console.WriteLine(String.Join(" ",arr));
        }
    }
}
