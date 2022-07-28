using System;
using System.Text;
using Generics.Models;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            // declare a custom list of type int
            MyList<int> numbersList = new MyList<int>();
            numbersList.Add(3);
            numbersList.Add(5);
            numbersList.Add(7);
            numbersList.Add(9);
            for (int i = 0; i < numbersList.Count; i++)
            {
                int number = numbersList.GetElementAt(i);

                Console.WriteLine(number);
            }

            // declare a custom list of type string
            MyList<string> stringsList = new MyList<string>();
            stringsList.Add("Generics ");
            stringsList.Add("are ");
            stringsList.Add("the ");
            stringsList.Add("best!");

            string result = "";

            for (int i = 0; i < stringsList.Count; i++)
            {
                string word = stringsList.GetElementAt(i);
                result += word;
            }
            Console.WriteLine(result);

            // declare a custom list of type Country (inside the Models folder)
            MyList<Country> countriesList = new MyList<Country>();
            countriesList.Add(new Country("Albania"));
            countriesList.Add(new Country("Bulgaria"));
            countriesList.Add(new Country("France"));
            countriesList.Add(new Country("Germany"));


            Country targetCountry = new Country("Bulgaria");

            bool countryExists = countriesList.Contains(targetCountry);
            Console.WriteLine($"{targetCountry.Name} exists? Answer: {countryExists}");
        }
    }
}
