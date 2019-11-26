using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;

namespace Logic
{
    public class PlaylistLogic
    {
        private IPlaylistContext _iPlaylistContext;
        public PlaylistLogic(IPlaylistContext iPlaylistContext)
        {
            _iPlaylistContext = iPlaylistContext;
        }
    }
}
