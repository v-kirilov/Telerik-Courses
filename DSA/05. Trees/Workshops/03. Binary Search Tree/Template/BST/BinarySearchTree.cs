using System;
using System.Collections.Generic;
using System.Linq;
namespace BST
{
    public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable<T>
    {
        //private T value;
        private IBinarySearchTree<T> left;
        private IBinarySearchTree<T> right;

        public BinarySearchTree(T value)
        {
            //Insert(value);
            this.Value = value;
            this.left = null;
            this.right = null;
        }

        public T Value
        {
            get;
            private set;
        }

        public IBinarySearchTree<T> Left
        {
            get;
            set;
        }

        public IBinarySearchTree<T> Right
        {
            get;
            set;
        }

        public int Height
        {
            get;
            private set;
        }

        public IList<T> GetInOrder()
        {
            List<T> expected = new List<T>();
            if (Left != null)
            {
                expected.AddRange(Left.GetInOrder());
                expected.Add(Value);
            }
            else
            {
                if (Value != null)
                {
                    expected.Add(Value);
                }
            }
            if (Right != null)
            {
                expected.AddRange(Right.GetInOrder());
            }
            return expected;
        }

        public IList<T> GetPostOrder()
        {
            List<T> expected = new List<T>();
            if (Left != null)
            {
                expected.AddRange(Left.GetPostOrder());
            }
            if (Right != null)
            {
                expected.AddRange(Right.GetPostOrder());
            }
            if (Value != null)
            {
                expected.Add(Value);
            }
            return expected;
        }

        public IList<T> GetPreOrder()
        {
            List<T> expected = new List<T>();

            if (Value != null)
            {
                expected.Add(Value);
            }
            if (Left != null)
            {
                expected.AddRange(Left.GetPreOrder());
            }
            if (Right != null)
            {
                expected.AddRange(Right.GetPreOrder());
            }
            return expected;
        }

        public IList<T> GetBFS()
        {
            Queue<IBinarySearchTree<T>> queue = new Queue<IBinarySearchTree<T>>();
            IList<T> expected = new List<T>();

            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                IBinarySearchTree<T> current = queue.Dequeue();
                if (current == null)
                {
                    continue;
                }
                queue.Enqueue(current.Left);
                queue.Enqueue(current.Right);

                expected.Add(current.Value);
            }
            return expected;
        }

        public void Insert(T element)
        {

            var node = new BinarySearchTree<T>(element);

            if (element.CompareTo(this.Value) < 0)
            {

                if (this.Left == null)
                {
                    this.Left = node;
                    return;
                }
                else
                {
                    var nextNode = this.Left;

                    BinarySearchTree<T> before = null;
                    while (nextNode != null)
                    {
                        before = (BinarySearchTree<T>)nextNode;
                        if (element.CompareTo(nextNode.Value) < 0)
                        {
                            nextNode = nextNode.Left;
                        }
                        else if (element.CompareTo(nextNode.Value) > 0)
                        {
                            nextNode = nextNode.Right;
                        }
                    }
                    if (element.CompareTo(before.Value) < 0)
                    {
                        before.Left = node;
                    }
                    else
                    {
                        before.Right = node;
                    }
                }
            }
            else if (element.CompareTo(this.Value) > 0)
            {
                if (this.Right == null)
                {
                    this.Right = node;

                    return;
                }
                else
                {
                    var nextNode = this.Right;

                    BinarySearchTree<T> before = null;


                    while (nextNode != null)
                    {
                        before = (BinarySearchTree<T>)nextNode;
                        if (element.CompareTo(nextNode.Value) < 0)
                        {
                            nextNode = nextNode.Left;

                        }
                        else if (element.CompareTo(nextNode.Value) > 0)
                        {
                            nextNode = nextNode.Right;
                        }

                    }
                    if (element.CompareTo(before.Value) < 0)
                    {
                        before.Left = node;
                    }
                    else
                    {
                        before.Right = node;
                    }
                }
            }
            var root = this;
            this.Height = FindHeight(root);
        }

        private int FindHeight(IBinarySearchTree<T> root)
        {
            if (root == null)
            {
                return -1;
            }
            int leftH = FindHeight(root.Left);
            int rightH = FindHeight(root.Right);
            if (leftH > rightH)
            {
                return leftH + 1;
            }
            else
            {
                return rightH + 1;
            }
        }

        public bool Search(T element)
        {
            IBinarySearchTree<T> root = this;
            while (root != null)
            {
                if (element.CompareTo(root.Value) == 0)
                {
                    return true;
                }
                if (element.CompareTo(root.Value) < 0)
                {
                    root = root.Left;
                    if (root != null)
                    {
                        continue;
                    }
                }
                else
                {
                    root = root.Right;
                    if (root != null)
                    {
                        continue;
                    }
                }
            }
            return false;
        }

        //Advanced task!
        public bool Remove(T value)
        {
            if (!Search(value))
            {
                return false;
            }
            else
            {
                var root = this;
                root = Remove(root, value);
                return true;
            }
        }

        private BinarySearchTree<T> Remove(BinarySearchTree<T> root, T value)
        {
            if (value.CompareTo(root.Value) < 0)  //Check for our value , if its smaller than we go left.
            {
                root.Left = Remove((BinarySearchTree<T>)root.Left, value);
            }
            else if (value.CompareTo(root.Value) > 0) //Check for our value , if its larger than we go right.
            {
                root.Right = Remove((BinarySearchTree<T>)root.Right, value);
            }
            else
            {
                //case 1: Node to be deleted has no children
                if (root.Left == null && root.Right == null)
                {
                    root = null;
                }
                //case 2 : node to be deleted has two children
                else if (root.Right != null && root.Left != null)
                {
                    BinarySearchTree<T> maxNode = MaxValue(root.Left);
                    root.Value = maxNode.Value;
                    root.Left = Remove((BinarySearchTree<T>)root.Left, root.Value);
                }
                else // node to be deleted has one children
                {
                    var child = root.Left != null ? root.Left : root.Right;
                    root = (BinarySearchTree<T>)child;
                }
            }
            return root;
        }

        private BinarySearchTree<T> MaxValue(IBinarySearchTree<T> node)
        {
            while (node.Right != null)
            {
                node = node.Right;
            }
            return (BinarySearchTree<T>)node;
        }
    }
}
