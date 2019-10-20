using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class GenreModel
    {
        public string Genre { get; set; }
        public int MovieId { get; set; }

        public GenreModel(string genre, int movieId)
        {
            Genre = genre;
            MovieId = movieId;
        }
    }
}
