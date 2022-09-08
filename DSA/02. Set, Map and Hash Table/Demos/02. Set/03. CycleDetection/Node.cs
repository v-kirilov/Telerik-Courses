using System;
using System.Collections.Generic;
using System.Text;

namespace CycleDetection
{
    public class Node
    {
        public Node(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }

        public Node Next { get; set; }
    }
}
