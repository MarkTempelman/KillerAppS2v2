using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.DTO;
using Logic.Models;

namespace Logic
{
    public class SearchLogic
    {
        private readonly GenreCollection _genreCollection;

        public SearchLogic(GenreCollection genreCollection)
        {
            _genreCollection = genreCollection;
        }

        public SearchDTO ToSearchDTO(SearchModel searchModel)
        {
            return new SearchDTO(searchModel.Genre.ToGenreDTO(), searchModel.ReleasedAfter, searchModel.ReleasedBefore, searchModel.SearchTerm, searchModel.SortBy);
        }
    }
}
