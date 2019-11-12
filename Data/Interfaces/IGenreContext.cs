using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;

namespace Data.Interfaces
{
    public interface IGenreContext
    {
        IEnumerable<GenreDTO> GetGenresByMovieId(int movieId);

        IEnumerable<GenreDTO> GetAllGenres();

        void AddGenreToMovie(GenreDTO genre);
    }
}
