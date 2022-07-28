using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQify.Tasks
{
    public static class Task09
    {
        //Task 09 - Filter the people who's first name ends with 'a' and find their average age.

        public static double Execute(List<Person> people)
        {
            var result = new List<Person>();
            var totalSum = 0.00;
            var filteredPeople = 0;

            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].FirstName.EndsWith('a'))
                {
                    totalSum += people[i].Age;
                    filteredPeople++;
                }
            }
            return totalSum/filteredPeople;
        }

        public static double ExecuteWithLINQ(List<Person> people)
        {
            return people
                .FindAll(x => x.FirstName.EndsWith('a'))
                .Average(x => x.Age);
        }
    }
}
