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
    public class GenreController : Controller
    {
        private GenreLogic _genreLogic;

        public GenreController(GenreLogic genreLogic)
        {
            _genreLogic = genreLogic;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewGenre(GenreViewModel genreViewModel)
        {
            if (_genreLogic.TryCreateNewGenre(ViewModelToModel.ToGenreModel(genreViewModel)))
            {
                return RedirectToAction("ManageGenres");
            }
            return RedirectToAction("ManageGenres", new
            {
                addGenreErrorMessage = "This genre already exists"
            });
        }

        public IActionResult ManageGenres(string addGenreErrorMessage)
        {
            List<GenreViewModel> genres = new List<GenreViewModel>();
            foreach (var genre in _genreLogic.GetAllGenres())
            {
                genres.Add(ModelToViewModel.ToGenreViewModel(genre));
            }

            if (addGenreErrorMessage != null)
            {
                TempData["AddGenreError"] = addGenreErrorMessage;
            }

            return View(genres);
        }

        public IActionResult RemoveGenre(int id)
        {
            _genreLogic.RemoveGenreById(id);
            return RedirectToAction("ManageGenres");
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
    }
}