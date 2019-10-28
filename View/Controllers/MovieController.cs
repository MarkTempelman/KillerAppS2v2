using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KillerApp.ViewModels;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using View.ViewModels;

namespace View.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieLogic _movieLogic = new MovieLogic();

        private string ShortenStringIfNecessary(string longString, int maxLength)
        {
            if (longString.Length > maxLength)
            {
                string shortString = longString.Remove(maxLength);
                return shortString + "...";
            }
            return longString;
        }

        private IEnumerable<MovieModel> GetAllMovieModels()
        {
            return _movieLogic.GetAllMovies();
        }

        private IEnumerable<MovieViewModel> ToMovieViewModels(IEnumerable<MovieModel> movies)
        {
            List<MovieViewModel> movieViewModels = new List<MovieViewModel>();
            foreach (MovieModel movie in movies)
            {
                List<GenreViewModel> genresViewModels = movie.Genres.Select(ToGenreViewModel).ToList();
                movieViewModels.Add(new MovieViewModel(movie.Title, movie.Description, movie.ReleaseDate, ShortenStringIfNecessary(movie.Description, 200), genresViewModels, movie.MovieId));
            }
            return movieViewModels;
        }

        public GenreViewModel ToGenreViewModel(GenreModel genre)
        {
            return new GenreViewModel(genre.Genre, genre.GenreId);
        }

        public SearchModel ToSearchModel(SearchViewModel search)
        {
            var searchModel = new SearchModel();
            if (search.GenreId != -1)
            {
                searchModel.Genre = new GenreModel(search.GenreId);
            }

            searchModel.ReleasedAfter = search.ReleasedAfter;
            searchModel.ReleasedBefore = search.ReleasedBefore;

            if (search.SearchTerm != null)
            {
                searchModel.SearchTerm = search.SearchTerm;
            }

            searchModel.SortBy = search.SortBy;

            return searchModel;
        }

        public ActionResult Index()
        {
            return View(ToMovieViewModels(GetAllMovieModels()));
        }

        public ActionResult MovieListPartial()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel search)
        {
            return View(ToMovieViewModels(_movieLogic.GetMoviesBySearchModel(_movieLogic.GetAllMovies(), ToSearchModel(search))));
        }

        public ActionResult MovieInfo(int id)
        {
            var movieModel = _movieLogic.GetMovieById(id);
            if (movieModel == null)
            {
                return NotFound();
            }
            List<GenreViewModel> genresViewModels = movieModel.Genres.Select(ToGenreViewModel).ToList();
            var movieViewModel = new MovieViewModel(movieModel.Title, movieModel.Description, movieModel.ReleaseDate,
                ShortenStringIfNecessary(movieModel.Description, 200), genresViewModels, movieModel.MovieId);

            return View(movieViewModel);
        }
    }
}