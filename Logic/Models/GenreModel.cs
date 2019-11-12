using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class GenreModel
    {
        public string Genre { get; set; }
        public int GenreId { get; set; }
        public int MovieId { get; set; }

        public GenreModel(string genre, int genreId)
        {
            Genre = genre;
            GenreId = genreId;
        }

        public GenreModel(int genreId)
        {
            GenreId = genreId;
        }
    }
}
