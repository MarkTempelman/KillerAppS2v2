using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;

namespace LogicTests.MemoryContext
{
    public class PlaylistMemoryContext : IPlaylistContext
    {
        public void AddMovieToPlaylist(int mediaId, int playlistId)
        {
            throw new NotImplementedException();
        }

        public int GetFavouritesPlaylistIdFromUserId(int userId)
        {
            switch (userId)
            {
                case 1:
                    return 1;
                case 2:
                    return 2;
                default:
                    return 0;
            }
        }

        public bool IsMediaInPlaylist(int mediaId, int playlistId)
        {
            if (mediaId == 1)
            {
                if (playlistId == 1)
                {
                    return true;
                }

                if (playlistId == 2)
                {
                    return false;
                }
            }

            if (mediaId == 2)
            {
                if (playlistId == 1)
                {
                    return true;
                }

                if (playlistId == 2)
                {
                    return true;
                }
            }

            return false;
        }

        public void RemoveMovieFromPlaylist(int mediaId, int playlistId)
        {
            throw new NotImplementedException();
        }
    }
}
