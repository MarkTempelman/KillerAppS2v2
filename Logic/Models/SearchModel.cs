using System;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace Logic.Models
{
    public class SearchModel
    {
        public GenreModel Genre { get; set; }
        public DateTime ReleasedAfter { get; set; } = DateTime.MinValue;
        public DateTime ReleasedBefore { get; set; } = DateTime.MaxValue;
        public string SearchTerm { get; set; }
        public SortBy SortBy { get; set; } = SortBy.Title;
        public List<GenreModel> AllGenres { get; set; }

        public SearchModel()
        {

        }
    }
}
