using System;
using System.Collections.Generic;
using System.Text;

namespace Start.model
{
    internal class Child : Base
    {
        public Child(string name, int age,string nickname) : base(name, age)
        {
            if (nickname.Length<2 || nickname.Length>10)
            {
                throw new ArgumentException("Not good");
            }
            this.Nickname= nickname;
        }

        public string Nickname { get; }
    }
}
