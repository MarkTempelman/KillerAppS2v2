using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            _playlistLogic.AddMovieToFavourites(id, int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value));

            return RedirectToAction("Index", "Movie");
        }
    }
}