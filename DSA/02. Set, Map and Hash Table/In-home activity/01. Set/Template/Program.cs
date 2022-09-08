using System;
using System.Collections.Generic;

namespace InHomeActivity.Set
{
    class Program
    {
        static void Main()
        {
            var array = new int[] { 1, 2, 3, 4 };
            Console.WriteLine(AreAllElementsUnique(array)); // true

            var list = new List<int>() { 1, 2, 3, 3 };
            Console.WriteLine(AreAllElementsUnique(list)); // false

            Console.WriteLine(AreAllElementsUnique("telerik")); // false

            Console.WriteLine(AreAllElementsUnique("tElerik")); // true

            Console.WriteLine(new String('-',10));

            var uniqueCharacters = Distinct("abbbc");
            Console.WriteLine(string.Join(',', uniqueCharacters)); // a,b,c

            var uniqueNumbers = Distinct(new int[] { 1, 2, 3, 3, 4, 1 });
            Console.WriteLine(string.Join(',', uniqueNumbers)); // 1,2,3,4

            Console.WriteLine(new String('-', 10));


            var union1 = Union("abc", "bce");
            Console.WriteLine(string.Join(',', union1)); // a,b,c,e

            var union2 = Union("abc", "def");
            Console.WriteLine(string.Join(',', union2)); // a,b,c,d,e,f

            var collection1 = new int[] { 1, 5, 9 };
            var collection2 = new int[] { 3, 5, 8 };
            var union3 = Union(collection1, collection2);
            Console.WriteLine(string.Join(',', union3)); // 1,5,9,3,8

            Console.WriteLine(new String('-', 10));

            var intersection1 = Intersect("abc", "bce");
            Console.WriteLine(string.Join(',', intersection1)); // b,c

            var intersection2 = Intersect("abc", "def");
            Console.WriteLine(string.Join(',', intersection2)); // 

            var collection11 = new int[] { 1, 5, 9 };
            var collection22 = new int[] { 3, 5, 8 };
            var intersection3 = Intersect(collection11, collection22);
            Console.WriteLine(string.Join(',', intersection3)); // 5

            Console.WriteLine(new String('-', 10));

            var collection10 = new int[] { 1, 3, 5, 7, 9 };
            var collection20 = new int[] { 2, 3, 4, 6 };
            var difference = Difference(collection10, collection20);
            Console.WriteLine(string.Join(',', difference)); // 1,5,7,9

        }

        static bool AreAllElementsUnique<T>(ICollection<T> collection)
        {
            HashSet<T> newSet = new HashSet<T>(collection);
            
            return newSet.Count==collection.Count;

        }
        static bool AreAllElementsUnique<T>(IEnumerable<T> collection)
        {
            HashSet<T> newSet = new HashSet<T>();
            foreach (var item in collection)
            {
                if (!newSet.Add(item))
                {
                    return false;
                }
            }
            return true;
        }
        static IEnumerable<T> Distinct<T>(IEnumerable<T> collection)
        {
            HashSet<T> newSet = new HashSet<T>(collection);
            return newSet;
        }
        static IEnumerable<T> Union<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
        {
            HashSet<T> setOne = new HashSet<T>(collection1);
            HashSet<T> setTwo = new HashSet<T>(collection1);

            foreach (var item in setTwo)
            {
                setOne.Add(item);
            }
            return setOne;
        }
        static IEnumerable<T> Intersect<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
        {
            var setOne = new HashSet<T>(collection1);
            var setTwo = new HashSet<T>(collection2);
            var intersected = new HashSet<T>();

            foreach (var item in collection1)
            {
                if (setTwo.Contains(item))
                {
                    intersected.Add(item);
                }
            }

            return intersected;
        }
        static IEnumerable<T> Difference<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
        {
            var setOne = new HashSet<T>(collection1);
            var setTwo = new HashSet<T>(collection2);
            var difference = new HashSet<T>();

            foreach (var item in collection1)
            {
                if (!setTwo.Contains(item))
                {
                    difference.Add(item);
                }
            }

            return difference;
        }
    }
}
