using System;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace Data.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public AccountType AccountType { get; set; }
        public string Password { get; set; }

        public UserDTO()
        {

        }

        public UserDTO(string username, string emailAddress, AccountType accountType, string password)
        {
            Username = username;
            EmailAddress = emailAddress;
            AccountType = accountType;
            Password = password;
        }

        public UserDTO(int userId, string username, string emailAddress, AccountType accountType)
        {
            UserId = userId;
            Username = username;
            EmailAddress = emailAddress;
            AccountType = accountType;
        }
    }
}
