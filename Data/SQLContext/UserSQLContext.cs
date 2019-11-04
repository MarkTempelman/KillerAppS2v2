using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Data.DTO;
using Data.Interfaces;
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
                MySqlCommand command = new MySqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_CreateUser";

                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@emailAddress", user.EmailAddress);
                command.Parameters.AddWithValue("@accountType", user.AccountType);
                command.Parameters.AddWithValue("password", user.Password);

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
    }
}
