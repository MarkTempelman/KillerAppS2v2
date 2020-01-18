using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using View.Helpers;
using View.ViewModels;

namespace View.Controllers
{
    public class RatingController : Controller
    {
        private RatingLogic _ratingLogic;
        private MediaLogic _mediaLogic;

        public RatingController(RatingLogic ratingLogic, MediaLogic mediaLogic)
        {
            _ratingLogic = ratingLogic;
            _mediaLogic = mediaLogic;
        }

        public ActionResult RateMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RateMovie(RatingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("MovieInfo", "Movie", new { id = model.MovieId });
            }
            model.UserId = MiscHelper.GetCurrentUserIdOrZero(this);
            RatingModel ratingModel = ViewModelToModel.ToRatingModel(model);
            ratingModel.MediaId = _mediaLogic.GetMediaIdFromMovieId(model.MovieId);
            _ratingLogic.RateMovie(ratingModel);
            return RedirectToAction("MovieInfo", "Movie", new { id = model.MovieId });
        }
    }
}