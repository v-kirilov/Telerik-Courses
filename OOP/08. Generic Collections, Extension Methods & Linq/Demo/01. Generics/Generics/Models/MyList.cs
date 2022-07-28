namespace Generics.Models
{
    // type parameter T in angle brackets
    public class MyList<T>
    {
        // T as a private member data type
        private T[] items;

        // field to keep track of the number of elements
        private int count;

        // constructor
        public MyList()
        {
            this.items = new T[4];
        }

        // T as method parameter type:
        public void Add(T value)
        {
            this.items[this.count] = value;
            this.count++;
        }

        // T as return type of a method
        public T GetElementAt(int index)
        {
            T element = this.items[index];
            return element;
        }

        public bool Contains(T element)
        {   // <-- Put a breakpoint here
            foreach (T item in this.items)
            {
                if(item.Equals(element))
                {
                    return true;
                }
            }

            return false;
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }
    }
}
