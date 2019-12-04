using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;

namespace Logic
{
    public class MediaLogic
    {
        private readonly IMediaContext _iMediaContext;

        public MediaLogic(IMediaContext iMediaContext)
        {
            _iMediaContext = iMediaContext;
        }

        public int GetMediaIdFromMovieId(int movieId)
        {
            return _iMediaContext.GetMediaIdFromMovieId(movieId);
        }
    }
}
