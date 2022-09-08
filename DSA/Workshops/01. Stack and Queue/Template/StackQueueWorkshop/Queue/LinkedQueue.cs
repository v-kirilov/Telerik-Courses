using System;
using System.Collections.Generic;
using System.Text;

namespace StackQueueWorkshop.Queue
{
    public class LinkedQueue<T> : IQueue<T>
    {
        private Node<T> head, tail;
        private int size = 0;

        public int Size
        {
            get
            {
                if (size == 0)
                {
                    return size;
                }
                else
                {
                    return size;
                }
            }
        }

        public bool IsEmpty
        {
            get
            {
                if (size == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Enqueue(T element)
        {
            if (size == 0)
            {
                size++;
                this.head = new Node<T>();
                this.head.Data = element;
            }
            else
            {

                if (this.head.Next == null)
                {
                    this.tail = new Node<T>();
                    tail.Data = element;
                    this.head.Next = tail;
                    size++;
                    return;
                }
                else
                {
                    Node<T> current = new Node<T>();
                    current.Next = head.Next;
                    while (current.Next != null)
                    {
                        current = current.Next;
                    }
                    this.tail.Data = element;
                    current.Next = tail;
                    size++;

                }
            }

        }

        public T Dequeue()
        {
            if (size == 0)
            {
                throw new InvalidOperationException("No nodes");
            }else
            {
                size--;
                var current = this.head;
                this.head = this.head.Next;
                return current.Data;

            }
        }

        public T Peek()
        {
            if (size == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }
           
            else
            {
                return this.head.Data;
            }
        }
    }
}
