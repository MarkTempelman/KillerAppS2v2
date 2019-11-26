using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;

namespace View.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly PlaylistLogic _playlistLogic;

        public PlaylistController(PlaylistLogic playlistLogic)
        {
            _playlistLogic = playlistLogic;
        }

        public IActionResult AddMovieToFavourites(int id)
        {
            //hardcoded userId is a place holder
            _playlistLogic.AddMovieToFavourites(id, 12);

            return RedirectToAction("Index", "Movie");
        }
    }
}