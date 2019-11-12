using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace View.Helpers
{
    public static class MiscHelper
    {
        private static int _maxStringLength = 500;
        public static string ShortenStringIfNecessary(string longString)
        {
            if (longString.Length > _maxStringLength)
            {
                string shortString = longString.Remove(_maxStringLength);
                return shortString + "...";
            }
            return longString;
        }
    }
}
