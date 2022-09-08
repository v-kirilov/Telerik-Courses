using System;
using System.Collections.Generic;
using System.Text;

namespace StackQueueWorkshop.Stack
{
    public class ArrayStack<T> : IStack<T>
    {
        private T[] items;
        private int top = 0;

        public int Size
        {
            get
            {
                if (top == 0)
                {
                    return 0;
                }
                else
                {

                    return this.items.Length;
                }
                //throw new NotImplementedException();
            }
        }

        public bool IsEmpty
        {
            get
            {
                if (top == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //throw new NotImplementedException();
            }
        }

        public void Push(T element)
        {
            if (top == 0)
            {
                top++;
                this.items = new T[top];
                this.items[0] = element;
            }

            else
            {
                T[] oldArr = this.items;
                this.items = new T[top + 1];
                for (int i = 0; i < oldArr.Length; i++)
                {
                    this.items[i] = oldArr[i];
                }
                this.items[top] = element;
                top++;
            }
            // throw new NotImplementedException();
        }

        public T Pop()
        {
            if (top == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }
            else
            {
                var element = this.items[top-1];
                top--;
                return element;
            }
            //throw new NotImplementedException();
        }

        public T Peek()
        {
            if (top==0)
            {
                throw new InvalidOperationException("Stack is empty");
            }
            return this.items[top-1];
            //throw new NotImplementedException();
        }
    }
}
