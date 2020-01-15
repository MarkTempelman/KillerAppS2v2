using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using MySql.Data.MySqlClient;

namespace Data.SQLContext
{
    public class RatingSQLContext : IRatingContext
    {
        private readonly MySqlConnection _conn;

        public RatingSQLContext(string connectionString)
        {
            _conn = new MySqlConnection(connectionString);
        }
    }
}
