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

        public MovieLogic()
        {
            _iMovieContext = new MovieSQLContext();
        }

        public MovieLogic(IMovieContext movieContext)
        {
            _iMovieContext = movieContext;
        }

        public IEnumerable<GenreModel> GetAllGenres() 
        {
            return _iMovieContext.GetAllGenres();
        }
        public IEnumerable<MovieModel> GetAllMovies()
        {
            return GetGenresForMovies(_iMovieContext.GetAllMovies());
        }

        private IEnumerable<MovieModel> GetGenresForMovies(IEnumerable<MovieModel> movies)
        {
            foreach (MovieModel movie in movies)
            {
                movie.Genres.AddRange(_iMovieContext.GetGenresByMovieId(movie.MovieId));
            }
            return movies;
        }

        public MovieModel GetMovieById(int id)
        {
            return _iMovieContext.GetMovieById(id);
        }

        public IEnumerable<MovieModel> GetMoviesBySearchModel(SearchModel search)
        {
            return GetGenresForMovies(_iMovieContext.GetMoviesBySearchModel(search));
        }
    }
}
