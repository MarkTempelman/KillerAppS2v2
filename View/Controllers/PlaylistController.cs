using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using View.Helpers;
using View.ViewModels;

namespace View.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly PlaylistLogic _playlistLogic;
        private readonly MovieLogic _movieLogic;

        public PlaylistController(PlaylistLogic playlistLogic, MovieLogic movieLogic)
        {
            _playlistLogic = playlistLogic;
            _movieLogic = movieLogic;
        }

        public IActionResult AddMovieToFavourites(int id)
        {
            _playlistLogic.AddMovieToFavourites(id, int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value));

            return RedirectToAction("Index", "Movie");
        }

        public IActionResult Favourites()
        {
            return View(ModelToViewModel.ToMovieViewModels(_movieLogic.GetMoviesFromFavourites(int.Parse(User.Claims
                .FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value))));
        }

        public IActionResult RemoveMovieFromFavourites(int id)
        {
            _playlistLogic.RemoveMovieFromFavourites(id, int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value));

            return RedirectToAction("Index", "Movie");
        }
    }
}