using System;

namespace Searching
{
    public class Program
    {
        public static void Main()
        {
            int[] array = new int[] { 3, 1, 4, 1, 5, 9, 2, 6 };
            int value = 9;
            int index;

            //
            // Linear Search
            //
            index = LinearSearch(array, value);
            if (index != -1)
            {
                Console.WriteLine($"Linear Search found {value} at index {index}.");
            }
            else
            {
                Console.WriteLine($"Linear Search couldn't find {value}");
            }

            //
            // Binary Search (Iterative)
            //
            index = BinarySearch_Iterative(array, value);
            if (index != -1)
            {
                Console.WriteLine($"Binary Search (Iterative) found {value} at index {index}.");
            }
            else
            {
                Console.WriteLine($"Binary Search (Iterative) couldn't find {value}");
            }

            //
            // Binary Search (Recursive)
            //
            index = BinarySearch_Recursive(array, value);
            if (index != -1)
            {
                Console.WriteLine($"Binary Search (Recursive) found {value} at index {index}.");
            }
            else
            {
                Console.WriteLine($"Binary Search (Recursive) couldn't find {value}");
            }
        }

        // Summary:
        //     Searches (linearly) an array for a specific value.
        // Parameters:
        //   array: The array to search.
        //   value: The value to search for.
        // Returns:
        //     The index of the specified value in the array, if the value is found; otherwise, -1.
        public static int LinearSearch(int[] array, int value)
        {
            Console.WriteLine("Linear Search");

            for (int i = 0; i < array.Length; i++)
            {
                int current = array[i];

                Console.WriteLine($" Compare {value} with {current} at index {i}");

                if (value == current)
                {
                    Console.WriteLine($" Found {value} at index {i} after {i} comparisons");
                    return i;
                }
            }

            // Not found
            return -1;
        }

        // Summary:
        //     Searches (binary) a sorted array for a specific value.
        // Parameters:
        //   array: The sorted array to search.
        //   value: The value to search for.
        // Returns:
        //     The index of the specified value in the array, if the value is found; otherwise, -1.
        public static int BinarySearch_Iterative(int[] array, int value)
        {
            Console.WriteLine("Binary Search (Iterative)");

            int startIndex = 0;
            int endIndex = array.Length - 1;
            // This variable is not part of the implementation and is used only for demonstration purposes.
            int comparisons = 0;
            while (startIndex <= endIndex)
            {
                int middleIndex = (startIndex + endIndex) / 2;
                int middleValue = array[middleIndex];
                Console.WriteLine($" Compare {value} with {middleValue} at index {middleIndex}");
                comparisons++;
                if (value == middleValue)
                {
                    Console.WriteLine($" Found {value} at index {middleIndex} after {comparisons} comparisons");
                    // Target found, return the index
                    return middleIndex;
                }

                if (value > middleValue)
                {
                    // If the target value is greater than the middle value,
                    // then startIndex should be updated so that the search continues in the right half of the array.
                    startIndex = middleIndex + 1;
                }
                else // value < middleValue
                {
                    // The only remaining case is when the target value is less than the middle value.
                    // Then endIndex should be updated so that the search continues in the left half of the array.
                    endIndex = middleIndex - 1;
                }
            }

            // Not found
            return -1;
        }

        private static void BinarySearch_Recursive_Demo()
        {
            int[] array = new int[] { 3, 1, 4, 1, 5, 9, 2, 6 };
            int value = 9;
            int index = BinarySearch_Recursive(array, value);

            if (index != -1)
            {
                Console.WriteLine($"{value} was found at index {index}.");
            }
            else
            {
                Console.WriteLine($"{value} was not found");
            }
        }

        // Summary:
        //     Searches (binary) a sorted array for a specific value.
        // Parameters:
        //   array: The sorted array to search.
        //   value: The value to search for.
        // Returns:
        //     The index of the specified value in the array, if the value is found; otherwise, -1.
        public static int BinarySearch_Recursive(int[] array, int value)
        {
            Console.WriteLine("Binary Search (Recursive)");
            return BinarySearch_Recursive(array, value, 0, array.Length - 1);
        }
        private static int BinarySearch_Recursive(int[] array, int value, int startIndex, int endIndex)
        {
            if (startIndex > endIndex)
            {
                return -1;
            }

            int middleIndex = (startIndex + endIndex) / 2;
            int middleValue = array[middleIndex];

            Console.WriteLine($" Compare {value} with {middleValue} at index {middleIndex}");
            if (value == middleValue)
            {
                Console.WriteLine($" Found {value} at index {middleIndex}");
                // Target found, return the index
                return middleIndex;
            }

            if (value > middleValue)
            {
                // If the target value is greater than the middle value,
                // then startIndex should be updated so that the search continues in the right half of the array.
                startIndex = middleIndex + 1;
            }
            else // value < middleValue
            {
                // The only remaining case is when the target value is less than the middle value.
                // Then endIndex should be updated so that the search continues in the left half of the array.
                endIndex = middleIndex - 1;
            }

            return BinarySearch_Recursive(array, value, startIndex, endIndex);
        }
    }
}
