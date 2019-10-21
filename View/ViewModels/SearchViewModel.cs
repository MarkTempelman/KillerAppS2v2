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
        public GenreViewModel Genre { get; set; }
        [DisplayName("Released after:")]
        public DateTime ReleasedAfter { get; set; } = DateTime.MinValue;
        [DisplayName("Released before:")]
        public DateTime ReleasedBefore { get; set; } = DateTime.MaxValue;
        [DisplayName("Search")]
        public string SearchTerm { get; set; }

        public SearchViewModel()
        {

        }

        public SearchViewModel(string sortBy, GenreViewModel genre, DateTime releasedAfter, DateTime releasedBefore, string searchTerm)
        {
            SortBy = sortBy;
            Genre = genre;
            ReleasedAfter = releasedAfter;
            ReleasedBefore = releasedBefore;
            SearchTerm = searchTerm;
        }
    }
}
