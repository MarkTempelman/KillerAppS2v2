using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace View.Helpers
{
    public static class MiscHelper
    {
        public static string ShortenStringIfNecessary(string longString, int maxLength)
        {
            if (longString.Length > maxLength)
            {
                string shortString = longString.Remove(maxLength);
                return shortString + "...";
            }
            return longString;
        }
    }
}
