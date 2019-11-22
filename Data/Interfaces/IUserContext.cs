using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;

namespace Data.Interfaces
{
    public interface IUserContext
    {
        void CreateUser(UserDTO user);
        UserDTO GetUserByUsername(string username);
    }
}
