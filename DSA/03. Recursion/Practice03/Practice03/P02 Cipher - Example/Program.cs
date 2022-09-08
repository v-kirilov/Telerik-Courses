using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher
{
    public class Program
    {
        static string message = string.Empty;

        static string cipher = string.Empty;

        static char[] DecipheredMessage;

        static Dictionary<string, char> cipherLibrary = new Dictionary<string, char>();

        static List<string> variations = new List<string>();

        static StringBuilder result = new StringBuilder();

        static void Main()
        {
            message = Console.ReadLine();

            cipher = Console.ReadLine();

            DecipheredMessage = new char[message.Length];

            char symbol = default(char);

            string symbolCipher = string.Empty;

            for (int i = 0; i < cipher.Length; i++)
            {
                if (cipher[i] >= 65 && cipher[i] <= 90)
                {
                    symbol = cipher[i];

                    for (int j = i + 1; j < cipher.Length; j++)
                    {
                        if (cipher[j] >= 48 && cipher[j] <= 57)
                        {
                            symbolCipher = string.Concat(symbolCipher, cipher[j]);
                        }
                        else
                        {
                            break;
                        }
                    }

                    cipherLibrary.Add(symbolCipher, symbol);

                    symbolCipher = string.Empty;
                }
            }

            DecipherMessage();

            variations.Sort();

            result.AppendLine(variations.Count.ToString());

            string formattedMessage = string.Empty;

            foreach (var item in variations)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    if (item[i] != default(char))
                    {
                        formattedMessage += item[i];
                    }
                }

                result.AppendLine(formattedMessage);

                formattedMessage = string.Empty;
            }

            Console.WriteLine(result.ToString().Trim());
        }

        private static void DecipherMessage(int index = 0)
        {
            if (index >= message.Length)
            {
                variations.Add(new string(DecipheredMessage));

                return;
            }

            string messageToDecipher = message.Substring(index);

            foreach (KeyValuePair<string, char> kvp in cipherLibrary)
            {
                if (messageToDecipher.Contains(kvp.Key) && kvp.Key == messageToDecipher.Substring(0, kvp.Key.Length))
                {
                    DecipheredMessage[index] = kvp.Value;

                    DecipherMessage(index + kvp.Key.Length);
                }
            }

            DecipheredMessage[index] = default(char);

            return;
        }
    }
}