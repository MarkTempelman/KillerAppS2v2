using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Data.DTO;
using Data.Interfaces;
using Enums;
using MySql.Data.MySqlClient;

namespace Data.SQLContext
{
    public class UserSQLContext : IUserContext
    {
        private readonly MySqlConnection _conn;

        public UserSQLContext(string connectionString)
        {
            _conn = new MySqlConnection(connectionString);
        }

        public void CreateUser(UserDTO user)
        {
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand("sp_CreateUser",
                    _conn) {CommandType = CommandType.StoredProcedure};

                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@emailAddress", user.EmailAddress);
                command.Parameters.AddWithValue("@accountType", user.AccountType.ToString());
                command.Parameters.AddWithValue("@password", user.Password);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public UserDTO GetUserByUsername(string username)
        {
            try
            {
                UserDTO user = new UserDTO();

                MySqlCommand command = new MySqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetUserByUsername";
                command.Parameters.AddWithValue("@username", username);

                _conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user = GetUserFromReader(reader);
                }

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool DoesUsernameExist(string username)
        {
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM `user` WHERE Username = @username",
                    _conn);

                command.Parameters.AddWithValue("@username", username);

                var result = int.Parse(command.ExecuteScalar().ToString());
                return result > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool DoesEmailAddressExist(string emailAddress)
        {
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM `user` WHERE EmailAddress = @emailAddress",
                    _conn);

                command.Parameters.AddWithValue("@emailAddress", emailAddress);

                var result = int.Parse(command.ExecuteScalar().ToString());
                return result > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<UserDTO> GetAllUsersExceptCurrent(int currentUserId)
        {
            List<UserDTO> users = new List<UserDTO>();
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * " +
                                                        "FROM `user` WHERE UserId != @userId",
                    _conn);

                command.Parameters.AddWithValue("@userId", currentUserId);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(GetUserFromReader(reader));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
            return users;
        }

        public void DeleteUserById(int userId)
        {
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM user WHERE UserId = @userId",
                    _conn);
                command.Parameters.AddWithValue("@userId", userId);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void SetUserAccountType(int userId, AccountType accountType)
        {
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand("UPDATE user SET AccountType = @accountType WHERE UserId = @userId",
                    _conn);
                command.Parameters.AddWithValue("@accountType", accountType);
                command.Parameters.AddWithValue("@userId", userId);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        private UserDTO GetUserFromReader(MySqlDataReader reader)
        {
            return new UserDTO(
                reader.GetInt32("UserId"),
                reader.GetString("Username"),
                reader.GetString("EmailAddress"),
                (AccountType) Enum.Parse(typeof(AccountType),
                    reader.GetString(reader.GetOrdinal("AccountType"))))
            {
                Password = reader.GetString("Password")
            };
        }
    }
}
