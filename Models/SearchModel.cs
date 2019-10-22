using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class SearchModel
    {
        public GenreModel Genre { get; set; }
        public DateTime ReleasedAfter { get; set; }
        public DateTime ReleasedBefore { get; set; }
        public string SearchTerm { get; set; }
        public string SortBy { get; set; }
        public List<GenreModel> AllGenres { get; set; }

        public SearchModel()
        {

        }

        public SearchModel(string sortBy, GenreModel genre, DateTime releasedAfter, DateTime releasedBefore, string searchTerm)
        {
            SortBy = sortBy;
            Genre = genre;
            ReleasedAfter = releasedAfter;
            ReleasedBefore = releasedBefore;
            SearchTerm = searchTerm;
        }
    }
}
