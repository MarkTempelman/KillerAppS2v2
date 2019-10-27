using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Models;

namespace Data.MemoryContext
{
    public class MovieMemoryContext : IMovieContext
    {
        public IEnumerable<MovieModel> GetAllMovies()
        {
            IEnumerable<MovieModel> movies = new List<MovieModel>
            {
                new MovieModel(1, "TestTitle2", "TestDescription1", new DateTime(2019, 10, 23)),
                new MovieModel(2, "TestTitle1", "TestDescription2", new DateTime(2019, 10, 15))
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
    }
}
