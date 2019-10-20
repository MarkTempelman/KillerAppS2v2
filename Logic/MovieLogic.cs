using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.SQLContext;
using Models;

namespace Logic
{
    public class MovieLogic
    {
        private readonly IMovieContext _iMovieContext = new MovieSQLContext();
        public List<MovieModel> GetAllMovies()
        {
            return _iMovieContext.GetAllMovies();
        }

        public List<MovieModel> SearchForMovies(SearchModel search)
        {
            List<MovieModel> movies = GetAllMovies();
            
            return movies;
        }

      
    }
}
