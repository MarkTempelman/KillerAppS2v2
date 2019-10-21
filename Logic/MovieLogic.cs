using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<MovieModel> GetAllMovies()
        {
            return _iMovieContext.GetAllMovies();
        }

        public List<MovieModel> GetMoviesBySearchModel(List<MovieModel> movies, SearchModel search)
        {
            if (search.Genre != null)
            {
                movies = FilterMoviesByGenre(movies, search.Genre);
            }

            movies = GetMoviesReleasedAfter(movies, search.ReleasedAfter);
            movies = GetMoviesReleasedBefore(movies, search.ReleasedBefore);

            if (search.SearchTerm != null)
            {
                movies = GetMoviesByTitle(movies, search.SearchTerm);
            }
            return movies;
        }

        private List<MovieModel> FilterMoviesByGenre(List<MovieModel> movies, GenreModel genre)
        {
            return (from movie in movies from movieGenre in movie.Genres where movieGenre.Genre == genre.Genre select movie).ToList();
        }

        private List<MovieModel> GetMoviesReleasedAfter(List<MovieModel> movies, DateTime releasedAfter)
        {
            return movies.Where(m => m.ReleaseDate > releasedAfter).ToList(); ;
        }

        private List<MovieModel> GetMoviesReleasedBefore(List<MovieModel> movies, DateTime releasedBefore)
        {
            return movies.Where(m => m.ReleaseDate < releasedBefore).ToList();
        }

        private List<MovieModel> GetMoviesByTitle(List<MovieModel> movies, string searchTerm)
        {
            return movies.Where(movie => movie.Title.Contains(searchTerm)).ToList();
        }
    }
}
