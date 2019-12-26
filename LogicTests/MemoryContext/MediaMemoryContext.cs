using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;

namespace LogicTests.MemoryContext
{
    public class MediaMemoryContext : IMediaContext
    {
        public int GetMediaIdFromMovieId(int movieId)
        {
            switch (movieId)
            {
                case 1:
                    return 1;
                case 2:
                    return 2;
                default:
                    return 0;
            }
        }

        public List<int> GetMediaIdsFromPlaylistId(int playlistId)
        {
            if (playlistId == 1)
            {
                return new List<int>{1};
            }

            if (playlistId == 2)
            {
                return new List<int>{1, 2};
            }
            return new List<int>();
        }
    }
}
