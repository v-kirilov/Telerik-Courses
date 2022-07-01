using System;
using System.Collections.Generic;
using System.Text;

namespace P02_Longest_Block_in_String
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            List<string> list = new List<string>();

            for (int i = 0; i < input.Length; i++)
            {
                list.Add(input[i].ToString());
            }

            string finalRepeating = list[0];
            for (int i = 0; i < list.Count; i++)
            {
                string repeating = list[i];
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[i] == list[j])
                    {
                        repeating += list[j];
                        if (repeating.Length > finalRepeating.Length)
                        {
                            finalRepeating = repeating;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            Console.WriteLine(finalRepeating);
        }


    }
}
