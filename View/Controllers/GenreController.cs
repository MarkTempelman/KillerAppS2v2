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

        public IActionResult AddNewGenre()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewGenre(GenreViewModel genreViewModel)
        {
            if (_genreLogic.TryCreateNewGenre(ViewModelToModel.ToGenreModel(genreViewModel)))
            {
                return RedirectToAction("Index", "Movie");
            }
            ModelState.AddModelError("Genre", "This genre already exists");
            return View(genreViewModel);
        }
    }
}