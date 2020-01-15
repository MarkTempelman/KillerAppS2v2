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
            movies = AddAverageRatingToMovies(movies);
            movies = AddPersonalRatingToMovies(movies, userId);
            return movies;
        }

        private List<MovieModel> AddAverageRatingToMovies(List<MovieModel> movies)
        {
            foreach (var movie in movies)
            {
                movie.AverageRating = CalculateAverageOfRatings(_iRatingContext.GetAllRatingsFromMediaId(
                    _mediaLogic.GetMediaIdFromMovieId(movie.MovieId)).
                    Select(ToRatingModel).ToList());
            }
            return movies;
        }

        private List<MovieModel> AddPersonalRatingToMovies(List<MovieModel> movies, int userId)
        {
            foreach (var movie in movies)
            {
                var personalRating = _iRatingContext.GetPersonalRatingOfMedia(userId,
                    _mediaLogic.GetMediaIdFromMovieId(movie.MovieId));
                if (personalRating != null)
                {
                    movie.PersonalRating = personalRating.Rating;
                }
                else
                {
                    movie.PersonalRating = 0;
                }
            }
            return movies;
        }

        private double CalculateAverageOfRatings(List<RatingModel> ratingModels)
        {
            if (ratingModels.Count < 1)
            {
                return 0.0;
            }
            
            return Math.Round(ratingModels.Select(r => r.Rating).Average(), 1);
        }

        public void DeleteRatingByMediaId(int id)
        {
            _iRatingContext.DeleteRatingsByMediaId(id);
        }

        public void DeleteRatingByUserId(int id)
        {
            _iRatingContext.DeleteRatingsByUserId(id);
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
