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
        IEnumerable<MovieModel> GetAllMovies();

        IEnumerable<GenreModel> GetGenresByMovieId(int movieId);

        IEnumerable<GenreModel> GetAllGenres();
        MovieModel GetMovieById(int id);
    }
}
