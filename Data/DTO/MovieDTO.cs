using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
    public class MovieDTO
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<GenreDTO> Genres { get; set; } = new List<GenreDTO>();
        public int MediaId { get; set; }
        public string ImagePath { get; set; }

        public MovieDTO(int movieId, string title, string description, DateTime releaseDate, int mediaId)
        {
            MovieId = movieId;
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
            MediaId = mediaId;
        }

        public MovieDTO()
        {

        }
    }
}
