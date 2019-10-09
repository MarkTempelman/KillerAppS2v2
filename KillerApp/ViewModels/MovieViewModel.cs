using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.ViewModels
{
    public class MovieViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double AverageRating { get; set; }
    }
}