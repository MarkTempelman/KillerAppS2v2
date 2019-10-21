﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class GenreModel
    {
        public string Genre { get; set; }
        public int GenreId { get; set; }

        public GenreModel(string genre, int genreId)
        {
            Genre = genre;
            GenreId = genreId;
        }
    }
}
