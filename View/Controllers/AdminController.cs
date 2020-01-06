using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Logic;
using Logic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment _hostingEnvironment;

        public AdminController(MovieLogic movieLogic, GenreLogic genreLogic, IHostingEnvironment hostingEnvironment)
        {
            _movieLogic = movieLogic;
            _genreLogic = genreLogic;
            _hostingEnvironment = hostingEnvironment;
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

        public IActionResult DeleteMovie(int id)
        {
            _movieLogic.DeleteMovieById(id);
            return RedirectToAction("Index", "Movie");
        }
    }
}