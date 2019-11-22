using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private readonly GenreLogic _genreLogic;
        private readonly MovieLogic _movieLogic;

        public AdminController(MovieLogic movieLogic, GenreLogic genreLogic)
        {
            _movieLogic = movieLogic;
            _genreLogic = genreLogic;
        }

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
        [ValidateAntiForgeryToken]
        public IActionResult AddMovie(MovieViewModel movie)
        {
            if (ModelState.IsValid)
            {
                _movieLogic.CreateNewMovie(ViewModelToModel.ToMovieModel(movie));
                return RedirectToAction("Index", "Movie");
            }

            foreach (GenreModel genre in _genreLogic.GetAllGenres())
            {
                movie.AllGenres.Add(ModelToViewModel.ToGenreViewModel(genre));
            }
            return View(movie);
        }

        public IActionResult AddGenreToMovie(int id)
        {
            GenreViewModel genreViewModel = new GenreViewModel();
            foreach (GenreModel genre in _genreLogic.GetGenreModelsNotAssignedToThisMovie(id))
            {
                genreViewModel.AllGenres.Add(ModelToViewModel.ToGenreViewModel(genre));
            }
            return View(genreViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddGenreToMovie(GenreViewModel genre)
        {
            if (genre.GenreId < 1)
            {
                foreach (GenreModel genreModel in _genreLogic.GetGenreModelsNotAssignedToThisMovie(genre.MovieId))
                {
                    genre.AllGenres.Add(ModelToViewModel.ToGenreViewModel(genreModel));
                }

                ModelState.AddModelError("Genre", "Please select a genre");
                return View(genre);
            }
            _genreLogic.AddGenreToMovie(ViewModelToModel.ToGenreModel(genre));
            return RedirectToAction("Index", "Movie");
        }

        public IActionResult EditMovie(int id)
        {
            var movieModel = _movieLogic.GetMovieById(id);
            if (movieModel == null)
            {
                return NotFound();
            }
            var movieViewModel = new MovieViewModel(movieModel.Title, movieModel.Description, movieModel.ReleaseDate,
                MiscHelper.ShortenStringIfNecessary(movieModel.Description), movieModel.MovieId);

            return View(movieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMovie(MovieViewModel movie)
        {
            if (ModelState.IsValid)
            {
                _movieLogic.EditMovie(ViewModelToModel.ToMovieModel(movie));
                return RedirectToAction("Index", "Movie");
            }

            return View(movie);
        }
    }
}