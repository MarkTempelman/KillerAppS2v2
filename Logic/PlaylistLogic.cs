﻿using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;

namespace Logic
{
    public class PlaylistLogic
    {
        private readonly IPlaylistContext _iPlaylistContext;
        private readonly IMediaContext _iMediaContext;

        public PlaylistLogic(IPlaylistContext iPlaylistContext, IMediaContext iMediaContext)
        {
            _iPlaylistContext = iPlaylistContext;
            _iMediaContext = iMediaContext;
        }

        public void AddMovieToFavourites(int movieId, int userId)
        {
            int playlistId = _iPlaylistContext.GetFavouritesPlaylistIdFromUserId(userId);
            int mediaId = _iMediaContext.GetMediaIdFromMovieId(movieId);
            _iPlaylistContext.AddMovieToPlaylist(mediaId, playlistId);
        }
    }
}
