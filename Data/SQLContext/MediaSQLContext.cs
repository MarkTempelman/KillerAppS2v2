using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using MySql.Data.MySqlClient;

namespace Data.SQLContext
{
    public class MediaSQLContext : IMediaContext
    {
        private readonly MySqlConnection _conn = SQLDatabaseConnection.GetConnection();
        public int GetMediaIdFromMovieId(int movieId)
        {
            try
            {
                int mediaId = -1;
                MySqlCommand command = new MySqlCommand("SELECT MediaId FROM movie WHERE MovieId = @movieId", _conn);
                command.Parameters.AddWithValue("@movieId", movieId);

                _conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    mediaId = reader.GetInt32("MediaId");
                }

                return mediaId;
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
