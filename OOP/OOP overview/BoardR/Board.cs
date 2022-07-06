using System;
using System.Collections.Generic;
using System.Text;

namespace BoardR
{
    internal class Board
    {
        public List<BoardItem> items; //Съсдаваме Лист от boarditems

        public Board()
        {
            this.items = new List<BoardItem>();   //Инициализираме този лист !!!
        }
    }
}
