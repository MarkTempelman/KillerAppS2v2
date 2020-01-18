using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.DTO;
using Data.Interfaces;
using Logic.Models;

namespace Logic
{
    public class RatingLogic
    {
        private readonly IRatingContext _iRatingContext;
        private readonly MediaLogic _mediaLogic;

        public RatingLogic(IRatingContext iRatingContext, MediaLogic mediaLogic)
        {
            _iRatingContext = iRatingContext;
            _mediaLogic = mediaLogic;
        }

        public List<MovieModel> AddRatingsToMovies(List<MovieModel> movies, int userId)
        {
            foreach (var movie in movies)
            {
                movie.AverageRating = GetAverageRatingForMovie(movie);
                
                movie.PersonalRating = GetPersonalRatingForMovie(movie, userId);
            }
            return movies;
        }

        public MovieModel AddRatingsToMovie(MovieModel movie, int userId)
        {
            movie.AverageRating = GetAverageRatingForMovie(movie);
            movie.PersonalRating = GetPersonalRatingForMovie(movie, userId);
            return movie;
        }

        private int GetAverageRatingForMovie(MovieModel movie)
        {
            return CalculateAverageOfRatings(
                _iRatingContext.GetAllRatingsFromMediaId(_mediaLogic.GetMediaIdFromMovieId(movie.MovieId))
                    .Select(ToRatingModel).ToList());
        }

        private int GetPersonalRatingForMovie(MovieModel movie, int userId)
        {
            if (userId > 0)
            {
                var personalRating = _iRatingContext.GetPersonalRatingOfMedia(userId,
                    _mediaLogic.GetMediaIdFromMovieId(movie.MovieId));
                if (personalRating != null)
                {
                    return personalRating.Rating;
                }
            }
            return 0;
        }

        private int CalculateAverageOfRatings(List<RatingModel> ratingModels)
        {
            if (ratingModels.Count < 1)
            {
                return 0;
            }
            
            return Convert.ToInt32(Math.Round(ratingModels.Select(r => r.Rating).Average(), 1));
        }

        public void DeleteRatingByMediaId(int id)
        {
            _iRatingContext.DeleteRatingsByMediaId(id);
        }

        public void DeleteRatingByUserId(int id)
        {
            _iRatingContext.DeleteRatingsByUserId(id);
        }

        public void RateMovie(RatingModel rating)
        {
            RatingDTO ratingDTO = ToRatingDTO(rating);
            if (_iRatingContext.DoesRatingExist(ratingDTO))
            {
                _iRatingContext.UpdateRating(ratingDTO);
            }
            else
            {
                _iRatingContext.NewRating(ratingDTO);
            }
        }

        public RatingDTO ToRatingDTO(RatingModel ratingModel)
        {
            return new RatingDTO(ratingModel.UserId, ratingModel.MediaId, ratingModel.Rating);
        }

        public RatingModel ToRatingModel(RatingDTO ratingDTO)
        {
            return new RatingModel(ratingDTO.UserId, ratingDTO.MediaId, ratingDTO.Rating);
        }
    }
}
