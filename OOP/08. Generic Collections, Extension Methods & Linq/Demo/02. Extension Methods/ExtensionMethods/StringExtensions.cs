using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensionMethods
{
    static class StringExtensions
    {
        // This is an extension method
        // The first parameter takes the "this" modifier and specifies the type for which the method is defined.
        public static int WordCount(this string str)
        {
            int count = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            return count;
        }
    }
}
