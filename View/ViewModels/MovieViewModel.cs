using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
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
        [DisplayName("Release date")]
        public DateTime ReleaseDate { get; set; }
        public string ShortDescription { get; set; }
        public int AverageRating { get; set; }
        public List<GenreViewModel> Genres { get; set; }
        public int MovieId { get; set; }
        public List<GenreViewModel> AllGenres { get; set; } = new List<GenreViewModel>();
        [DisplayName("Genre")]
        public int GenreId { get; set; }
        public bool IsFavourite { get; set; }
        public string GenresString { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }

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