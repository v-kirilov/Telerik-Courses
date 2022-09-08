using System;
using System.Collections.Generic;
using System.Linq;

namespace HashTables.InClassActivity
{
    public class HashTableTasks
    {
        /// <summary>
        /// Counts the number of occurrences of a each word in a collection.
        /// </summary>
        /// <param name="words">A collection of words.</param>
        /// <returns>A dictionary of occurrences by word.</returns>
        public static Dictionary<string, int> CountOccurences(string[] words)
        {
            Dictionary<string, int> countOccurences = new Dictionary<string, int>();
            foreach (string word in words)
            {
                if (!countOccurences.ContainsKey(word))
                {
                    countOccurences[word] = 1;
                }
                else
                {

                    countOccurences[word]++;
                }

            }

            return countOccurences;
            // your implementation
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Return the indices of the first two numbers that sum to a given number.
        /// </summary>
        /// <param name="numbers">Collection of numbers</param>
        /// <param name="sum">Target sum</param>
        /// <returns>An array containing the indices of the first two numbers that produce the target sum.</returns>
        public static int[] TwoSum(int[] numbers, int sum)
        {
            var dict = new Dictionary<int, int>();
            int firstIndex = -1;
            int secondIndex = -1;

            int index = 0;
            for (int i = 0; i < numbers.Length; i++)
            {

                dict[index] = numbers[i];
                index++;

            }
            foreach (var pair in dict)
            {
                int firstNumber = pair.Value;
                int searchedNumber = sum - firstNumber;
                if (dict.ContainsValue(searchedNumber))
                {
                    if (searchedNumber == pair.Value)
                    {
                        continue;
                    }
                       var secondIndexCandidate = dict.FirstOrDefault(x => x.Value == searchedNumber).Key;
                    if (secondIndexCandidate != null)
                    {
                        firstIndex = pair.Key;
                        secondIndex = dict.FirstOrDefault(x => x.Value == searchedNumber).Key;
                        break;
                    }


                }
            }

            int[] output = { firstIndex, secondIndex };
            return output;
        }

        /// <summary>
        /// Counts how many coins are special.
        /// </summary>
        /// <param name="coins">Coins to check.</param>
        /// <param name="catalogue">The catalogue of special coins.</param>
        /// <returns>The count of special coins</returns>
        public static int SpecialCoins(string coins, string catalogue)
        {
            int counter = 0;
            HashSet<string> coinsSet = new HashSet<string>();
            foreach (var ch in catalogue)
            {
                coinsSet.Add(ch.ToString());
            }
            foreach (char ch in coins)
            {
                if (coinsSet.Contains(ch.ToString()))
                {
                    counter++;
                }
            }
            return counter;
        }

        /// <summary>
        /// Determines whether two strings are isomorphic. 
        /// Two strings are considered isomorphic if each character from the first string can map to a character in the seconds string.
        /// </summary>
        /// <param name="s1">The first string.</param>
        /// <param name="s2">The second string.</param>
        /// <returns>True if the two strings are isomorphic; otherwise, false.</returns>
        public static bool AreIsomorphic(string s1, string s2)
        {
            if (s1.Length != s2.Length)
            {
                return false;
            }
            var dict = new Dictionary<char, char>();
            for (int i = 0; i < s1.Length; i++)
            {
                char first = s1[i];
                char second = s2[i];

                if (dict.ContainsKey(first))
                {
                    if (dict[first] == second)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (dict.ContainsValue(second))
                    {
                        return false;
                    }
                    dict[first] = second;
                }

            }

            return true;
        }
    }
}
