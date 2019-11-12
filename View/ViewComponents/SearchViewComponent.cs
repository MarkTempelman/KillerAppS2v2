using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;
using View.Controllers;
using View.Helpers;
using View.ViewModels;

namespace View.ViewComponents
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly GenreLogic _genreLogic = new GenreLogic();

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
