using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace View.ViewModels
{
    public class RatingViewModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        [Range(0, 100)]
        public int Rating { get; set; }

        public RatingViewModel()
        {

        }

        public RatingViewModel(int movieId, int rating)
        {
            MovieId = movieId;
            Rating = rating;
        }
    }
}
