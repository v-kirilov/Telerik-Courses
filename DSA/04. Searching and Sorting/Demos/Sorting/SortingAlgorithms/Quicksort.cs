namespace DSA.SortingAlgorithms
{
    public class Quicksort
    {
        public static void Sort(int[] array)
        {
            Sort(array, 0, array.Length - 1);
        }

        private static void Sort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int partitionIndex = Partition(array, left, right);
                Sort(array, left, partitionIndex - 1);
                Sort(array, partitionIndex, right);
            }
        }

        private static int Partition(int[] array, int left, int right)
        {
            int leftIndex = left;
            int rightIndex = right;

            int pivot = array[(left + right) / 2];

            while (leftIndex <= rightIndex)
            {
                // Find index of element larger that pivot
                while (array[leftIndex] < pivot)
                    leftIndex++;

                // Find index of element smaller than pivot
                while (array[rightIndex] > pivot)
                    rightIndex--;

                if (leftIndex <= rightIndex)
                {
                    int temp = array[leftIndex];
                    array[leftIndex] = array[rightIndex];
                    array[rightIndex] = temp;

                    leftIndex++;
                    rightIndex--;
                }
            }

            // At this point, this is the index at which partioning stopped
            return leftIndex;
        }
    }
}
