using System.Collections.Generic;

using HashTables.InClassActivity;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HashTables.Tests
{
    [TestClass]
    public class HashTableTasks_Tests
    {
        [TestMethod]
        [DynamicData(nameof(CountOccurences_TestData), DynamicDataSourceType.Method)]
        public void CountOccurences_Should(string[] test, Dictionary<string, int> expected)
        {
            Dictionary<string, int> actual = HashTableTasks.CountOccurences(test);

            CollectionAssert.AreEquivalent(expected, actual);
        }

        public static IEnumerable<object[]> CountOccurences_TestData()
        {
            yield return new object[]
            {
                new string[] { "gosho", "pesho", "gosho" },
                new Dictionary<string, int>() { { "gosho", 2 }, { "pesho", 1 } }
            };
            yield return new object[]
            {
                new string[] { "c#", "is", "the", "best" },
                new Dictionary<string, int>() { { "c#", 1 }, { "is", 1 }, { "the", 1 }, { "best", 1 }, }
            };
            yield return new object[]
            {
                "one two two three three three four four four four five five five five five".Split(" "),
                new Dictionary<string, int>() { { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 } }
            };
        }

        [TestMethod]
        [DataRow(new int[] { 1, 2, 3 }, 5, new int[] { 1, 2 })]
        [DataRow(new int[] { 2, 0, 1, 3, 2 }, 4, new int[] { 2, 3 })]
        [DataRow(new int[] { 2, 0, 1, 4, 2 }, 4, new int[] { 1, 3 })]
        [DataRow(new int[] { 2, 0, 1, 5, 2 }, 4, new int[] { 0, 4 })]
        [DataRow(new int[] { 1, 2, 3 }, 6, new int[] { -1, -1 })]
        public void TwoSum_Should(int[] test, int sum, int[] expected)
        {
            int[] actual = HashTableTasks.TwoSum(test, sum);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("abcD", "abd", 2)]
        [DataRow("abcDD", "cDfg", 3)]
        [DataRow("aaaCCcccd", "acCe", 8)]
        [DataRow("aaBBbbbc", "Bc", 3)]
        public void SpecialCoins_Should(string coins, string catalogue, int expected)
        {
            int actual = HashTableTasks.SpecialCoins(coins, catalogue);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("egg", "add", true)]
        [DataRow("aab", "xyz", false)]
        [DataRow("paper", "title", true)]
        [DataRow("tidal", "paper", false)]
        public void IsomorphicStrings_Should(string s1, string s2, bool expected)
        {
            bool actual = HashTableTasks.AreIsomorphic(s1, s2);

            Assert.AreEqual(expected, actual);
        }
    }
}
