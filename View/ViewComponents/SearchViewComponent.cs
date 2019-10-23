using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using View.Controllers;
using View.ViewModels;

namespace View.ViewComponents
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly MovieLogic _movieLogic = new MovieLogic();
        private readonly MovieController _movieController = new MovieController();

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.Run<IViewComponentResult>(() =>
            {
                SearchViewModel searchViewModel = new SearchViewModel();
                foreach (GenreModel genre in _movieLogic.GetAllGenres())
                {
                    searchViewModel.AllGenres.Add(_movieController.ToGenreViewModel(genre));
                }

                return View("Search", searchViewModel);
            });
        }
    }
}
