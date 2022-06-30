using System;
using System.Linq;
using System.Numerics;

namespace P02_Move
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int startingPoint = int.Parse(Console.ReadLine());
            int[] arr = Console.ReadLine().Split(',').Select(int.Parse).ToArray();
            BigInteger forwardSum = 0;
            BigInteger backwardSum = 0;

            string input = Console.ReadLine();
            while (input != "exit")
            {
                string[] inptArgs = input.Split(' ');
                int steps = int.Parse(inptArgs[0]);
                string direction = inptArgs[1];
                int size = int.Parse(inptArgs[2]);

                if (direction == "forward")
                {
                    for (int i = 0; i < steps; i++)
                    {
                        if (startingPoint + size % arr.Length >= arr.Length)
                        {
                            startingPoint = startingPoint + size % arr.Length - arr.Length;
                            forwardSum += arr[startingPoint];
                        }
                        else
                        {
                            startingPoint += size%arr.Length;
                            forwardSum += arr[startingPoint];
                        } 
                    }
                }
                else
                {
                    for (int i = 0; i < steps; i++)
                    {
                        if (startingPoint - size%arr.Length < 0)
                        {
                            startingPoint = startingPoint - size%arr.Length + arr.Length;
                            backwardSum += arr[startingPoint];
                        }
                        else
                        {
                            startingPoint -= size%arr.Length;
                            backwardSum += arr[startingPoint];
                        }
                    }
                }


                input = Console.ReadLine();
            }

            Console.WriteLine($"Forward: {forwardSum}");


            Console.WriteLine($"Backwards: {backwardSum}");
        }
    }
}
