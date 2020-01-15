using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class RatingModel
    {
        public int UserId { get; set; }
        public int MediaId { get; set; }
        public double Rating { get; set; }

        public RatingModel(int userId, int mediaId, double rating)
        {
            UserId = userId;
            MediaId = mediaId;
            Rating = rating;
        }
    }
}
