using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using Data;
using Data.Interfaces;
using Data.SQLContext;
using Data.DTO;

namespace Logic
{
    public class GenreLogic
    {
        private readonly IGenreContext _iGenreContext;

        public GenreLogic(IGenreContext iGenreContext)
        {
            _iGenreContext = iGenreContext;
        }

        public GenreLogic()
        {
            _iGenreContext = new GenreSQLContext();
        }

        public IEnumerable<GenreModel> GetAllGenres()
        {
            return _iGenreContext.GetAllGenres().Select(ToGenreModel).ToList(); ;
        }

        public IEnumerable<MovieModel> AddGenresToMovies(IEnumerable<MovieModel> movies)
        {
            foreach (MovieModel movie in movies)
            {
                movie.Genres.AddRange(_iGenreContext.GetGenresByMovieId(movie.MovieId).Select(ToGenreModel));
            }
            return movies;
        }

        public GenreDTO ToGenreDTO(GenreModel genreModel)
        {
            if (genreModel == null)
            {
                return null;
            }
            return new GenreDTO(genreModel.Genre, genreModel.GenreId);
        }

        public GenreModel ToGenreModel(GenreDTO genreDTO)
        {
            return new GenreModel(genreDTO.Genre, genreDTO.GenreId);
        }
    }
}
