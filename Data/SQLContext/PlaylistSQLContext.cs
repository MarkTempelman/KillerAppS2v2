using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using MySql.Data.MySqlClient;

namespace Data.SQLContext
{
    public class PlaylistSQLContext : IPlaylistContext
    {
        private readonly MySqlConnection _conn = SQLDatabaseConnection.GetConnection();
        public void AddMovieToPlaylist(int mediaId, int playlistId)
        {
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO media_playlist (PlaylistId, MediaId) VALUES (@playlistId, @mediaId)",
                    _conn);
                command.Parameters.AddWithValue("@mediaId", mediaId);
                command.Parameters.AddWithValue("@playlistId", playlistId);

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

        public int GetFavouritesPlaylistIdFromUserId(int userId)
        {
            int playlistId = -1;
            try
            {
               MySqlCommand command = new MySqlCommand("SELECT PlaylistId FROM playlist " +
                                                       "WHERE PlaylistName = 'Favourites' " +
                                                       "AND UserId = @userId", 
                    _conn);
                command.Parameters.AddWithValue("@userId", userId);

                _conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    playlistId = reader.GetInt32("PlaylistId");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _conn.Close();
            }
            return playlistId;
        }
    }
}
