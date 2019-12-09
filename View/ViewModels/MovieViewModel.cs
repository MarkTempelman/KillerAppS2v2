using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using View.ViewModels;

namespace View.ViewModels
{
    public class MovieViewModel
    {
        [Required]
        [StringLength(45, ErrorMessage = "Title cannot be longer than 45 characters")]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string ShortDescription { get; set; }
        public double AverageRating { get; set; }
        public List<GenreViewModel> Genres { get; set; }
        public int MovieId { get; set; }
        public List<GenreViewModel> AllGenres { get; set; } = new List<GenreViewModel>();
        public int GenreId { get; set; }
        public bool IsFavourite { get; set; }
        public string GenresString { get; set; }

        public MovieViewModel(string title, string description, DateTime releaseDate, string shortDescription, List<GenreViewModel> genres, int movieId)
        {
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
            ShortDescription = shortDescription;
            Genres = genres;
            MovieId = movieId;
        }

        public MovieViewModel(string title, string description, DateTime releaseDate, string shortDescription, int movieId)
        {
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
            ShortDescription = shortDescription;
            MovieId = movieId;
        }

        public MovieViewModel()
        {

        }
    }
}