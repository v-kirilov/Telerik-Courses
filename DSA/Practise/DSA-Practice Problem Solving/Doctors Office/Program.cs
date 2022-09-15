using System;
using System.Collections.Generic;
using System.Text;

namespace Doctors_Office
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<String> patients = new List<string>(1000000);
            string input = Console.ReadLine();
            StringBuilder sb = new StringBuilder();

            while (input != "End")
            {
                string[] cmdArgs = input.Split(' ');
                if (cmdArgs[0] == "Append")
                {
                    patients.Add(cmdArgs[1]);
                    Console.WriteLine("OK");
                }
                else if (cmdArgs[0] == "Examine")
                {
                    int toRemove = int.Parse(cmdArgs[1]);
                    if (patients.Count < toRemove)
                    {
                        Console.WriteLine("Error");
                        input = Console.ReadLine();
                        continue;
                    }
                    for (int i = 0; i < toRemove; i++)
                    {
                        sb.Append(patients[0] + " ");
                        patients.RemoveAt(0);
                    }
                    sb.Remove(sb.Length-1,1);
                    Console.WriteLine(sb.ToString());
                    sb.Clear();
                }
                else if (cmdArgs[0] == "Insert")
                {
                    int toInsert = int.Parse(cmdArgs[1]);
                    string name = cmdArgs[2];
                    if (toInsert < 0 || toInsert > patients.Count)
                    {
                        Console.WriteLine("Error");
                        input = Console.ReadLine();
                        continue;
                    }
                    patients.Insert(toInsert, name);
                    Console.WriteLine("OK");
                }
                else if (cmdArgs[0] == "Find")
                {
                    string name = cmdArgs[1];

                    if (!patients.Contains(name))
                    {
                        Console.WriteLine(0);
                    }else
                    {
                        int counter = 0;
                        foreach (var patient in patients)
                        {
                            if (patient == name)
                            {
                                counter++;
                            }
                        }
                        Console.WriteLine(counter);
                    }
                    
                }

                input = Console.ReadLine();
            }
        }
    }
}
