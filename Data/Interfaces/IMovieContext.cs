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

        MovieModel GetMovieById(int id);

        IEnumerable<MovieModel> GetMoviesBySearchModel(SearchModel search);
    }
}
