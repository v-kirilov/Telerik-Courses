using System;
using System.Collections.Generic;

namespace InHomeActivity.Map
{
    class Program
    {
        static void Main()
        {
            var result = CountOccurrences(new[] { "js", "c#", "js", "c#", "c++" });
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }

            Console.WriteLine(new String('-', 10));

            var data = new[]
                {
                    new KeyValuePair<string,string>("US", "New York"),
                    new KeyValuePair<string,string>("BG", "Sofia"),
                    new KeyValuePair<string,string>("UK", "London"),
                    new KeyValuePair<string,string>("BG", "Plovdiv"),
                    new KeyValuePair<string,string>("UK", "Manchester"),
                    new KeyValuePair<string,string>("US", "Chicago")
                };

            Dictionary<string, List<string>> newDict = Group(data);

            foreach (var kvp in newDict)
            {
                Console.WriteLine($"Country: {kvp.Key}");
                Console.WriteLine(String.Join(",",kvp.Value));
            }

            Console.WriteLine(new String('-', 10));

        }

        static Dictionary<string, int> CountOccurrences(string[] array)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (string item in array)
            {
                if (!dict.ContainsKey(item))
                {
                    dict.Add(item, 0);
                }

                dict[item]++;
            }

            return dict;
        }

        static Dictionary<string, List<string>> Group(KeyValuePair<string, string>[] data)
        {
            var dict = new Dictionary<string, List<string>>();

            foreach (var kvp in data)
            {
                if (!dict.ContainsKey(kvp.Key))
                {
                    dict[kvp.Key] = new List<string>();
                }
                dict[kvp.Key].Add(kvp.Value);
            }

            return dict;
        }

    }
}
