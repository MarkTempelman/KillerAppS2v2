using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Enums;
using Renci.SshNet;

namespace View.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Your username must be at least 3 characters long")]
        [MaxLength(45, ErrorMessage = "Your username cannot be longer than 45 characters")]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^(?:[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?)$", 
            ErrorMessage = "Please enter a valid email address")]
        [DisplayName("Email address")]
        public string EmailAddress { get; set; }
        public AccountType AccountType { get; set; }
        [Required]
        public string Password { get; set; }
        public int Id { get; set; }

        public UserViewModel()
        {

        }

        public UserViewModel(string username, string emailAddress, AccountType accountType, string password, int id)
        {
            Username = username;
            EmailAddress = emailAddress;
            AccountType = accountType;
            Password = password;
            Id = id;
        }
    }
}
