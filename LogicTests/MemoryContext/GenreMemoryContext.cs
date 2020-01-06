using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;
using Data.Interfaces;

namespace LogicTests.MemoryContext
{
    public class GenreMemoryContext : IGenreContext
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

        public void AddGenreToMovie(GenreDTO genre)
        {
            throw new NotImplementedException();
        }

        public void CreateNewGenre(GenreDTO genre)
        {
            throw new NotImplementedException();
        }

        public bool DoesGenreExist(string genre)
        {
            switch (genre)
            {
                case "TestGenre1":
                case "TestGenre2":
                    return true;
                default:
                    return false;
            }
        }

        public void RemoveGenreById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
