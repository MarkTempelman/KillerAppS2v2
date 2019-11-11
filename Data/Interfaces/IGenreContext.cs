using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;
using Models;

namespace Data.Interfaces
{
    public interface IGenreContext
    {
        IEnumerable<GenreDTO> GetGenresByMovieId(int movieId);

        IEnumerable<GenreDTO> GetAllGenres();

        void AddGenreToMovie(GenreDTO genre);
    }
}
