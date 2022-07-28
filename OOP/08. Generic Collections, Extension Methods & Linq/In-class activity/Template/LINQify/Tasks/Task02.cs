using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQify.Tasks
{
    public static class Task02
    {
        //Task 02 - Filter all people who are younger than 30 years and return them.

        public static List<Person> Execute(List<Person> people)
        {
            var result = new List<Person>();

            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].Age < 30)
                {
                    result.Add(people[i]);
                }
            }

            return result;
        }

        public static List<Person> ExecuteWithLINQ(List<Person> people)
        {
            return people
                .Where(x => x.Age < 30)
                .ToList();
        }
    }
}
