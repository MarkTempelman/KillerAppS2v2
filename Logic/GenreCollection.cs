using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Data.Interfaces;
using Data.SQLContext;
using Data.DTO;
using Logic.Models;

namespace Logic
{
    public class GenreCollection
    {
        private readonly IGenreContext _iGenreContext;

        public GenreCollection(IGenreContext iGenreContext)
        {
            _iGenreContext = iGenreContext;
        }

        public IEnumerable<GenreModel> GetAllGenres()
        {
            return _iGenreContext.GetAllGenres()
                .Select(ToGenreModel);
        }

        public IEnumerable<GenreModel> GetGenreModelsNotAssignedToThisMovie(int movieId)
        {
            return _iGenreContext.GetAllGenres().Select(ToGenreModel)
                .Except(_iGenreContext.GetGenresByMovieId(movieId).Select(ToGenreModel));
        }

        public IEnumerable<MovieModel> AddGenresToMovies(IEnumerable<MovieModel> movies)
        {
            movies = movies.ToList();
            foreach (MovieModel movie in movies)
            {
                foreach (var genre in _iGenreContext.GetGenresByMovieId(movie.MovieId))
                {
                    movie.Genres.Add(ToGenreModel(genre));
                }
            }
            return movies;
        }

        public GenreModel CreateNewGenreModel(string genre, int genreId, int movieId)
        {
            return new GenreModel(genre, genreId, movieId, _iGenreContext);
        }

        public GenreModel ToGenreModel(GenreDTO genre)
        {
            return CreateNewGenreModel(genre.Genre, genre.GenreId, genre.MovieId);
        }

        public void RemoveGenreById(int id)
        {
            _iGenreContext.RemoveGenreById(id);
        }
    }
}
