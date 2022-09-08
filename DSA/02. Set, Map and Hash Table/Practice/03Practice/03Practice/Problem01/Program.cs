using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, int> symbols = new Dictionary<char, int>();
            Dictionary<char, int> lowerCase = new Dictionary<char, int>();
            Dictionary<char, int> upperCase = new Dictionary<char, int>();

            string input = Console.ReadLine();
            foreach (var ch in input)
            {
                if (ch >= 97 && ch <= 122)
                {
                    if (lowerCase.ContainsKey(ch))
                    {
                        lowerCase[ch]++;
                    }
                    else
                    {
                        lowerCase[ch] = 1;
                    }
                }
                else if (ch >= 65 && ch <= 90)
                {
                    if (upperCase.ContainsKey(ch))
                    {
                        upperCase[ch]++;
                    }
                    else
                    {
                        upperCase[ch] = 1;
                    }
                }
                else
                {
                    if (symbols.ContainsKey(ch))
                    {
                        symbols[ch]++;
                    }
                    else
                    {
                        symbols[ch] = 1;
                    }
                }
            }


            symbols = symbols
                .OrderByDescending(x => x.Value)
                .ThenBy(y => y.Key)
                .Take(1)
                .ToDictionary(a => a.Key, b => b.Value);
            lowerCase = lowerCase
                .OrderByDescending(x => x.Value)
                .ThenBy(y => y.Key)
                .Take(1)
                .ToDictionary(a => a.Key, b => b.Value);
            upperCase = upperCase
                .OrderByDescending(x => x.Value)
                .ThenBy(y => y.Key)
                .Take(1)
                .ToDictionary(a => a.Key, b => b.Value);

            if (symbols.Count == 0)
            {
                Console.WriteLine($"-");
            }
            else
            {
                foreach (var item in symbols)
                {
                    Console.WriteLine($"{item.Key} {item.Value}");
                }
            }
            if (lowerCase.Count == 0)
            {
                Console.WriteLine($"-");
            }
            else
            {
                foreach (var item in lowerCase)
                {
                    Console.WriteLine($"{item.Key} {item.Value}");
                }
            }
            if (upperCase.Count == 0)
            {
                Console.WriteLine($"-");
            }
            else
            {
                foreach (var item in upperCase)
                {
                    Console.WriteLine($"{item.Key} {item.Value}");
                }
            }
            Console.WriteLine();
        }
    }
}
