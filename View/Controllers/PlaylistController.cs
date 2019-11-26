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
        private PlaylistLogic _playlistLogic;

        public PlaylistController(PlaylistLogic playlistLogic)
        {
            _playlistLogic = playlistLogic;
        }

        [HttpPost]
        public IActionResult AddMovieToFavourites(int id)
        {
            

            return RedirectToAction("Index", "Movie");
        }
    }
}