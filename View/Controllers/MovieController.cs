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

        private string ShortenStringIfNecessary(string longString)
        {
            if (longString.Length > 100)
            {
                string shortString = longString.Remove(100);
                return shortString += "...";
            }
            return longString;
        }

        public ActionResult HomePage()
        {
            IEnumerable<MovieModel> movies = _movieLogic.GetAllMovies();
            List<MovieViewModel> movieViewModels = new List<MovieViewModel>();
            foreach (var movie in movies)
            {
                MovieViewModel movieViewModel = new MovieViewModel(movie.Title, movie.Description, movie.ReleaseDate, ShortenStringIfNecessary(movie.Description));
                movieViewModels.Add(movieViewModel);
            }
            return View(movieViewModels);
        }
    }
}