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
        private readonly GenreLogic _genreLogic = new GenreLogic();

        public SearchLogic(GenreLogic genreLogic)
        {
            _genreLogic = genreLogic;
        }

        public SearchLogic()
        {

        }

        public SearchDTO ToSearchDTO(SearchModel searchModel)
        {
            return new SearchDTO(_genreLogic.ToGenreDTO(searchModel.Genre), searchModel.ReleasedAfter, searchModel.ReleasedBefore, searchModel.SearchTerm, searchModel.SortBy);
        }
    }
}
