using System;
using System.Collections.Generic;
using System.Text;

namespace ReturnDelegate
{
    class Record
    {
        public RecordType Type { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
    }

    enum RecordType
    {
        Vinyl,
        CD,
        Casette
    }
}
