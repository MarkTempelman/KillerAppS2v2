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
    public class GenreLogic
    {
        private readonly IGenreContext _iGenreContext;

        public GenreLogic(IGenreContext iGenreContext)
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
                movie.Genres
                    .AddRange(_iGenreContext.GetGenresByMovieId(movie.MovieId).Select(ToGenreModel));
            }
            return movies;
        }

        public void AddGenreToMovie(GenreModel genre)
        {
            _iGenreContext.AddGenreToMovie(ToGenreDTO(genre));
        }

        public bool TryCreateNewGenre(GenreModel genre)
        {
            if (!_iGenreContext.DoesGenreExist(genre.Genre))
            {
                _iGenreContext.CreateNewGenre(ToGenreDTO(genre));
                return true;
            }

            return false;
        }

        public GenreDTO ToGenreDTO(GenreModel genreModel)
        {
            if (genreModel == null)
            {
                return null;
            }

            GenreDTO genreDTO = new GenreDTO(genreModel.Genre);
            if (genreModel.MovieId > 0)
            {
                genreDTO.MovieId = genreModel.MovieId;
            }

            if (genreModel.GenreId > 0)
            {
                genreDTO.GenreId = genreModel.GenreId;
            }
            return genreDTO;
        }

        public GenreModel ToGenreModel(GenreDTO genreDTO)
        {
            return new GenreModel(genreDTO.Genre, genreDTO.GenreId);
        }
    }
}
