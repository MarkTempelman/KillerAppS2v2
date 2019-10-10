using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace KillerApp.ViewModels
{
    public class MovieViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double AverageRating { get; set; }

        public MovieViewModel(string title, string description, DateTime releaseDate)
        {
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
        }

        public MovieViewModel()
        {

        }
    }
}