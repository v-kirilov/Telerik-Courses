using System;
using System.Collections.Generic;
using System.Text;

namespace DoublyLinkedListWorkshop.Tests
{
    public class Utils
    {
        public static LinkedList<int> CreateTestList(IEnumerable<int> items)
        {
            LinkedList<int> result = new LinkedList<int>();
            foreach (int item in items)
            {
                result.AddLast(item);
            }
            return result;
        }

        public static bool TestListMatchesExpected(LinkedList<int> list, List<int> expectedValues)
        {
            if (list.Count != expectedValues.Count)
            {
                return false;
            }

            int index = 0;
            foreach (int element in list)
            {
                if (element != expectedValues[index])
                {
                    return false;
                }
                index++;
            }

            return true;
        }
    }
}
