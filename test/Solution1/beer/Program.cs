using System;

namespace beer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Beer beer = new Beer("shumensko");
            beer.Update("pirinsko");
            Console.WriteLine(beer);
        }
    }

    public class Beer
    {
        private readonly string name;
        public Beer(string name)
        {
            this.name = name;
        }
        public void Update(string newName)
        {
            this.name = newName;
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
