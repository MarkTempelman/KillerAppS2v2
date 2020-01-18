using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;
using Data.Interfaces;

namespace LogicTests.MemoryContext
{
    public class RatingMemoryContext : IRatingContext
    {
        public List<RatingDTO> GetAllRatingsFromMediaId(int id)
        {
            List<RatingDTO> rating = new List<RatingDTO>();
            return rating;
        }

        public RatingDTO GetPersonalRatingOfMedia(int userId, int mediaId)
        {
            throw new NotImplementedException();
        }

        public void DeleteRatingsByMediaId(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRatingsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public void NewRating(RatingDTO rating)
        {
            throw new NotImplementedException();
        }

        public void UpdateRating(RatingDTO rating)
        {
            throw new NotImplementedException();
        }

        public bool DoesRatingExist(RatingDTO rating)
        {
            throw new NotImplementedException();
        }
    }
}
