using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQify.Tasks
{
    public static class Task08
    {
        //Task 08 - Return a collection of strings containing every person's fullname

        public static List<string> Execute(List<Person> people)
        {
            List<string> fullNames = new List<string>();

            for (int i = 0; i < people.Count; i++)
            {
                fullNames.Add($"{people[i].FirstName} {people[i].LastName}");
            }

            return fullNames;
        }

        public static List<string> ExecuteWithLINQ(List<Person> people)
        {
            return people
                .Select(x => x.FirstName +" "+ x.LastName)
                .ToList();
        }
    }
}
