using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Logic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using View.Helpers;
using View.ViewModels;

namespace View.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieLogic _movieLogic;
        private readonly GenreLogic _genreLogic;
        private readonly IHostingEnvironment _hostingEnvironment;

        public MovieController(MovieLogic movieLogic, GenreLogic genreLogic, IHostingEnvironment hostingEnvironment)
        {
            _movieLogic = movieLogic;
            _genreLogic = genreLogic;
            _hostingEnvironment = hostingEnvironment;
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

        [Authorize(Policy = "admin")]
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
        [Authorize(Policy = "admin")]
        public IActionResult AddMovie(MovieViewModel movie)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (movie.Image != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath + "/images");
                    uniqueFileName = Guid.NewGuid() + "_" + movie.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    movie.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                movie.ImagePath = uniqueFileName;

                _movieLogic.CreateNewMovie(ViewModelToModel.ToMovieModel(movie));
                return RedirectToAction("Index", "Movie");
            }

            foreach (GenreModel genre in _genreLogic.GetAllGenres())
            {
                movie.AllGenres.Add(ModelToViewModel.ToGenreViewModel(genre));
            }
            return View(movie);
        }

        [Authorize(Policy = "admin")]
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
        [Authorize(Policy = "admin")]
        public IActionResult EditMovie(MovieViewModel movie)
        {
            if (ModelState.IsValid)
            {
                _movieLogic.EditMovie(ViewModelToModel.ToMovieModel(movie));
                return RedirectToAction("Index", "Movie");
            }

            return View(movie);
        }

        [Authorize(Policy = "admin")]
        public IActionResult DeleteMovie(int id)
        {
            _movieLogic.DeleteMovieById(id);
            return RedirectToAction("Index", "Movie");
        }
    }
}