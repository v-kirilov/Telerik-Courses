namespace DSA.SortingAlgorithms
{
    public class MergeSort
    {
        public static void Sort(int[] arr)
        {
            Sort(arr, 0, arr.Length - 1);
        }

        private static void Sort(int[] array, int leftIndex, int rightIndex)
        {
            if (leftIndex < rightIndex)
            {
                int middleIndex = (leftIndex + rightIndex) / 2;
                Sort(array, leftIndex, middleIndex);
                Sort(array, middleIndex + 1, rightIndex);
                Merge(array, leftIndex, middleIndex, rightIndex);
            }
        }
        // 0, 1, 2, 3, 4, 5, 6
        private static void Merge(int[] array, int startIndex, int middleIndex, int endIndex)
        {
            int[] mergeArr = new int[endIndex - startIndex + 1];

            int leftIndex = startIndex;
            int rightIndex = middleIndex + 1;
            
            int mergeIndex = 0;

            // main merge
            while (leftIndex <= middleIndex && rightIndex <= endIndex)
            {
                if (array[leftIndex] < array[rightIndex])
                {
                    mergeArr[mergeIndex++] = array[leftIndex++];
                }
                else
                {
                    mergeArr[mergeIndex++] = array[rightIndex++];
                }
            }

            // check for leftovers from left subArray
            while (leftIndex <= middleIndex)
            {
                mergeArr[mergeIndex++] = array[leftIndex++];
            }
            // check for leftovers from right subArray
            while (rightIndex <= endIndex)
            {
                mergeArr[mergeIndex++] = array[rightIndex++];
            }
  
            // copy back to original array
            for (int i = 0; i < mergeArr.Length; i++)
            {
                array[startIndex + i] = mergeArr[i];
            }
        }
    }
}
