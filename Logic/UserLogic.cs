using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;
using Data.Interfaces;
using Data.SQLContext;
using Models;

namespace Logic
{
    public class UserLogic
    {
        private readonly IUserContext _iUserContext;
        public UserLogic()
        {
            _iUserContext = new UserSQLContext();
        }

        public void CreateUser(UserModel userModel)
        {
            _iUserContext.CreateUser(ToUserDTO(userModel));
        }

        public UserModel CheckUserValidity(string username, string password)
        {
            return ToUserModel(_iUserContext.CheckUserValidity(username, password));
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
