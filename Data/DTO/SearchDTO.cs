using System;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace Data.DTO
{
    public class SearchDTO
    {
        public GenreDTO Genre { get; set; }
        public DateTime ReleasedAfter { get; set; }
        public DateTime ReleasedBefore { get; set; }
        public string SearchTerm { get; set; }
        public SortBy SortBy { get; set; }
        public List<GenreDTO> AllGenres { get; set; }

        public SearchDTO()
        {

        }

        public SearchDTO(GenreDTO genre, DateTime releasedAfter, DateTime releasedBefore, string searchTerm, SortBy sortBy)
        {
            Genre = genre;
            ReleasedAfter = releasedAfter;
            ReleasedBefore = releasedBefore;
            SearchTerm = searchTerm;
            SortBy = sortBy;
        }
    }
}
