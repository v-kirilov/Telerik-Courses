using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQify.Tasks
{
    public static class Task01
    {
        //Task 01 - Filter all people with blue eyes and return them.

        public static List<Person> Execute(List<Person> people)
        {
            var result = new List<Person>();
            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].EyeColor == "blue")
                {
                    result.Add(people[i]);
                }
            }
            return result;
        }

        public static List<Person> ExecuteWithLINQ(List<Person> people)
        {
            return people
                .Where(person => person.EyeColor == "blue")
                .ToList();
        }
    }
}
