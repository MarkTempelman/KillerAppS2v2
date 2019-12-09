using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using View.ViewModels;

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

        public static string GetStringFromGenreViewModels(List<GenreViewModel> genres)
        {
            string returnValue = "";
            foreach (var genre in genres)
            {
                returnValue += genre.Genre;
                returnValue += ", ";
            }

            return returnValue.Substring(0,(returnValue.Length - 2));
        }
    }
}
