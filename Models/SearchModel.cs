using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class SearchModel
    {
        public string Genre { get; set; }
        public DateTime ReleasedAfter { get; set; }
        public DateTime ReleasedBefore { get; set; }
        public string SearchTerm { get; set; }
        public string SortBy { get; set; }
    }
}
