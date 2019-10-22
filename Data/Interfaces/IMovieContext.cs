using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Data.Interfaces
{
    public interface IMovieContext
    {
        List<MovieModel> GetAllMovies();

        List<GenreModel> GetGenresByMovieId(int movieId);

        List<GenreModel> GetAllGenres();
    }
}
