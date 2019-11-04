using System;
using System.Collections.Generic;
using System.Text;
using Models.Enums;

namespace Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public AccountType AccountType { get; set; } = AccountType.User;
        public string Password { get; set; }

        public UserModel()
        {

        }

        public UserModel(string username, string emailAddress, string password)
        {
            Username = username;
            EmailAddress = emailAddress;
            Password = password;
        }
    }
}
