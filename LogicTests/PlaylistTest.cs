using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Data.Interfaces;
using Logic;
using LogicTests.MemoryContext;
using NUnit.Framework;

namespace LogicTests
{
    public class PlaylistTest
    {
        private IPlaylistContext _iPlaylistContext;
        private IMediaContext _iMediaContext;
        private PlaylistLogic _playlistLogic;

        [SetUp]
        public void SetUp()
        {
            _iPlaylistContext = new PlaylistMemoryContext();
            _iMediaContext = new MediaMemoryContext();
            _playlistLogic = new PlaylistLogic(_iPlaylistContext, _iMediaContext);
        }

        [Test]
        public void GetMediaIdsFromFavourites_ExistingUser_ReturnsIntList()
        {
            var expected = new List<int>{1};
            var actual = _playlistLogic.GetMediaIdsFromFavourites(1);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetMediaIdsFromFavourites_NonExistingUser_ReturnsEmptyList()
        {
            var expected = new List<int>();
            var actual = _playlistLogic.GetMediaIdsFromFavourites(3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetPlaylistIdFromUserId_ExistingUser_ReturnsInt()
        {
            var expected = 1;
            var actual = _playlistLogic.GetPlaylistIdFromUserId(1);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetPlaylistIdFromUserId_NonExistingUser_ReturnsInt()
        {
            var expected = 0;
            var actual = _playlistLogic.GetPlaylistIdFromUserId(3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsMediaInPlaylist_MediaIsInPlaylist_ReturnsTrue()
        {
            var expected = true;
            var actual = _playlistLogic.IsMediaInPlaylist(1, 1);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsMediaInPlaylist_MediaIsNotInPlaylist_ReturnsFalse()
        {
            var expected = false;
            var actual = _playlistLogic.IsMediaInPlaylist(3, 1);
            Assert.AreEqual(expected, actual);
        }
    }
}
