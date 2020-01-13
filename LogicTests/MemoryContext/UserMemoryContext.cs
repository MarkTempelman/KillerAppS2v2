using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;
using Data.Interfaces;
using Enums;

namespace LogicTests.MemoryContext
{
    public class UserMemoryContext : IUserContext
    {
        public void CreateUser(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetUserByUsername(string username)
        {
            UserDTO user = new UserDTO();
            if (username == "Admin")
            {
                user = new UserDTO(
                    "Admin",
                    "admin@admin.com",
                    AccountType.Admin,
                    "$2b$10$7iFKR8mNWQ8TPiOYYq1pzeBM.TQ4A9NMQl0iM7MVocQsNCUyhBgkS"
                )
                {
                    UserId = 1
                };
            }
            if (username == "User")
            {
                user = new UserDTO(
                    "User",
                    "user@user.com",
                    AccountType.User,
                    "$2b$10$GKMANNUgilRJ39yD4BjQwOq5w3OETc7rMkeeLadjfFbKTdhNRP78C"
                )
                {
                    UserId = 2
                };
            }

            return user;
        }

        public bool DoesUsernameExist(string username)
        {
            return username == "Admin" || username == "User";
        }

        public bool DoesEmailAddressExist(string emailAddress)
        {
            return emailAddress == "admin@admin.com" || emailAddress == "user@user.com";
        }

        public List<UserDTO> GetAllUsersExceptCurrent(int currentUserId)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public void SetUserAccountType(int userId, AccountType accountType)
        {
            throw new NotImplementedException();
        }
    }
}
