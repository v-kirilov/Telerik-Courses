using System;

namespace BoardR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BoardItem item = new BoardItem("Refactor this mess", DateTime.Now.AddDays(2));
            item.AdvanceStatus();
            BoardItem anotherItem = new BoardItem("Encrypt user data", DateTime.Now.AddDays(10));

            Board newBoard = new Board();
            newBoard.items.Add(item);
            newBoard.items.Add(anotherItem);

            foreach (var boardItem in newBoard.items)
            {
                boardItem.AdvanceStatus();
            }

            foreach (var boarItem in newBoard.items)
            {
                Console.WriteLine(boarItem.ViewInfo());
            }
        }
    }
}
