namespace DSA.SortingAlgorithms
{
    public class Selectionsort
    {
        public static void Sort(int[] array)
        {
            // One by one move boundary of unsorted subarray
            for (int i = 0; i < array.Length - 1; i++)
            {
                // Find the index of minimum element in unsorted array
                int minElemIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[minElemIndex])
                    {
                        minElemIndex = j;
                    }
                }

                // Swap the found minimum element with the first element
                int temp = array[minElemIndex];
                array[minElemIndex] = array[i];
                array[i] = temp;
            }
        }

    }
}
