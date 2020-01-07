using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;
using Data.Interfaces;
using Data.SQLContext;
using Logic.Models;
using Enums;
using Org.BouncyCastle.Security;

namespace Logic
{
    public class UserLogic
    {
        private readonly IUserContext _iUserContext;
        private readonly PlaylistLogic _playlistLogic;

        public UserLogic(IUserContext iUserContext, PlaylistLogic playlistLogic)
        {
            _iUserContext = iUserContext;
            _playlistLogic = playlistLogic;
        }

        public void CreateUser(UserModel userModel)
        {
            userModel.Password = EncryptionLogic.EncryptPassword(userModel.Password);
            _iUserContext.CreateUser(ToUserDTO(userModel));
        }

        public UserModel CheckUserValidity(string username, string password)
        {
            UserModel user = ToUserModel(_iUserContext.GetUserByUsername(username));
            if (user.UserId > 0)
            {
                if (EncryptionLogic.ValidatePassword(password, user.Password))
                {
                    return user;
                }
            }
            return null;
        }

        public bool DoesUserNameExist(string username)
        {
            return _iUserContext.DoesUsernameExist(username);
        }

        public bool DoesEmailAddressExist(string emailAddress)
        {
            return _iUserContext.DoesEmailAddressExist(emailAddress);
        }

        public List<UserModel> GetAllUsersExceptCurrent(int userId)
        {
            List<UserModel> users = new List<UserModel>();

            foreach (var user in _iUserContext.GetAllUsersExceptCurrent(userId))
            {
                users.Add(ToUserModel(user));
            }

            return users;
        }

        public void DeleteUser(int userId)
        {
            _playlistLogic.DeleteAllPlaylistsOfUser(userId);
            _iUserContext.DeleteUserById(userId);
        }

        public void SetUserAccountType(int userId, AccountType accountType)
        {
            _iUserContext.SetUserAccountType(userId, accountType);
        }

        public UserDTO ToUserDTO(UserModel userModel)
        {
            return new UserDTO(userModel.Username, userModel.EmailAddress, userModel.AccountType, userModel.Password);
        }

        public UserModel ToUserModel(UserDTO userDTO)
        {
            return new UserModel(userDTO.Username, userDTO.EmailAddress, userDTO.AccountType, userDTO.Password, userDTO.UserId);
        }
    }
}
