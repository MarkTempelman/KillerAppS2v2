using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class MovieModel
    {
        public int MovieId { get; set; } = -1;
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<GenreModel> Genres { get; set; } = new List<GenreModel>();
        public int MediaId { get; set; } = -1;
        public bool IsFavourite { get; set; } = false;
        public string ImagePath { get; set; }
        public int AverageRating { get; set; }
        public int PersonalRating { get; set; }

        public MovieModel(int movieId, string title, string description, DateTime releaseDate, int mediaId)
        {
            MovieId = movieId;
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
            MediaId = mediaId;
        }

        public MovieModel(string title, string description, DateTime releaseDate)
        {
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
        }

        public MovieModel()
        {

        }
    }
}
