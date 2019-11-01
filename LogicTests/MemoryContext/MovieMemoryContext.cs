using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;
using Data.Interfaces;
using Models;

namespace LogicTests.MemoryContext
{
    public class MovieMemoryContext : IMovieContext
    {
        public IEnumerable<MovieDTO> GetAllMovies()
        {
            IEnumerable<MovieDTO> movies = new List<MovieDTO>
            {
                new MovieDTO(1, "TestTitle2", "TestDescription1", new DateTime(2019, 10, 23),1),
                new MovieDTO(2, "TestTitle1", "TestDescription2", new DateTime(2019, 10, 15), 2)
            };
            return movies;
        }

        public IEnumerable<GenreModel> GetGenresByMovieId(int movieId)
        {
            List<GenreModel> genres = new List<GenreModel>();
            if (movieId == 1)
            {
                genres.Add(new GenreModel("TestGenre1", 1));
            }
            else if (movieId == 2)
            {
                genres.Add(new GenreModel("TestGenre2", 2));
            }
            return genres;
        }

        public IEnumerable<GenreModel> GetAllGenres()
        {
            List<GenreModel> genres = new List<GenreModel>
            {
                new GenreModel("TestGenre1", 1),
                new GenreModel("TestGenre2", 2)
            };
            return genres;
        }

        public MovieDTO GetMovieById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MovieDTO> GetMoviesBySearchModel(SearchDTO search)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MovieModel> GetMoviesBySearchModel(SearchModel search)
        {
            throw new NotImplementedException();
        }
    }
}
