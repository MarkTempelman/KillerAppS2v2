using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.DTO;
using Models;

namespace Logic
{
    public class SearchLogic
    {
        GenreLogic _genreLogic = new GenreLogic();

        public SearchLogic(GenreLogic genreLogic)
        {
            _genreLogic = genreLogic;
        }
        private SearchDTO ToSearchDTO(SearchModel searchModel)
        {
            List<GenreDTO> genreList= searchModel.AllGenres.Select(genreModel => _genreLogic.ToGenreDTO(genreModel)).ToList();
            return new SearchDTO(_genreLogic.ToGenreDTO(searchModel.Genre), searchModel.ReleasedAfter, searchModel.ReleasedBefore, searchModel.SearchTerm, searchModel.SortBy, genreList);
        }
    }
}
