using System;
using System.Collections.Generic;
using System.Text;

namespace StackQueueWorkshop.Queue
{
    public class ArrayQueue<T> : IQueue<T>
    {
        private T[] items;
        private int tail=0;

        public int Size
        {
            get
            {
                if (tail == 0)
                {
                    return 0;
                }
                else
                {

                    return this.items.Length;
                }
            }
        }

        public bool IsEmpty
        {
            get
            {
                if (tail == 0)
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
            if (tail == 0)
            {
                tail++;
                this.items = new T[tail];
                this.items[0] = element;
            }

            else
            {
                T[] oldArr = this.items;
                this.items = new T[tail + 1];
                for (int i = 0; i < oldArr.Length; i++)
                {
                    this.items[i] = oldArr[i];
                }
                this.items[tail] = element;
                tail++;
            }
        }

        public T Dequeue()
        {
            if (tail == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }
            else
            {
                var element = this.items[0];
                T[] oldArr = this.items;
                this.items = new T[tail - 1];
                for (int i = 0; i < oldArr.Length-1; i++)
                {
                    this.items[i] = oldArr[i + 1];
                }
                tail--;
                return element;
            }
        }

        public T Peek()
        {
            if (tail == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }
            return this.items[0];
            throw new NotImplementedException();
        }
    }
}
