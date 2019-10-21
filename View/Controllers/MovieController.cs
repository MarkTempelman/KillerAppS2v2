﻿using System;
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
                return shortString += "...";
            }
            return longString;
        }

        private IEnumerable<MovieModel> GetAllMovieModels()
        {
            return _movieLogic.GetAllMovies();
        }

        private IEnumerable<MovieViewModel> ToMovieViewModels(IEnumerable<MovieModel> movies)
        {
            return movies.Select(movie => new MovieViewModel(movie.Title, movie.Description, movie.ReleaseDate, ShortenStringIfNecessary(movie.Description, 200)));
        }

        public GenreModel ToGenreModel(GenreViewModel genre)
        {
            return new GenreModel(genre.Genre, genre.GenreId);
        }

        public SearchModel ToSearchModel(SearchViewModel search)
        {
            var searchModel = new SearchModel();
            if (search.Genre != null)
            {
                searchModel.Genre = ToGenreModel(search.Genre);
            }

            searchModel.ReleasedAfter = search.ReleasedAfter;
            searchModel.ReleasedBefore = search.ReleasedBefore;

            if (search.SearchTerm != null)
            {
                searchModel.SearchTerm = search.SearchTerm;
            }

            if (search.SortBy != null)
            {
                searchModel.SortBy = search.SortBy;
            }

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

        public ActionResult SearchPartial()
        {
            SearchViewModel searchViewModel = new SearchViewModel();
            return View(searchViewModel);
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel search)
        {
            return View(ToMovieViewModels(_movieLogic.GetMoviesBySearchModel(_movieLogic.GetAllMovies(), ToSearchModel(search))));
        }
    }
}