using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IMediaContext
    {
        int GetMediaIdFromMovieId(int movieId);
    }
}
