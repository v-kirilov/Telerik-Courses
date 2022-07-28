using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQify.Tasks
{
    public static class Task11
    {
        //Task 11 - Return a collection ordered by first name and then ordered by last name.

        public static List<Person> Execute(List<Person> people)
        {
            var result = new List<Person>(people);

            result.Sort((a, b) =>
            {
                if (a.FirstName.CompareTo(b.FirstName) != 0)
                {
                    return a.FirstName.CompareTo(b.FirstName);
                }
                else
                {
                    return a.LastName.CompareTo(b.LastName);
                }
            });

            return result;
        }

        public static List<Person> ExecuteWithLINQ(List<Person> people)
        {
            return people
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();
        }
    }
}
