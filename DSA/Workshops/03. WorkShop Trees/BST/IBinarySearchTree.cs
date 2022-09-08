using System;
using System.Collections.Generic;

namespace BST
{
    public interface IBinarySearchTree<T> where T : IComparable<T>
    {
        T Value { get; set; }

        IBinarySearchTree<T> Left { get; set; }

        IBinarySearchTree<T> Right { get; set; }

        int Height { get; }

        IList<T> GetInOrder();

        IList<T> GetPostOrder();

        IList<T> GetPreOrder();

        IList<T> GetBFS();

        void Insert(T element);

        bool Search(T element);

        // Advanced task!
        //bool Remove(T value);
    }
}
