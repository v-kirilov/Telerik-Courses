using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinearDataStructures.Common;
using LinearDataStructures;

namespace LinearDataStructures.Tests
{
    [TestClass]
    public class LinearStructuresTasksTests
    {
        [TestMethod]
        [DataRow(new[] { 1 }, new[] { 1 }, true)]
        [DataRow(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }, true)]
        [DataRow(new[] { 1 }, new[] { 1, 2 }, false)]
        [DataRow(new[] { 1, 2, 3 }, new[] { 1, 2, 3, 4 }, false)]
        [DataRow(new[] { 2, 1 }, new[] { 1, 2 }, false)]
        public void ListsAreEqual(int[] array1, int[] array2, bool expected)
        {
            // Arrange
            var list1 = new SinglyLinkedList<int>(array1);
            var list2 = new SinglyLinkedList<int>(array2);

            // Act
            var result = LinearStructuresTasks.AreListsEqual(list1, list2);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        [DataRow(new[] { 1 }, 1)]
        [DataRow(new[] { 1, 2 }, 2)]
        [DataRow(new[] { 1, 2, 3 }, 2)]
        [DataRow(new[] { 1, 2, 3, 4 }, 3)]
        [DataRow(new[] { 1, 2, 3, 4, 5 }, 3)]
        public void FindMiddleNode(int[] array, int expected)
        {
            // Arrange
            var list = new SinglyLinkedList<int>(array);

            // Act
            var middle = LinearStructuresTasks.FindMiddleNode(list);

            // Assert
            Assert.AreEqual(middle.Value, expected);
        }

        [TestMethod]
        [DataRow(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }, new[] { 1, 1, 2, 2, 3, 3 })]
        [DataRow(new[] { 1, 2, 3 }, new[] { 1, 2, 3, 4, 5 }, new[] { 1, 1, 2, 2, 3, 3, 4, 5 })]
        [DataRow(new[] { 1, 2, 3 }, new[] { 4, 5, 6 }, new[] { 1, 2, 3, 4, 5, 6 })]
        [DataRow(new[] { 1, 3, 5 }, new[] { 2, 4, 6 }, new[] { 1, 2, 3, 4, 5, 6 })]
        [DataRow(new int[0], new[] { 1, 2, 3 }, new[] { 1, 2, 3 })]
        [DataRow(new[] { 1, 2, 3 }, new int[0], new[] { 1, 2, 3 })]
        [DataRow(new[] { 3, 6 }, new[] { 1, 4 }, new[] { 1, 3, 4, 6 })]
        [DataRow(new[] { 1, 2, 4, 5 }, new[] { 3 }, new[] { 1, 2, 3, 4, 5 })]
        [DataRow(new[] { 1, 5 }, new[] { 2, 3, 4 }, new[] { 1, 2, 3, 4, 5 })]
        public void MergeSortedLists(int[] arr1, int[] arr2, int[] arrExpected)
        {
            // Arrange
            var list1 = new SinglyLinkedList<int>(arr1);
            var list2 = new SinglyLinkedList<int>(arr2);
            var expectedList = new SinglyLinkedList<int>(arrExpected);

            // Act
            var mergedList = LinearStructuresTasks.MergeLists(list1, list2);

            // Assert
            Assert.IsTrue(LinearStructuresTasks.AreListsEqual(mergedList, expectedList));
        }

        [TestMethod]
        [DataRow(new[] { 1, 2, 3 }, new[] { 3, 2, 1 })]
        [DataRow(new[] { 1, 2, 3, 4 }, new[] { 4, 3, 2, 1 })]
        [DataRow(new[] { 1, 2 }, new[] { 2, 1 })]
        [DataRow(new[] { 1 }, new[] { 1 })]
        public void ReverseList(int[] test, int[] expected)
        {
            // Arrange
            var inputList = new SinglyLinkedList<int>(test);
            var expectedList = new SinglyLinkedList<int>(expected);

            // Act
            var reversedList = LinearStructuresTasks.ReverseList(inputList);

            // Assert
            Assert.IsTrue(LinearStructuresTasks.AreListsEqual(reversedList, expectedList));
        }

        [TestMethod]
        [DataRow("(1 + (2 * 3))", true)]
        [DataRow("1 + (2 * 3))", false)]
        [DataRow("(1 + )2 * 3))", false)]
        [DataRow("(1 + (2 * 3)", false)]
        [DataRow("((((5 / 2) + 8) - 1 ) * 3) + 12", true)]
        [DataRow(")12 + 3 + (2 * 8)", false)]
        public void ValidateParentheses(string expression, bool expected)
        {
            // Arrange
            // Act
            bool actual = LinearStructuresTasks.AreValidParentheses(expression);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("abc#d", '#', "abd")]
        [DataRow("abcd##e##", '#', "a")]
        [DataRow("abc####de", '#', "de")]
        [DataRow("teler#ric#k", '#', "telerik")]
        [DataRow("jav##ava###script#####", '#', "js")]
        public void RemoveBackspaces(string sequence, char backspaceChar, string expected)
        {
            // Arrange
            // Act
            string actual = LinearStructuresTasks.RemoveBackspaces(sequence, backspaceChar);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
