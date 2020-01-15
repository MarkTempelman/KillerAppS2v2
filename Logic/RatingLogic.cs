using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;
using Data.Interfaces;
using Logic.Models;

namespace Logic
{
    public class RatingLogic
    {
        private readonly IRatingContext _iRatingContext;

        public RatingLogic(IRatingContext iRatingContext)
        {
            _iRatingContext = iRatingContext;
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
