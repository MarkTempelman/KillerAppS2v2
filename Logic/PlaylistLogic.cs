using System;
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

        public void RemoveMovieFromFavourites(int movieId, int userId)
        {
            _iPlaylistContext.RemoveMovieFromPlaylist(_iMediaContext.GetMediaIdFromMovieId(movieId),
                GetPlaylistIdFromUserId(userId));
        }

        public List<int> GetMediaIdsFromFavourites(int userId)
        {
            return _iMediaContext.GetMediaIdsFromPlaylistId(
                _iPlaylistContext.GetFavouritesPlaylistIdFromUserId(userId));
        }

        public int GetPlaylistIdFromUserId(int userId)
        {
            return _iPlaylistContext.GetFavouritesPlaylistIdFromUserId(userId);
        }

        public bool IsMediaInPlaylist(int mediaId, int playlistId)
        {
            return _iPlaylistContext.IsMediaInPlaylist(mediaId, playlistId);
        }
    }
}
