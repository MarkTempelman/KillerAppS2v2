using System;
using System.Collections.Generic;
using System.Text;
using Models.Enums;

namespace Data.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public AccountType AccountType { get; set; }
        public string Password { get; set; }
    }
}
