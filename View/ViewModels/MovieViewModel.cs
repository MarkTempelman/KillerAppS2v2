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
        public string ShortDescription { get; set; }

        public double AverageRating { get; set; }

        public MovieViewModel(string title, string description, DateTime releaseDate, string shortDescription)
        {
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
            ShortDescription = shortDescription;
        }

        public MovieViewModel()
        {

        }
    }
}