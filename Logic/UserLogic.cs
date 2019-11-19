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

        public UserLogic(IUserContext iUserContext)
        {
            _iUserContext = iUserContext;
        }

        public void CreateUser(UserModel userModel)
        {
            userModel.Password = EncryptionLogic.EncryptPassword(userModel.Password);
            _iUserContext.CreateUser(ToUserDTO(userModel));
        }

        public UserModel CheckUserValidity(string username, string password)
        {
            UserModel user = ToUserModel(_iUserContext.GetUserByUsername(username));
            if (EncryptionLogic.ValidatePassword(password, user.Password))
            {
                return user;
            }

            return null;
        }

        public UserDTO ToUserDTO(UserModel userModel)
        {
            return new UserDTO(userModel.Username, userModel.EmailAddress, userModel.AccountType, userModel.Password);
        }

        public UserModel ToUserModel(UserDTO userDTO)
        {
            return new UserModel(userDTO.Username, userDTO.EmailAddress, userDTO.AccountType, userDTO.Password);
        }
    }
}
