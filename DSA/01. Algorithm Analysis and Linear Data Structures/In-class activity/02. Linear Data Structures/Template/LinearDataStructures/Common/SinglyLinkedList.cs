using System;

namespace LinearDataStructures.Common
{
    public class SinglyLinkedList<T>
    {
        public Node<T> Head
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates an empty list.
        /// </summary>
        public SinglyLinkedList()
        {
            this.Head = null;
        }

        /// <summary>
        /// Creates a new list and initializes its head with the given value.
        /// </summary>
        /// <param name="value">The value of the head node.</param>
        public SinglyLinkedList(T value)
        {
            this.AddFirst(value);
        }

        /// <summary>
        /// Creates a new list from the given array.
        /// </summary>
        /// <param name="array">The array to create a new list from.</param>
        public SinglyLinkedList(T[] array)
        {
            if (array == null)
            {
                throw new ArgumentException("Cannot create a list from a null array.");
            }

            // Iterate the array from last to first
            for (int i = array.Length - 1; i >= 0; i--)
            {
                this.AddFirst(array[i]);
            }
        }


        /// <summary>
        /// Adds a new head node with the given value.
        /// </summary>
        /// <param name="value">The value of the new head node.</param>
        /// <returns>A reference to the newly created head.</returns>
        public Node<T> AddFirst(T value)
        {
            var node = new Node<T>(value);
            node.Next = this.Head;
            this.Head = node;

            return node;
        }

        /// <summary>
        /// Removes the head node of the list.
        /// </summary>
        /// <returns>The value of the head.</returns>
        public T RemoveFirst()
        {
            if (this.Head == null)
            {
                throw new NullReferenceException("Cannot remove head. The list is empty.");
            }

            T value = this.Head.Value;
            this.Head = this.Head.Next;

            return value;
        }

        public override string ToString()
        {
            string result = "";

            var current = this.Head;
            while (current != null)
            {
                result += $"{current.Value} ";
                current = current.Next;
            }

            return result.TrimEnd();
        }
    }
}
