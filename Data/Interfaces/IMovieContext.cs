using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DTO;

namespace Data.Interfaces
{
    public interface IMovieContext
    {
        IEnumerable<MovieDTO> GetAllMovies();

        MovieDTO GetMovieById(int id);

        IEnumerable<MovieDTO> GetMoviesBySearchModel(SearchDTO search);

        void CreateNewMovie(MovieDTO movie);

        void EditMovie(MovieDTO movie);

        MovieDTO GetMovieFromMediaId(int mediaId);

    }
}
