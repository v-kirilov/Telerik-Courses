using System;
using System.Threading;

namespace BoardR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board newBoard = new Board();
            var item1 = new BoardItem("Implement login/logout", DateTime.Now.AddDays(3));
            var item2 = new BoardItem("Secure admin endpoints", DateTime.Now.AddDays(5));

            newBoard.AddItem(item1);
            newBoard.AddItem(item2);
           // newBoard.AddItem(item1);


            //Board.Items.Add(item1);
            //Board.Items.Add(item2);

            //var item = new BoardItem("Refactor this mess", DateTime.Now.AddDays(2));
            //item.DueDate = item.DueDate.AddYears(2);
            //item.Title = "Not that important";
            //item.RevertStatus();
            //item.AdvanceStatus();
            //item.RevertStatus();

            //Console.WriteLine(item.ViewHistory());

            //Console.WriteLine("\n--------------\n");

            //var anotherItem = new BoardItem("Don't refactor anything", DateTime.Now.AddYears(10));
            //anotherItem.AdvanceStatus();
            //anotherItem.AdvanceStatus();
            //anotherItem.AdvanceStatus();
            //anotherItem.AdvanceStatus();
            //anotherItem.AdvanceStatus();
            //Console.WriteLine(anotherItem.ViewHistory());




            //Thread.Sleep(1000);
            //BoardItem itemTwo = new BoardItem("Encrypt user data", DateTime.Now.AddDays(10));

            //Console.WriteLine();

            //Board newBoard = new Board();
            //newBoard.items.Add(itemOne);
            //newBoard.items.Add(itemTwo);

            //foreach (var boardItem in newBoard.items)
            //{
            //    boardItem.AdvanceStatus();
            //}

            //foreach (var boarItem in newBoard.items)
            //{
            //    Console.WriteLine(boarItem.ViewInfo());
            //}
            //// In progres
            ////To Do
            //var item = new BoardItem("Rewrite everything", DateTime.Now.AddDays(1));
            //Console.WriteLine();

            //var log2 = new EventLog("An important thing happened");



            //Console.WriteLine(log2.Description);
            //Console.WriteLine(log2.ViewInfo());
        }
    }
}
