using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data.Interfaces
{
    public interface IGenreContext
    {
        IEnumerable<GenreModel> GetGenresByMovieId(int movieId);

        IEnumerable<GenreModel> GetAllGenres();
    }
}
