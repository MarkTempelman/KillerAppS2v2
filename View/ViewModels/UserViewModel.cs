using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Enums;

namespace View.ViewModels
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public AccountType AccountType { get; set; }
        public string Password { get; set; }
    }
}
