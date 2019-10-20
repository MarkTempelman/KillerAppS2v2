using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace View.ViewModels
{
    public class SearchViewModel
    {
        [DisplayName("Sort by:")]
        public string SortBy { get; set; }
        public string Genre { get; set; }
        [DisplayName("Released after:")]
        public DateTime ReleasedAfter { get; set; }
        [DisplayName("Released before:")]
        public DateTime ReleasedBefore { get; set; }
        [DisplayName("Search")]
        public string SearchTerm { get; set; }
    }
}
