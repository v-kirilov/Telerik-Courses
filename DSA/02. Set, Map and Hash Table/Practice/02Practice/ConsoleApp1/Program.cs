using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{

    internal class Program
    {
        internal class Item
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Type { get; set; }

            public Item(string name, decimal price, string type)
            {
                Name = name;
                Price = price;
                Type = type;
            }
        }
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            List<Item> items = new List<Item>(100);
            while (input != "end")
            {
                string[] cmdArgs = input.Split(' ');
                string command = cmdArgs[0];
                if (command == "add")
                {
                    string productName = cmdArgs[1];
                    decimal price = decimal.Parse(cmdArgs[2]);
                    string type = cmdArgs[3];
                    if (items.Any(x => x.Name == productName))
                    {
                        Console.WriteLine($"Error: Item {productName} already exists");
                        //input = Console.ReadLine();
                        //continue;
                    }
                    else
                    {
                        Item newProdcut = new Item(productName, price, type);
                        items.Add(newProdcut);
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
                        if (!items.Any(x => x.Type == typeToFilter))
                        {
                            Console.WriteLine($"Error: Type {typeToFilter} does not exist");
                            input = Console.ReadLine();
                            continue;
                        }
                        List<Item> newList = items.Where(x => x.Type == typeToFilter).OrderBy(y => y.Price).ThenBy(z => z.Name).ThenBy(fr => fr.Type).Take(10).ToList();

                        Print(newList);

                    }
                    else if (cmdArgs.Length == 7)
                    {
                        //{ MIN_PRICE} to { MAX_PRICE}
                        decimal minPrice = decimal.Parse(cmdArgs[4]);
                        decimal maxPrice = decimal.Parse(cmdArgs[6]);

                        List<Item> newList = items.Where(x => x.Price >= minPrice).Where(y => y.Price <= maxPrice).OrderBy(h => h.Price).ThenBy(g => g.Name).ThenBy(k => k.Type).Take(10).ToList();

                        Print(newList);

                    }
                    else if (cmdArgs[3] == "from")
                    {
                        decimal minPrice = decimal.Parse(cmdArgs[4]);
                        List<Item> newList = items.Where(x => x.Price >= minPrice).OrderBy(h => h.Price).ThenBy(g => g.Name).ThenBy(k => k.Type).Take(10).ToList();

                        Print(newList);
                    }
                    else if (cmdArgs[3] == "to")
                    {
                        decimal maxPrice = decimal.Parse(cmdArgs[4]);
                        List<Item> newList = items.Where(x => x.Price <= maxPrice).OrderBy(h => h.Price).ThenBy(g => g.Name).ThenBy(k => k.Type).Take(10).ToList();

                        Print(newList);
                    }
                }

                input = Console.ReadLine();
            }
        }
        static void Print(List<Item> newList)
        {
            Console.Write("Ok: ");
            List<string> printString = new List<string>();
            foreach (var item in newList)
            {
                string toAdd = item.Name;

                toAdd += $"({item.Price:f2})";

                printString.Add(toAdd);
                toAdd = string.Empty;
            }
            Console.Write(string.Join(", ", printString));
            Console.WriteLine();
            return;
        }
    }
}
