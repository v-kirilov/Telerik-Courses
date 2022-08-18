using System;
using System.Collections.Generic;
using System.Text;

namespace StackQueueWorkshop
{
    public class Node<T>
    {
        public T Data
        {
            get;
            set;
        }

        public Node<T> Next
        {
            get;
            set;
        }
    }
}
