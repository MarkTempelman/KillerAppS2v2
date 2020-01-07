using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;
using Enums;

namespace Data.Interfaces
{
    public interface IUserContext
    {
        void CreateUser(UserDTO user);
        UserDTO GetUserByUsername(string username);
        bool DoesUsernameExist(string username);
        bool DoesEmailAddressExist(string emailAddress);
        List<UserDTO> GetAllUsersExceptCurrent(int currentUserId);
        void DeleteUserById(int userId);
        void SetUserAccountType(int userId, AccountType accountType);
    }
}
