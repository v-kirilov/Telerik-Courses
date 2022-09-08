using DSA.SortingAlgorithms;
using System;
using System.Diagnostics;

namespace DSA
{
	class Program
    {
        static void Main()
        {
            int arrayLength = 100; // Change this variable and see how the sorting algorithms behave
            int[] originalArray = ArrayHelper.GenerateRandomArray(arrayLength);
            int[] arrayToSort;
            TimeSpan time;

            // System.Array.Sort()
            arrayToSort = ArrayHelper.DuplicateArray(originalArray);
            time = Measure(() => Array.Sort(arrayToSort));
            Console.WriteLine($"    Array.Sort: {time}");

            // Quick sort
            arrayToSort = ArrayHelper.DuplicateArray(originalArray);
            time = Measure(() => Quicksort.Sort(arrayToSort));
            Console.WriteLine($"    Quicksort.Sort: {time}");

            // Merge sort
            arrayToSort = ArrayHelper.DuplicateArray(originalArray);
            time = Measure(() => MergeSort.Sort(arrayToSort));
            Console.WriteLine($"    Mergesort.Sort: {time}");

            // Selection sort
            arrayToSort = ArrayHelper.DuplicateArray(originalArray);
            time = Measure(() => Selectionsort.Sort(arrayToSort));
            Console.WriteLine($"    Selectionsort.Sort: {time}");

            // Bubble sort
            arrayToSort = ArrayHelper.DuplicateArray(originalArray);
            time = Measure(() => Bubblesort.Sort(arrayToSort));
            Console.WriteLine($"    Bubblesort.Sort: {time}");
        }

        static TimeSpan Measure(Action sortingAlgorithm)
        {
            var timer = new Stopwatch();
            timer.Start();
            sortingAlgorithm();
            timer.Stop();
            return timer.Elapsed;
        }
    }
}
