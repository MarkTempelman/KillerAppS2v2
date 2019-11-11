using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KillerApp.ViewModels;
using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using View.ViewModels;

namespace View.Controllers
{
    [Authorize(Policy = "admin")]
    public class AdminController : Controller
    {
        private readonly GenreLogic _genreLogic = new GenreLogic();
        private readonly MovieController _movieController = new MovieController();
        private readonly MovieLogic _movieLogic = new MovieLogic();

        public IActionResult AddMovie()
        {
            MovieViewModel movie = new MovieViewModel();
            foreach (GenreModel genre in _genreLogic.GetAllGenres())
            {
                movie.AllGenres.Add(_movieController.ToGenreViewModel(genre));
            }
            return View(movie);
        }

        [HttpPost]
        public IActionResult AddMovie(MovieViewModel movie)
        {
            _movieLogic.CreateNewMovie(ToMovieModel(movie));
            return RedirectToAction("Index", "Movie");
        }

        public IActionResult AddGenreToMovie(int movieId)
        {
            GenreViewModel genreViewModel = new GenreViewModel();
            foreach (GenreModel genre in _genreLogic.GetAllGenres())
            {
                genreViewModel.AllGenres.Add(_movieController.ToGenreViewModel(genre));
            }

            genreViewModel.MovieId = movieId;
            return View(genreViewModel);
        }

        private MovieModel ToMovieModel(MovieViewModel movieViewModel)
        {
            MovieModel movieModel = new MovieModel(movieViewModel.Title, movieViewModel.Description, movieViewModel.ReleaseDate);
            movieModel.Genres.Add(new GenreModel(movieViewModel.GenreId));

            return movieModel;
        }
    }
}