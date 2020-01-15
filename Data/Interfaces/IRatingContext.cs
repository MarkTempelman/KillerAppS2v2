using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;

namespace Data.Interfaces
{
    public interface IRatingContext
    {
        List<RatingDTO> GetAllRatingsFromMediaId(int id);
        RatingDTO GetPersonalRatingOfMedia(int userId, int mediaId);
        void DeleteRatingsByMediaId(int id);
        void DeleteRatingsByUserId(int id);
        void NewRating(RatingDTO rating);
        void UpdateRating(RatingDTO rating);
        bool DoesRatingExist(RatingDTO rating);
    }
}
