using System;
using System.Collections.Generic;
using System.Text;
using Models.Enums;

namespace Models
{
    public class SearchModel
    {
        public GenreModel Genre { get; set; }
        public DateTime ReleasedAfter { get; set; }
        public DateTime ReleasedBefore { get; set; }
        public string SearchTerm { get; set; }
        public SortBy SortBy { get; set; }
        public List<GenreModel> AllGenres { get; set; }

        public SearchModel()
        {

        }
    }
}
