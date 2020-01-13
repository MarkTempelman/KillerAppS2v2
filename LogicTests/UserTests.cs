using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Logic;
using LogicTests.MemoryContext;
using NUnit.Framework;

namespace LogicTests
{
    public class UserTests
    {
        private IUserContext _iUserContext;
        private UserLogic _userLogic;
        private PlaylistLogic _playlistLogic;
        private IMediaContext _mediaContext;
        private IPlaylistContext _playlistContext;
        
        [SetUp]
        public void SetUp()
        {
            _iUserContext = new UserMemoryContext();
            _mediaContext = new MediaMemoryContext();
            _playlistContext = new PlaylistMemoryContext();
            _playlistLogic = new PlaylistLogic(_playlistContext, _mediaContext);
            _userLogic = new UserLogic(_iUserContext, _playlistLogic);
        }

        [Test]
        public void CheckUserValidity_CorrectUsernameAndPassword_ReturnsValidUserModel()
        {
            var expected = 1;
            var actual = _userLogic.CheckUserValidity("Admin", "admin").UserId;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CheckUserValidity_WrongPassword_ReturnsNull()
        {
            var actual = _userLogic.CheckUserValidity("Admin", "aefae");

            Assert.IsNull(actual);
        }

        [Test]
        public void CheckUserValidity_NonExistantUser_ReturnsNull()
        {
            var actual = _userLogic.CheckUserValidity("aefaef", "password");

            Assert.IsNull(actual);
        }

        [Test]
        public void DoesUserNameExist_ExistingUserName_ReturnsTrue()
        {
            var actual = _userLogic.DoesUserNameExist("Admin");

            Assert.IsTrue(actual);
        }

        [Test]
        public void DoesUserNameExist_NonExistingUserName_ReturnsFalse()
        {
            var actual = _userLogic.DoesUserNameExist("Admin2");

            Assert.IsFalse(actual);
        }

        [Test]
        public void DoesEmailAddressExist_ExistingEmail_ReturnsTrue()
        {
            var actual = _userLogic.DoesEmailAddressExist("admin@admin.com");

            Assert.IsTrue(actual);
        }

        [Test]
        public void DoesEmailAddressExist_NonExistingEmail_ReturnsFalse()
        {
            var actual = _userLogic.DoesEmailAddressExist("admin2@admin.com");

            Assert.IsFalse(actual);
        }
    }
}
