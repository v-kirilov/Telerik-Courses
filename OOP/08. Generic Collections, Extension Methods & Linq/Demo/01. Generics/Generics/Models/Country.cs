using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.Models
{
    public class Country
    {
        public Country(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public override bool Equals(object obj)
        {   // <-- Put a breakpoint here
            Country otherPerson = obj as Country;
            if (otherPerson != null)
            {
                return this.Name == otherPerson.Name;
            }
            else
            {
                return false;
            }
        }
    }
}
