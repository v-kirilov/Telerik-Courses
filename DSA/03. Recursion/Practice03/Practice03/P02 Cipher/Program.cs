using System;
using System.Collections.Generic;
using System.Linq;

namespace P02_Cipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string message = Console.ReadLine();
            string cipher = Console.ReadLine();
            List<string> list = new List<string>(100);

            for (int i = 0; i < cipher.Length; i++)
            {
                string word = string.Empty;
                if (char.IsLetter(cipher[i]))
                {
                    word = word + cipher[i].ToString();
                    for (int j = i + 1; j < cipher.Length; j++)
                    {
                        if (char.IsDigit(cipher[j]))
                        {
                            word = word + cipher[j].ToString();

                        }
                        else
                        {
                            break;
                        }
                    }

                    list.Add(word);
                }
            }

            List<string> extracted = new List<string>();

            string checker = string.Empty;
            string wordToPass = string.Empty;

            FindWord(message, list, extracted, wordToPass, checker);

            Console.WriteLine();
            extracted.RemoveAll(x => x.Length < 3);

            if (extracted.Count == 0)
            {
                Console.WriteLine(0);

            }
        }
        //1122
        //A1B12C11D2

        //3
        //AADD
        //ABD
        //CDD


        private static void FindWord(string message, List<string> list, List<string> extracted, string word, string checker)  //Message 1122 //Code "A1" 
        {
            if (message.Length == 0)
            {
                if (extracted.Contains(word))
                {
                    return;
                }
                else
                {
                    extracted.Add(word);
                    return;
                }
            }
            for (int i = 0; i < message.Length; i++)
            {
                checker += message[i].ToString(); // Message 1122 //Code "A1" 
                foreach (var item in list)
                {
                    if (checker == item.Substring(1))
                    {
                        message.Remove(0, checker.Length);
                       checker = String.Empty;
                        string newWord = word + item.ElementAt(0);
                        FindWord(message.Substring(1), list, extracted, newWord, checker);
                    }
                }
            }
            //extracted.Add(word);
        }
        //private static void Variations(List<char> input, int num, string write)
        //{
        //    if (num == 0)
        //    {
        //        Console.WriteLine(write);
        //        return;
        //    }
        //    for (int i = 0; i < input.Count; i++)
        //    {
        //        string newWrite = write + input[i];
        //        Variations(input, num - 1, newWrite);
        //    }
        //}

    }
}
