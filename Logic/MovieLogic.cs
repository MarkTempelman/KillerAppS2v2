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
        public MovieLogic()
        {
            _iMovieContext = new MovieSQLContext();
        }

        public MovieLogic(IMovieContext movieContext)
        {
            _iMovieContext = movieContext;
        }
        private readonly IMovieContext _iMovieContext;
        public List<MovieModel> GetAllMovies()
        {
            return _iMovieContext.GetAllMovies();
        }

        public List<MovieModel> SearchForMovies(List<MovieModel> movies, SearchModel search)
        {
            return movies;
        }

        private List<MovieModel> FilterByGenre(List<MovieModel> movies, GenreModel genre)
        {
            return (from movie in movies from movieGenre in movie.Genres where movieGenre.Genre == genre.Genre select movie).ToList();
        }

        private List<MovieModel> ReleasedAfter(List<MovieModel> movies, DateTime releasedAfter)
        {
            return movies.Where(m => m.ReleaseDate > releasedAfter).ToList(); ;
        }

        private List<MovieModel> ReleasedBefore(List<MovieModel> movies, DateTime releasedBefore)
        {
            return movies.Where(m => m.ReleaseDate < releasedBefore).ToList();
        }

        private List<MovieModel> SearchForMoviesByTitle(List<MovieModel> movies, string searchTerm)
        {
            return (movies.Where(movie => movie.Title.Contains(searchTerm))).ToList();
        }
    }
}
