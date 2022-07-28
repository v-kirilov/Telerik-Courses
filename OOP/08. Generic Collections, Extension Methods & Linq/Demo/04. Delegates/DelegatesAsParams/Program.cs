using System;
using System.Collections.Generic;
using System.Linq;

namespace DelegatesAsParams
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Exclude() accepts a func delegate as a param)
            var result = Exclude(list, x => x % 2 == 0); // 1, 3, 5, 7, 9
            var result2 = Exclude(list, x => x > 5);  // 1, 2, 3, 4, 5
            var result3 = Exclude(list, x => x == 3 && x == 8); // 1, 2, 4, 5, 6, 7, 9, 10
        }

        static List<T> Exclude<T>(List<T> items, Func<T, bool> condition)
        {
            var list = new List<T>();
            foreach (var item in items)
            {
                if (!condition(item))
                {
                    list.Add(item);
                }
            }

            return list;
        }
    }
}
