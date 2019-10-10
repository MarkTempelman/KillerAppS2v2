using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KillerApp.ViewModels;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace View.Controllers
{
    public class MovieController : Controller
    {
        private MovieLogic _movieLogic = new MovieLogic();
        public ActionResult HomePage()
        {
            IEnumerable<MovieModel> movies = _movieLogic.GetAllMovies();
           List<MovieViewModel> movieViewModels = new List<MovieViewModel>();
            foreach (var movie in movies)
            {
                MovieViewModel movieViewModel = new MovieViewModel(movie.Title, movie.Description, movie.ReleaseDate);
                movieViewModels.Add(movieViewModel);
            }
            return View(movieViewModels);
        }
    }
}