namespace DSA.SortingAlgorithms
{
    public class Bubblesort
    {
        public static void Sort(int[] array)
        {
            bool swapped = true;
            while (swapped)
            {
                swapped = false;
                // The inner loop steps through the array, comparing
                // each element with its neighbor.
                // If two elements are out of order, they are swapped.
                for (int i = 0; i < array.Length - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        int temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }
            }
        }

    }
}
