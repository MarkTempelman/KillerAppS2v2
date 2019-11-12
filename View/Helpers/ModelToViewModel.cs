﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using KillerApp.ViewModels;
using Models;
using View.ViewModels;

namespace View.Helpers
{
    public static class ModelToViewModel
    {
        public static UserViewModel ToUserViewModel(UserModel userModel)
        {
            return new UserViewModel(userModel.Username, userModel.EmailAddress, userModel.AccountType, userModel.Password);
        }

        public static GenreViewModel ToGenreViewModel(GenreModel genre)
        {
            return new GenreViewModel(genre.Genre, genre.GenreId);
        }

        public static IEnumerable<MovieViewModel> ToMovieViewModels(IEnumerable<MovieModel> movies, int maxStringLength)
        {
            List<MovieViewModel> movieViewModels = new List<MovieViewModel>();
            foreach (MovieModel movie in movies)
            {
                List<GenreViewModel> genresViewModels = movie.Genres.Select(ToGenreViewModel).ToList();
                movieViewModels.Add(new MovieViewModel(
                    movie.Title,
                    movie.Description,
                    movie.ReleaseDate,
                    MiscHelper.ShortenStringIfNecessary(movie.Description, maxStringLength),
                    genresViewModels,
                    movie.MovieId));
            }
            return movieViewModels;
        }
    }
}
