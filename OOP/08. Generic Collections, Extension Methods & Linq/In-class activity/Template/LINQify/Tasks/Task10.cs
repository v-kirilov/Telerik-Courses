using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQify.Tasks
{
    public static class Task10
    {
        //Task 10 - Return a collection of People skipping the first 5 in the list and then taking another 5

        public static List<Person> Execute(List<Person> people)
        {
            var result = new List<Person>();
            for (int i = 0; i < people.Count; i++)
            {
                if (i >= 5 && i <10)
                {
                    result.Add(people[i]);
                }
            }
            return result;
        }

        public static List<Person> ExecuteWithLINQ(List<Person> people)
        {
            return people
                .Skip(5)
                .Take(5)
                .ToList();
        }
    }
}
