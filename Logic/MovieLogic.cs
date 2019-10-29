using System;
using System.Collections.Generic;
using System.Linq;
using Data.Interfaces;
using Data.SQLContext;
using Models;

namespace Logic
{
    public class MovieLogic
    {
        private readonly IMovieContext _iMovieContext;
        private readonly GenreLogic _genreLogic = new GenreLogic();

        public MovieLogic()
        {
            _iMovieContext = new MovieSQLContext();
        }

        public MovieLogic(IMovieContext movieContext)
        {
            _iMovieContext = movieContext;
        }

        public IEnumerable<MovieModel> GetAllMovies()
        {
            return _genreLogic.AddGenresToMovies(_iMovieContext.GetAllMovies());
        }

        public MovieModel GetMovieById(int id)
        {
            return _iMovieContext.GetMovieById(id);
        }

        public IEnumerable<MovieModel> GetMoviesBySearchModel(SearchModel search)
        {
            return _genreLogic.AddGenresToMovies(_iMovieContext.GetMoviesBySearchModel(search));
        }
    }
}
