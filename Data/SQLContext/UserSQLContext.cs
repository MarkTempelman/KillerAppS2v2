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
        private readonly MySqlConnection _conn = SQLDatabaseConnection.GetConnection();

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
                    user = new UserDTO(
                        reader.GetString(reader.GetOrdinal("Username")),
                        reader.GetString(reader.GetOrdinal("EmailAddress")),
                        (AccountType) Enum.Parse(typeof(AccountType),
                            reader.GetString(reader.GetOrdinal("AccountType"))),
                        reader.GetString(reader.GetOrdinal("Password"))
                    ) {UserId = reader.GetInt32("UserId")};
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
    }
}
