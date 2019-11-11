using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace View.ViewModels
{
    public class GenreViewModel
    {
        public string Genre { get; set; }
        public int GenreId { get; set; }
        public List<GenreViewModel> AllGenres { get; set; } = new List<GenreViewModel>();
        public int MovieId { get; set; }

        public GenreViewModel(string genre, int genreId)
        {
            Genre = genre;
            GenreId = genreId;
        }

        public GenreViewModel()
        {

        }
    }
}
