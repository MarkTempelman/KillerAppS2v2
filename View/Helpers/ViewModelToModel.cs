using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using View.ViewModels;

namespace View.Helpers
{
    public static class ViewModelToModel
    {
        public static GenreModel ToGenreModel(GenreViewModel genreViewModel)
        {
            GenreModel genreModel = new GenreModel(genreViewModel.Genre, genreViewModel.GenreId);
            if (genreViewModel.MovieId > 0)
            {
                genreModel.MovieId = genreViewModel.MovieId;
            }

            return genreModel;
        }
    }
}
