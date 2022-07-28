using System;
using System.Collections.Generic;
using System.Linq;

namespace ReturnDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            var records = new List<Record>();

            var result = records
                .Where(record => record.Artist == "Justin Bieber")
                .OrderBy(GetFilter("Vinyl"));
        }

        //GetFilter returns a Func delegate
        static Func<Record, IComparable> GetFilter(string filter)
        {
            return filter switch
            {
                "Vinyl"   => record => record.Type == RecordType.Vinyl,
                "CD"      => record => record.Type == RecordType.CD,
                "Casette" => record => record.Type == RecordType.Casette,
                "Artist"  => record => record.Artist,
                _         => record => record.Title,
            };
        }
    }
}
