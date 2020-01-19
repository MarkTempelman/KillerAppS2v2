using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;
using Data.Interfaces;

namespace Logic.Models
{
    public class GenreModel: IEquatable<GenreModel>
    {
        private IGenreContext _iGenreContext;
        public string Genre { get; set; }
        public int GenreId { get; set; }
        public int MovieId { get; set; }

        public GenreModel(string genre, int genreId, int movieId, IGenreContext iGenreContext)
        {
            Genre = genre;
            GenreId = genreId;
            MovieId = movieId;
            _iGenreContext = iGenreContext;
        }

        public void AddGenreToMovie()
        {
            _iGenreContext.AddGenreToMovie(ToGenreDTO());
        }

        public bool TryCreateNewGenre()
        {
            if (!_iGenreContext.DoesGenreExist(Genre))
            {
                _iGenreContext.CreateNewGenre(ToGenreDTO());
                return true;
            }

            return false;
        }

        public GenreDTO ToGenreDTO()
        {
            GenreDTO genreDTO = new GenreDTO(Genre);
            if (MovieId > 0)
            {
                genreDTO.MovieId = MovieId;
            }

            if (GenreId > 0)
            {
                genreDTO.GenreId = GenreId;
            }
            return genreDTO;
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
