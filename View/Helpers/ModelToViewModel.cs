﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Logic.Models;
using View.ViewModels;

namespace View.Helpers
{
    public static class ModelToViewModel
    {
        public static UserViewModel ToUserViewModel(UserModel userModel)
        {
            return new UserViewModel(userModel.Username, userModel.EmailAddress, userModel.AccountType, userModel.Password, userModel.UserId);
        }

        public static GenreViewModel ToGenreViewModel(GenreModel genre)
        {
            return new GenreViewModel(genre.Genre, genre.GenreId);
        }

        public static IEnumerable<MovieViewModel> ToMovieViewModels(IEnumerable<MovieModel> movies)
        {
            List<MovieViewModel> movieViewModels = new List<MovieViewModel>();
            foreach (MovieModel movie in movies)
            {
                movieViewModels.Add(ToMovieViewModel(movie));
            }
            return movieViewModels;
        }

        public static MovieViewModel ToMovieViewModel(MovieModel movie)
        {
            string imagePath = null;
            if (movie.ImagePath != null)
            {
                imagePath = movie.ImagePath;
            }
            List<GenreViewModel> genresViewModels = movie.Genres.Select(ToGenreViewModel).ToList();
            MovieViewModel movieViewModel = new MovieViewModel(
                    movie.Title,
                    movie.Description,
                    movie.ReleaseDate,
                    MiscHelper.ShortenStringIfNecessary(movie.Description),
                    genresViewModels,
                    movie.MovieId)
                {
                    IsFavourite = movie.IsFavourite,
                    ImagePath = imagePath,
                    AverageRating = movie.AverageRating,
                    PersonalRating = new RatingViewModel(movie.MovieId, movie.PersonalRating)
                };
            return movieViewModel;
        }
    }
}
