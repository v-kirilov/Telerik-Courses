using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQify.Tasks
{
    public static class Task12
    {
        //Task 12 - Find the people who are under or are 55 and their eye color is green. 
        //Then order them by first name, then by last name and add them to the result list until a person's last name ends with 'z'

        public static List<Person> Execute(List<Person> people)
        {
            var filteredPeople = new List<Person>();

            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].Age <= 55 && people[i].EyeColor == "green")
                {
                    filteredPeople.Add(people[i]);
                }
            }

            filteredPeople.Sort((a, b) =>
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

            var result = new List<Person>();

            for (int i = 0; i < filteredPeople.Count; i++)
            {
                if (filteredPeople[i].LastName.EndsWith('z'))
                {
                    break;
                }
                result.Add(filteredPeople[i]);
            }

            return result;
        }

        public static List<Person> ExecuteWithLINQ(List<Person> people)
        {
            return people
                .Where(x=>x.Age<=55 && x.EyeColor=="green")
                .OrderBy(x=>x.FirstName)
                .ThenBy(x=>x.LastName)
                .TakeWhile(x => !x.LastName.EndsWith('z'))
                .ToList();
        }
    }
}
