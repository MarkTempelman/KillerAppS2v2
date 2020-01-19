using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using BCrypt.Net;

namespace Logic
{
    public static class EncryptionLogic
    {
        public static string EncryptPassword(string submittedPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(submittedPassword);
        }

        public static bool ValidatePassword(string submittedPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(submittedPassword, hashedPassword);
        }
    }
}
