using System;
using System.Collections.Generic;
using System.Text;

namespace BoardR
{
    internal class Board
    {
        private List<BoardItem> items = new List<BoardItem>(); //Create list of boardItems

        public List<BoardItem> Items
        {
            get
            {
                List<BoardItem> copyList = new List<BoardItem>(this.items);
                return copyList;
            }

        }

        public void AddItem(BoardItem item)
        {
            if (items.Contains(item))
            {
                throw new ArgumentException($"item already exists at {this.items}");
            }
            items.Add(item);
        }
    }
}
