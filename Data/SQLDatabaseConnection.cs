using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Data
{
    class SQLDatabaseConnection
    {
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection("Server=studmysql01.fhict.local;Uid=dbi415127;Database=dbi415127;Pwd=test1234;");
        }
    }
}
