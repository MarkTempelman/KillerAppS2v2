using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using View.ViewModels;
using Logic.Models;

namespace View.Helpers
{
    public static class ViewModelToModel
    {
        public static GenreModel ToGenreModel(GenreViewModel genreViewModel, GenreCollection genreCollection)
        {
            int genreId = 0;
            int movieId = 0;

            if (genreViewModel.GenreId > 0)
            {
                genreId = genreViewModel.GenreId;
            }

            if (genreViewModel.MovieId > 0)
            {
                movieId = genreViewModel.MovieId;
            }
            return genreCollection.CreateNewGenreModel(genreViewModel.Genre, genreId, movieId);
        }

        public static SearchModel ToSearchModel(SearchViewModel search, GenreCollection genreCollection)
        {
            var searchModel = new SearchModel();
            if (search.GenreId != -1)
            {
                searchModel.Genre = genreCollection.CreateNewGenreModel(null, search.GenreId, 0);
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

        public static MovieModel ToMovieModel(MovieViewModel movieViewModel, GenreCollection genreCollection)
        {
            MovieModel movieModel = new MovieModel(movieViewModel.Title, movieViewModel.Description, movieViewModel.ReleaseDate);
            movieModel.Genres.Add(genreCollection.CreateNewGenreModel(null, movieViewModel.GenreId, 0));
            movieModel.MovieId = movieViewModel.MovieId;
            movieModel.ImagePath = movieViewModel.ImagePath;
            return movieModel;
        }

        public static UserModel ToUserModel(UserViewModel userViewModel)
        {
            return new UserModel(userViewModel.Username, userViewModel.EmailAddress, userViewModel.Password);
        }

        public static RatingModel ToRatingModel(RatingViewModel ratingViewModel)
        {
            return new RatingModel(ratingViewModel.UserId, ratingViewModel.Rating);
        }
    }
}
