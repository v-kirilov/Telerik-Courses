using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Problem_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Dictionary<string, Dictionary<decimal, string>> dict = new Dictionary<string, Dictionary<decimal, string>>();
            while (input != "end")
            {
                string[] cmdArgs = input.Split(' ');
                string command = cmdArgs[0];
                if (command == "add")
                {
                    string productName = cmdArgs[1];
                    decimal price = decimal.Parse(cmdArgs[2]);
                    string type = cmdArgs[3];
                    if (dict.ContainsKey(productName))
                    {
                        Console.WriteLine($"Error: Item {productName} already exists");
                        //input = Console.ReadLine();
                        //continue;
                    }
                    else
                    {
                        dict.Add(productName, new Dictionary<decimal, string> { { price, type } });
                        Console.WriteLine($"Ok: Item {productName} added successfully");
                        //input = Console.ReadLine();
                        //continue;
                    }
                }
                else
                {
                    string filterType = cmdArgs[2];
                    if (filterType == "type")
                    {
                        string typeToFilter = cmdArgs[3];
                        if (!dict.Any(x => x.Value.ContainsValue(typeToFilter)))
                        {
                            Console.WriteLine($"Error: Type {typeToFilter} does not exist");
                            input = Console.ReadLine();
                            continue;
                        }
                        var filteredDictByType = new Dictionary<string, Dictionary<decimal, string>>();

                        filteredDictByType = dict.Where(x => x.Value.ContainsValue(typeToFilter)).Take(10).ToDictionary(x => x.Key, y => y.Value);

                        filteredDictByType = filteredDictByType.OrderBy(x => x.Value.Keys.Any()).ToDictionary(x => x.Key, x => x.Value.OrderBy(y => y.Value).ToDictionary(y => y.Key, y => y.Value));

                       

                        //worldPopulation = worldPopulation.OrderByDescending(x => x.Value.Values.Sum()).ToDictionary(x => x.Key, x => x.Value.OrderByDescending(y => y.Value).ToDictionary(y => y.Key, y => y.Value));


                        //filteredDictByType = filteredDictByType.OrderBy(x => x.Key).Take(10).ToDictionary(x => x.Key, y => y.Value);
                        //filteredDictByType = filteredDictByType.OrderBy(x => x.Value.Select(x => x.Key)).OrderBy(x => x.Key).Take(10).ToDictionary(x => x.Key, y => y.Value);

                        Print(filteredDictByType);

                    }
                    else if (cmdArgs.Length == 7)
                    {
                        //{ MIN_PRICE} to { MAX_PRICE}
                        decimal minPrice = decimal.Parse(cmdArgs[4]);
                        decimal maxPrice = decimal.Parse(cmdArgs[6]);

                        var filteredDictByType = new Dictionary<string, Dictionary<decimal, string>>();

                        filteredDictByType = dict.OrderBy(a => a.Value.Keys.Any()).ToDictionary(b => b.Key, c => c.Value.Where(z => z.Key>=minPrice).Where(j=>j.Key<=maxPrice).ToDictionary(d => d.Key, e => e.Value));

                        filteredDictByType = filteredDictByType.OrderBy(x => x.Value.Keys.Sum()).ToDictionary(a => a.Key, y => y.Value.OrderBy(z => z.Value).ToDictionary(d => d.Key, f => f.Value));


                        filteredDictByType = filteredDictByType.OrderBy(x => x.Key).Take(10).ToDictionary(a =>a.Key, b => b.Value.OrderBy(z=>z.Value).ToDictionary(d => d.Key, e => e.Value));

                        var Final = new Dictionary<string, Dictionary<decimal, string>>();

                        foreach (var pairs in filteredDictByType)
                        {
                            if (pairs.Value.Count>0)
                            {
                                Final[pairs.Key] = pairs.Value;
                            }
                        }


                        Print(Final);

                    }
                    else if (cmdArgs[3] == "from")
                    {
                        decimal minPrice = decimal.Parse(cmdArgs[4]);
                        var filteredDictByType = new Dictionary<string, Dictionary<decimal, string>>();

                        filteredDictByType = dict.OrderBy(x => x.Value.Keys.Any()).ToDictionary(a => a.Key, y => y.Value.Where(z => z.Key >= minPrice).OrderBy(fr=>fr.Key).ToDictionary(d => d.Key, e => e.Value).OrderBy(ko=>ko.Key).ToDictionary(za=>za.Key,zb=>zb.Value));

                        

                        var Final = new Dictionary<string, Dictionary<decimal, string>>();

                        foreach (var pairs in filteredDictByType)
                        {
                            if (pairs.Value.Count > 0)
                            {
                                Final[pairs.Key] = pairs.Value;
                            }
                        }

                        Final = Final.Take(10).ToDictionary(x => x.Key, y => y.Value);


                        Print(Final);
                    }
                    else if (cmdArgs[3] == "to")
                    {
                        decimal maxPrice = decimal.Parse(cmdArgs[4]);
                        var filteredDictByType = new Dictionary<string, Dictionary<decimal, string>>();

                        filteredDictByType = dict.OrderBy(x => x.Value.Keys.Sum()).ToDictionary(a => a.Key, y => y.Value.Where(z => z.Key <= maxPrice).OrderBy(b=>b.Key).ToDictionary(d => d.Key, e => e.Value).OrderBy(h=>h.Key).ToDictionary(fr=>fr.Key,hr=>hr.Value));



                        var Final = new Dictionary<string, Dictionary<decimal, string>>();

                        foreach (var pairs in filteredDictByType)
                        {
                            if (pairs.Value.Count > 0)
                            {
                                Final[pairs.Key] = pairs.Value;
                            }
                        }


                        Print(Final);
                    }
                }



                input = Console.ReadLine();
            }
            //filter by price from { MIN_PRICE} to { MAX_PRICE} – lists the first 10(sorted) items that have ITEM_PRICE in the given range, inclusive;
            //filter by price from { MIN_PRICE} – lists the first 10 items(sorted) that have a greater ITEM_PRICE than the given, inclusive;
            //filter by price to { MAX_PRICE} – lists the first 10 items(sorted) that have a smaller ITEM_PRICE that the given, inclusive;

            //First by ITEM_PRICE, ascending
            //Then by ITEM_NAME, ascending
            //Lastly by ITEM_TYPE, ascending

            //add CowMilk 1.90 dairy
            //add BulgarianYogurt 1.90 dairy
            //add SmartWatch 1111.90 technology
            //add Candy 0.90 food
            //add Lemonade 11.90 drinks
            //add Sweatshirt 121.90 clothes
            //add Pants 49.90 clothes
            //add CowMilk 1.90 dairy
            //add Eggs 2.34 food
            //add Cheese 5.55 dairy
            //filter by type clothes
            //filter by price from 1.00 to 2.00
            //add FreshOrange 1.99 juice
            //add Aloe 2.7 juice
            //filter by price from 1200
            //add Socks 2.90 clothes
            //filter by type fruits
            //add DellXPS13 1700.1234 technology
            //filter by price from 1200
            //filter by price from 1.50
            //filter by price to 2.00
            //filter by type clothes
            //end
        }

        static void Print(Dictionary<string, Dictionary<decimal, string>> filteredDictByType)
        {
            Console.Write("Ok: ");
            List<string> printString = new List<string>();
            foreach (var item in filteredDictByType)
            {
                string toAdd = item.Key;
                foreach (var pricePair in item.Value)
                {
                    toAdd += $"({pricePair.Key:f2})";
                }
                printString.Add(toAdd);
                toAdd = string.Empty;
            }
            Console.Write(string.Join(", ", printString));
            Console.WriteLine();
            return;
        }
    }
}
