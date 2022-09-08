using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem_02
{
    internal class Program
    {
        internal class Pokemon
        {
            public string Name { get; set; }
            public int Power { get; set; }
            public int Position { get; set; }
            public string Type { get; set; }

            public Pokemon(string name, string type, int power, int position)
            {
                this.Name = name;
                this.Power = power;
                this.Type = type;
                this.Position = position;
            }
            public override string ToString()
            {
                return $"{this.Name}({this.Power})";
            }
        }
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            List<Pokemon> list = new List<Pokemon>(4);
            //Dictionary<int, Pokemon> list = new Dictionary<int, Pokemon>();
            StringBuilder sb = new StringBuilder();
            while (input != "end")
            {
                string[] cmdArgs = input.Split(' ');
                if (cmdArgs[0] == "add")
                {
                    Pokemon newPokemon = new Pokemon(cmdArgs[1], cmdArgs[2], int.Parse(cmdArgs[3]), int.Parse(cmdArgs[4]));
                    int index = int.Parse(cmdArgs[4]);
                    if (index-1 <= list.Count - 1)
                    {
                        for (int i = index - 1; i < list.Count; i++)
                        {
                            var pokemonToMove = list[i];
                            pokemonToMove.Position++;
                        }
                        list.Insert(index - 1, newPokemon);
                    }
                    else
                    {
                        list.Insert(index - 1, newPokemon);
                    }
                    sb.AppendLine($"Added pokemon {newPokemon.Name} to position {index}");
                }
                else if (cmdArgs[0] == "find")
                {
                    string type = cmdArgs[1];
                    List<Pokemon> newList = list
                        .Where(x => x.Type == type)
                        .OrderBy(z => z.Name)
                        .ThenByDescending(y => y.Power)
                        .Take(5)
                        .ToList();
                    sb.Append($"Type {type}: ");

                    sb.Append(string.Join("; ", newList));

                    sb.AppendLine();
                }
                else if (cmdArgs[0] == "ranklist")
                {
                    int start = int.Parse(cmdArgs[1]);
                    start--;
                    int end = int.Parse(cmdArgs[2]);
                    
                    //var outputList = new List<Pokemon>();
                    for (int i = start; i < end; i++)
                    {
                        sb.Append($"{i+1}. {list[i].Name}({list[i].Power}); ");
                    }
                    sb.Remove(sb.Length-2,2);
                    sb.AppendLine();
                }


                input = Console.ReadLine();
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
