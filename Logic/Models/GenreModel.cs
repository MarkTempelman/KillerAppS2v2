using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class GenreModel: IEquatable<GenreModel>
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

        public GenreModel(string genre)
        {
            Genre = genre;
        }

        public bool Equals(GenreModel otherGenre)
        {
            if (otherGenre is null)
                return false;
            return GenreId == otherGenre.GenreId;
        }

        public override bool Equals(object obj) => Equals(obj as GenreModel);
        public override int GetHashCode() => (GenreId).GetHashCode();
    }
}
