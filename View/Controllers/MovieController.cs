using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using View.Helpers;
using View.ViewModels;

namespace View.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieLogic _movieLogic;

        public MovieController(MovieLogic movieLogic)
        {
            _movieLogic = movieLogic;
        }

        public ActionResult Index()
        {
            var movies = _movieLogic.GetAllMovies().ToList();
            if (User.Identity.IsAuthenticated)
            {
                movies = _movieLogic.CheckIfMoviesAreFavourites(movies, int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value));
            }

            var movieViewModels = ModelToViewModel.ToMovieViewModels(movies);

            foreach (var movie in movieViewModels)
            {
                movie.GenresString = MiscHelper.GetStringFromGenreViewModels(movie.Genres);
            }
            return View(movieViewModels);
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel search)
        {
            List<MovieModel> movies = _movieLogic.GetMoviesBySearchModel(ViewModelToModel.ToSearchModel(search)).ToList();
            if (User.Identity.IsAuthenticated)
            {
                movies = _movieLogic.CheckIfMoviesAreFavourites(movies, int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value));
            }

            var movieViewModels = ModelToViewModel.ToMovieViewModels(movies);

            foreach (var movie in movieViewModels)
            {
                movie.GenresString = MiscHelper.GetStringFromGenreViewModels(movie.Genres);
            }
            return View(movieViewModels);
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
                MiscHelper.ShortenStringIfNecessary(movieModel.Description), genresViewModels, movieModel.MovieId);

            return View(movieViewModel);
        }
    }
}