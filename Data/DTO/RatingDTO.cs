using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
    public class RatingDTO
    {
        public int UserId { get; set; }
        public int MediaId { get; set; }
        public int Rating { get; set; }

        public RatingDTO()
        {

        }

        public RatingDTO(int userId, int mediaId, int rating)
        {
            UserId = userId;
            MediaId = mediaId;
            Rating = rating;
        }
    }
}
