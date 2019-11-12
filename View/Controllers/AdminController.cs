using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Logic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using View.Helpers;
using View.ViewModels;

namespace View.Controllers
{
    [Authorize(Policy = "admin")]
    public class AdminController : Controller
    {
        private readonly GenreLogic _genreLogic = new GenreLogic();
        private readonly MovieLogic _movieLogic = new MovieLogic();

        public IActionResult AddMovie()
        {
            MovieViewModel movie = new MovieViewModel();
            foreach (GenreModel genre in _genreLogic.GetAllGenres())
            {
                movie.AllGenres.Add(ModelToViewModel.ToGenreViewModel(genre));
            }
            return View(movie);
        }

        [HttpPost]
        public IActionResult AddMovie(MovieViewModel movie)
        {
            _movieLogic.CreateNewMovie(ViewModelToModel.ToMovieModel(movie));
            return RedirectToAction("Index", "Movie");
        }

        public IActionResult AddGenreToMovie(int id)
        {
            GenreViewModel genreViewModel = new GenreViewModel();
            foreach (GenreModel genre in _genreLogic.GetAllGenres())
            {
                genreViewModel.AllGenres.Add(ModelToViewModel.ToGenreViewModel(genre));
            }
            return View(genreViewModel);
        }

        [HttpPost]
        public IActionResult AddGenreToMovie(GenreViewModel genre)
        {
            _genreLogic.AddGenreToMovie(ViewModelToModel.ToGenreModel(genre));
            return RedirectToAction("Index", "Movie");
        }
    }
}