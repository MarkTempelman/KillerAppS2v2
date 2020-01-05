using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
    public class GenreDTO
    {
        public string Genre { get; set; }
        public int GenreId { get; set; }
        public int MovieId { get; set; }

        public GenreDTO(string genre, int genreId)
        {
            Genre = genre;
            GenreId = genreId;
        }

        public GenreDTO(int genreId)
        {
            GenreId = genreId;
        }

        public GenreDTO(string genre)
        {
            Genre = genre;
        }
    }
}
