using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Data;
using Data.Interfaces;
using Data.SQLContext;

namespace Logic
{
    public class GenreLogic
    {
        private readonly IGenreContext _iGenreContext;

        public GenreLogic()
        {
            _iGenreContext = new GenreSQLContext();
        }

        public IEnumerable<GenreModel> GetAllGenres()
        {
            return _iGenreContext.GetAllGenres();
        }

        public IEnumerable<MovieModel> AddGenresToMovies(IEnumerable<MovieModel> movies)
        {
            foreach (MovieModel movie in movies)
            {
                movie.Genres.AddRange(_iGenreContext.GetGenresByMovieId(movie.MovieId));
            }
            return movies;
        }
    }
}
