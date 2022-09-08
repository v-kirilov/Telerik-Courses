using System;
using System.Collections.Generic;
using System.Text;

namespace StackQueueWorkshop.Stack
{
    public class LinkedStack<T> : IStack<T>
    {
        private Node<T> top;
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
            //throw new NotImplementedException();
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

        public void Push(T element)
        {
            if (size == 0)
            {
                size++;
                this.top = new Node<T>();
                this.top.Data = element;
            }
            else
            {
                Node<T> current = new Node<T>();
                if (this.top.Next == null)
                {
                    current.Data = element;
                    this.top.Next = current;
                    size++;
                    return;
                }
                else
                {
                    current.Next = top.Next;
                while (current.Next != null)
                    {
                        current = current.Next;
                    }
                }
                var newNode = new Node<T>();
                newNode.Data = element;
                current.Next = newNode;
                size++;
            }


            //throw new NotImplementedException();
        }

        public T Pop()
        {
            if (size == 0)
            {
                throw new InvalidOperationException("No nodes");
            }
            else
            {
                size--;
                var current = new Node<T>();
                var previous = new Node<T>();
                current.Data = this.top.Data;
                current.Next = this.top.Next;
                while (current.Next != null)
                {
                    previous = current;
                    current = current.Next;
                }
                previous.Next = null;
                return current.Data;

            }
        }

        public T Peek()
        {
            if (size==0)
            {
                throw new InvalidOperationException("Stack is empty");
            }else if (size ==1)
            {
                return this.top.Data;
            }else
            {
                var current = this.top.Next;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                return current.Data;
            }
        }
    }
}
