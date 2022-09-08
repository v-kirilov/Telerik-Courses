namespace HashTable.SinglyLinkedList
{
    public class LinkedList<T>
    {
        public LinkedListNode<T> Head
        {
            get;
            private set;
        }

        public LinkedListNode<T> Tail
        {
            get;
            private set;
        }

        public int Count
        {
            get;
            private set;
        }

        public void AddFirst(T value)
        {
            LinkedListNode<T> node = new LinkedListNode<T>(value);

            // Save off the head node so we don't lose it
            LinkedListNode<T> temp = this.Head;

            // Point head to the new node
            this.Head = node;

            // Insert the rest of the list behind the head
            this.Head.Next = temp;

            this.Count++;

            if (this.Count == 1)
            {
                // If the list was empty then Head and Tail should both point to the new node.
                this.Tail = this.Head;
            }
        }

        public void AddLast(T value)
        {
            LinkedListNode<T> node = new LinkedListNode<T>(value);

            if (this.Count == 0)
            {
                this.Head = node;
            }
            else
            {
                this.Tail.Next = node;
            }

            this.Tail = node;

            this.Count++;
        }

        public void RemoveFirst()
        {
            if (this.Count != 0)
            {
                // Before: Head -> 3 -> 5
                // After:  Head ------> 5

                // Head -> 3 -> null
                // Head ------> null
                this.Head = this.Head.Next;
                this.Count--;

                if (this.Count == 0)
                {
                    this.Tail = null;
                }
            }
        }

        public void RemoveLast()
        {
            if (this.Count != 0)
            {
                if (this.Count == 1)
                {
                    this.Head = null;
                    this.Tail = null;
                }
                else
                {
                    // Before: Head --> 3 --> 5 --> 7
                    //         Tail = 7
                    // After:  Head --> 3 --> 5 --> null
                    //         Tail = 5
                    LinkedListNode<T> current = this.Head;
                    while (current.Next != this.Tail)
                    {
                        current = current.Next;
                    }

                    current.Next = null;
                    this.Tail = current;
                }

                this.Count--;
            }
        }

        public bool Remove(T item)
        {
            LinkedListNode<T> previous = null;
            LinkedListNode<T> current = this.Head;

            // 1: Empty list - do nothing
            // 2: Single node: (previous is null)
            // 3: Many nodes
            //    a: node to remove is the first node
            //    b: node to remove is the middle or last

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    // it's a node in the middle or end
                    if (previous != null)
                    {
                        // Case 3b

                        // Before: Head -> 3 -> 5 -> null
                        // After:  Head -> 3 ------> null
                        previous.Next = current.Next;

                        // it was the end - so update Tail
                        if (current.Next == null)
                        {
                            this.Tail = previous;
                        }

                        this.Count--;
                    }
                    else
                    {
                        // Case 2 or 3a
                        this.RemoveFirst();
                    }

                    return true;
                }

                previous = current;
                current = current.Next;
            }

            return false;
        }

        public bool Contains(T item)
        {
            LinkedListNode<T> current = this.Head;
            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public void Clear()
        {
            this.Head = null;
            this.Tail = null;
            this.Count = 0;
        }

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            LinkedListNode<T> current = this.Head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }
    }
}
