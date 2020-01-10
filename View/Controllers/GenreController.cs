using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using View.Helpers;
using View.ViewModels;

namespace View.Controllers
{
    public class GenreController : Controller
    {
        private GenreLogic _genreLogic;

        public GenreController(GenreLogic genreLogic)
        {
            _genreLogic = genreLogic;
        }

        [HttpPost]
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
    }
}