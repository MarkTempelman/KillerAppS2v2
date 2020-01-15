using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace View.ViewModels
{
    public class RatingViewModel
    {
        public int UserId { get; set; }
        public int MediaId { get; set; }
        public double Rating { get; set; }

        public RatingViewModel(int userId, int mediaId, double rating)
        {
            UserId = userId;
            MediaId = mediaId;
            Rating = rating;
        }
    }
}
