using System;

namespace ConstantComplexity
{
    class Program
    {
        static void Main(string[] args)
        {
            // All of these are operations with constant complexity.
            int sum1 = CalculateSum(3, 5);   // This method is with constant complexity a.k.a. O(1)
            int sum2 = CalculateSum(13, 55); // This method is with constant complexity a.k.a. O(1)

            int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int middle = GetMiddleItem(array);  // This method is with constant complexity a.k.a. O(1)
        }

        static int CalculateSum(int a, int b)
        {
            int result = a + b;
            return result;
        }

        static int GetMiddleItem(int[] array)
        {
            if (array != null && array.Length > 0)      // Even though the 'if' statement contains several lines of code,
            {                                           // each of them, individually, has constant O(1) complexity.
                int midIndex = array.Length / 2;        // This makes the complexity of the entire method O(1)
                int midElement = array[midIndex];       //    
                return midElement;                      //
            }                                           // 
            else
            {
                throw new ArgumentException("Array is null or empty");
            }
        }
    }
}
