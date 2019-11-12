using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KillerApp.ViewModels;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Enums;
using View.Helpers;
using View.ViewModels;

namespace View.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieLogic _movieLogic = new MovieLogic();
        private static int _maxStringLength = 500;

        public ActionResult Index()
        {
            return View(ModelToViewModel.ToMovieViewModels(_movieLogic.GetAllMovies(), _maxStringLength));
        }

        public ActionResult MovieListPartial()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel search)
        {
            return View(ModelToViewModel.ToMovieViewModels(_movieLogic.GetMoviesBySearchModel(ViewModelToModel.ToSearchModel(search)), _maxStringLength));
        }

        public ActionResult MovieInfo(int id)
        {
            var movieModel = _movieLogic.GetMovieById(id);
            if (movieModel == null)
            {
                return NotFound();
            }
            List<GenreViewModel> genresViewModels = movieModel.Genres.Select(ModelToViewModel.ToGenreViewModel).ToList();
            var movieViewModel = new MovieViewModel(movieModel.Title, movieModel.Description, movieModel.ReleaseDate,
                MiscHelper.ShortenStringIfNecessary(movieModel.Description, _maxStringLength), genresViewModels, movieModel.MovieId);

            return View(movieViewModel);
        }
    }
}