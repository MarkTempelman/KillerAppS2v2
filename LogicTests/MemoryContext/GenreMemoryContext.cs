using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;
using Data.Interfaces;
using Models;

namespace LogicTests.MemoryContext
{
    class GenreMemoryContext : IGenreContext
    {
        public IEnumerable<GenreDTO> GetGenresByMovieId(int movieId)
        {
            List<GenreDTO> genres = new List<GenreDTO>();
            if (movieId == 1)
            {
                genres.Add(new GenreDTO("TestGenre1", 1));
            }
            else if (movieId == 2)
            {
                genres.Add(new GenreDTO("TestGenre2", 2));
            }

            return genres;
        }

        public IEnumerable<GenreDTO> GetAllGenres()
        {
            List<GenreDTO> genres = new List<GenreDTO>
            {
                new GenreDTO("TestGenre1", 1),
                new GenreDTO("TestGenre2", 2)
            };
            return genres;
        }
    }
}
