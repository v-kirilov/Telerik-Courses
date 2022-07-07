using System;
using System.Collections.Generic;
using System.Linq;
using Cosmetics.Core;

namespace Cosmetics.Tests.Helpers
{
    public class TestUtilities
    {
        /**
         * Returns a new List with size equal to wantedSize.
         * Useful when you do not care what the contents of the List are,
         * for example when testing if a list of a command throws exception
         * when it's parameters list's size is less/more than expected.
         *
         * @param wantedSize the size of the List to be returned.
         * @return a new List with size equal to wantedSize
         */
        public static List<string> InitializeListWithSize(int wantedSize)
        {
            return new string[wantedSize].ToList();
        }

        public static Repository InitializeRepository()
        {
            return new Repository();
        }
    }
}
