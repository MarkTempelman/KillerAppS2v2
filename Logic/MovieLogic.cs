using System;
using System.Collections.Generic;
using System.Linq;
using Data.DTO;
using Data.Interfaces;
using Data.SQLContext;
using Models;

namespace Logic
{
    public class MovieLogic
    {
        private readonly IMovieContext _iMovieContext;
        private readonly GenreLogic _genreLogic = new GenreLogic();
        private readonly SearchLogic _searchLogic = new SearchLogic();

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
            return _genreLogic.AddGenresToMovies(_iMovieContext.GetAllMovies().Select(ToMovieModel));
        }

        public MovieModel GetMovieById(int id)
        {
            return ToMovieModel(_iMovieContext.GetMovieById(id));
        }

        public IEnumerable<MovieModel> GetMoviesBySearchModel(SearchModel search)
        {
            return _iMovieContext.GetMoviesBySearchModel(_searchLogic.ToSearchDTO(search)).Select(ToMovieModel);
        }

        private MovieDTO ToMovieDTO(MovieModel movieModel)
        {
            return new MovieDTO(movieModel.MovieId, movieModel.Title, movieModel.Description, movieModel.ReleaseDate, movieModel.MediaId);
        }

        private MovieModel ToMovieModel(MovieDTO movieDTO)
        {
            return new MovieModel(movieDTO.MovieId, movieDTO.Title, movieDTO.Description, movieDTO.ReleaseDate, movieDTO.MediaId);
        }
    }
}
