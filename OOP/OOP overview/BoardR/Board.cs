using System;
using System.Collections.Generic;
using System.Text;

namespace BoardR
{
    static class Board
    {
        private static List<BoardItem> items = new List<BoardItem>(); //Create list of boardItems
        public static void AddItem(BoardItem item)
        {
            if (items.Contains(item))
            {
                throw new ArgumentException($"item already exists at {items}");
            }
            items.Add(item);
        }
        public static int TotalItems
        {
            get
            {
                return items.Count;
            }
            
        }
    }
}
