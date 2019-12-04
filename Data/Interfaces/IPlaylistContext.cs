﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IPlaylistContext
    {
        void AddMovieToPlaylist(int mediaId, int playlistId);
        int GetFavouritesPlaylistIdFromUserId(int userId);
        bool IsMediaInPlaylist(int mediaId, int playlistId);
    }
}
