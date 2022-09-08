using System;
using System.Collections.Generic;

namespace P03_Linked_List_Cycles
{
    internal class Program
    {
        public class Node
        {
            public Node(int value)
            {
                this.Value = value;
            }
            public int Value
            {
                get;
                set;
            }
            public Node Next
            {
                get;
                set;

            }
        }
        static void Main(string[] args)
        {
            Node head = Init();
            //We need to check now if a loop exists!

            //CheckForCycle(head);
            CheckForCycleSlowFastPonter(head);
        }

        private static void CheckForCycleSlowFastPonter(Node head)
        {
            var slow = head;
            var fast = head;
            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;

                if (slow == fast)
                {
                    Console.WriteLine("We have a loop!");
                    return;
                }
            }
            Console.WriteLine("We don't have a loop!");

        }
        private static void CheckForCycle(Node head)
        {
            HashSet<int> set = new HashSet<int>();


            while (head != null)
            {
                if (set.Contains(head.Value))
                {
                    Console.WriteLine("We have a loop!");
                    return;
                }
                else
                {
                    set.Add(head.Value);
                    head = head.Next;
                }
            }
            Console.WriteLine("We don't have a loop!");
        }

        static Node Init()
        {
            Node head = new Node(1);
            var current = head;
            for (int i = 2; i < 11; i++)
            {
                current.Next = new Node(i);
                current = current.Next;
            }
            current.Next = head; // We make a loop!
            return head;
        }
    }
}
