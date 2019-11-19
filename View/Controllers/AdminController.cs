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
        public IActionResult AddMovie(MovieViewModel movie)
        {
            _movieLogic.CreateNewMovie(ViewModelToModel.ToMovieModel(movie));
            return RedirectToAction("Index", "Movie");
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
        public IActionResult AddGenreToMovie(GenreViewModel genre)
        {
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
        public IActionResult EditMovie(MovieViewModel movie)
        {
            _movieLogic.EditMovie(ViewModelToModel.ToMovieModel(movie));

            return RedirectToAction("Index", "Movie");
        }
    }
}