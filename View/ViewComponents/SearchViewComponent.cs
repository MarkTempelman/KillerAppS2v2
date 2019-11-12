using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using View.Controllers;
using View.Helpers;
using View.ViewModels;

namespace View.ViewComponents
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly GenreLogic _genreLogic = new GenreLogic();
        private readonly MovieController _movieController = new MovieController();

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.Run<IViewComponentResult>(() =>
            {
                SearchViewModel searchViewModel = new SearchViewModel();
                foreach (GenreModel genre in _genreLogic.GetAllGenres())
                {
                    searchViewModel.AllGenres.Add(ModelToViewModel.ToGenreViewModel(genre));
                }

                return View("Search", searchViewModel);
            });
        }
    }
}
