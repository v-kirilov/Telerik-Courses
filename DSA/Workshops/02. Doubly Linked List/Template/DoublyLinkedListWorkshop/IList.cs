using System.Collections.Generic;

namespace DoublyLinkedListWorkshop
{
    public interface IList<T> : IEnumerable<T>
    {
        T Head { get; }

        T Tail { get; }

        int Count { get; }

        void AddFirst(T value);

        void AddLast(T value);

        void Add(int index, T value);

        T Get(int index);

        int IndexOf(T value);

        T RemoveFirst();

        T RemoveLast();
    }
}
