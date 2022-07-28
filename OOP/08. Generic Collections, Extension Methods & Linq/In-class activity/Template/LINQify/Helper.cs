using System;
using System.Collections.Generic;
using System.Text;

namespace LINQify
{
    public static class Helper
    {
        public static List<Person> GetData()
        {
            return new List<Person>
            {
                new Person
                {
                    FirstName = "Bowman",
                    LastName = "Parks",
                    Age = 21,
                    EyeColor = "blue"
                },
                new Person
                {
                    FirstName = "Eaton",
                    LastName = "Mcconnell",
                    Age = 49,
                    EyeColor = "green"
                },
                new Person
                {
                    FirstName = "Alfreda",
                    LastName = "Dixon",
                    Age = 46,
                    EyeColor = "brown"
                },
                new Person
                {
                    FirstName = "Terry",
                    LastName = "Hickman",
                    Age = 28,
                    EyeColor = "blue"
                },
                new Person
                {
                    FirstName = "Byers",
                    LastName = "Henderson",
                    Age = 47,
                    EyeColor = "brown"
                },
                new Person
                {
                    FirstName = "Anita",
                    LastName = "Haynes",
                    Age = 27,
                    EyeColor = "brown"
                },
                new Person
                {
                    FirstName = "Walls",
                    LastName = "Mendez",
                    Age = 42,
                    EyeColor = "green"
                },
                new Person
                {
                    FirstName = "Kellie",
                    LastName = "Clayton",
                    Age = 21,
                    EyeColor = "brown"
                },
                new Person
                {
                    FirstName = "Dorothea",
                    LastName = "Boyle",
                    Age = 50,
                    EyeColor = "blue"
                },
                new Person
                {
                    FirstName = "Jacqueline",
                    LastName = "Brennan",
                    Age = 45,
                    EyeColor = "green"
                },
                new Person
                {
                    FirstName = "Sloan",
                    LastName = "Ruiz",
                    Age = 35,
                    EyeColor = "green"
                },
                new Person
                {
                    FirstName = "Florence",
                    LastName = "Keith",
                    Age = 38,
                    EyeColor = "brown"
                },
                new Person
                {
                    FirstName = "Gordon",
                    LastName = "Gibbs",
                    Age = 21,
                    EyeColor = "green"
                },
                new Person
                {
                    FirstName = "Butler",
                    LastName = "Sargent",
                    Age = 33,
                    EyeColor = "blue"
                },
                new Person
                {
                    FirstName = "Powell",
                    LastName = "Clay",
                    Age = 25,
                    EyeColor = "green"
                },
                new Person
                {
                    FirstName = "Charity",
                    LastName = "Levine",
                    Age = 32,
                    EyeColor = "blue"
                },
                new Person
                {
                    FirstName = "Shauna",
                    LastName = "Harris",
                    Age = 25,
                    EyeColor = "brown"
                },
                new Person
                {
                    FirstName = "Avila",
                    LastName = "Johnson",
                    Age = 34,
                    EyeColor = "green"
                },
                new Person
                {
                    FirstName = "Juanita",
                    LastName = "Waller",
                    Age = 32,
                    EyeColor = "brown"
                },
                new Person
                {
                    FirstName = "Maribel",
                    LastName = "Hartman",
                    Age = 46,
                    EyeColor = "blue"
                },
                new Person
                {
                    FirstName = "Virgie",
                    LastName = "Castaneda",
                    Age = 42,
                    EyeColor = "blue"
                },
                new Person
                {
                    FirstName = "Dee",
                    LastName = "Cooley",
                    Age = 18,
                    EyeColor = "green"
                },
                new Person
                {
                    FirstName = "Marietta",
                    LastName = "Mays",
                    Age = 19,
                    EyeColor = "green"
                },
                new Person
                {
                    FirstName = "House",
                    LastName = "Gross",
                    Age = 40,
                    EyeColor = "blue"
                },
                new Person
                {
                    FirstName = "Copeland",
                    LastName = "Hester",
                    Age = 37,
                    EyeColor = "green"
                },
                new Person
                {
                    FirstName = "Serrano",
                    LastName = "Wynn",
                    Age = 26,
                    EyeColor = "blue"
                },
                new Person
                {
                    FirstName = "Inez",
                    LastName = "Donaldson",
                    Age = 45,
                    EyeColor = "green"
                },
                new Person
                {
                    FirstName = "Johns",
                    LastName = "Neal",
                    Age = 34,
                    EyeColor = "green"
                },
                new Person
                {
                    FirstName = "Spencer",
                    LastName = "Aguilar",
                    Age = 48,
                    EyeColor = "blue"
                },
                new Person
                {
                    FirstName = "Price",
                    LastName = "Molina",
                    Age = 30,
                    EyeColor = "brown"
                },
                new Person
                {
                    FirstName = "Beatrice",
                    LastName = "Mullins",
                    Age = 46,
                    EyeColor = "brown"
                },
                new Person
                {
                    FirstName = "Jodi",
                    LastName = "King",
                    Age = 31,
                    EyeColor = "brown"
                }
            };
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string EyeColor { get; set; }
    }
}
