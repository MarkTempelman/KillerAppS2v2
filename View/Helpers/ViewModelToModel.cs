using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using View.ViewModels;
using Logic.Models;

namespace View.Helpers
{
    public static class ViewModelToModel
    {
        public static GenreModel ToGenreModel(GenreViewModel genreViewModel)
        {
            GenreModel genreModel = new GenreModel(genreViewModel.Genre);
            if (genreViewModel.MovieId > 0)
            {
                genreModel.MovieId = genreViewModel.MovieId;
            }

            if (genreViewModel.GenreId > 0)
            {
                genreModel.GenreId = genreViewModel.GenreId;
            }
            return genreModel;
        }

        public static SearchModel ToSearchModel(SearchViewModel search)
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

        public static MovieModel ToMovieModel(MovieViewModel movieViewModel)
        {
            MovieModel movieModel = new MovieModel(movieViewModel.Title, movieViewModel.Description, movieViewModel.ReleaseDate);
            movieModel.Genres.Add(new GenreModel(movieViewModel.GenreId));
            movieModel.MovieId = movieViewModel.MovieId;
            movieModel.ImagePath = movieViewModel.ImagePath;
            return movieModel;
        }

        public static UserModel ToUserModel(UserViewModel userViewModel)
        {
            return new UserModel(userViewModel.Username, userViewModel.EmailAddress, userViewModel.Password);
        }
    }
}
