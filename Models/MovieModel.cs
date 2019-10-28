using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MovieModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<GenreModel> Genres { get; set; } = new List<GenreModel>();
        public int MediaId { get; set; }

        public MovieModel(int movieId, string title, string description, DateTime releaseDate, int mediaId)
        {
            MovieId = movieId;
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
            MediaId = mediaId;
        }
    }
}
