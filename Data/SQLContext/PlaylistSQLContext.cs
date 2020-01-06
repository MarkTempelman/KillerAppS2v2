using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using MySql.Data.MySqlClient;

namespace Data.SQLContext
{
    public class PlaylistSQLContext : IPlaylistContext
    {
        private readonly MySqlConnection _conn;

        public PlaylistSQLContext(string connectionString)
        {
            _conn = new MySqlConnection(connectionString);
        }
        public void AddMovieToPlaylist(int mediaId, int playlistId)
        {
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO media_playlist (PlaylistId, MediaId) " +
                                                        "VALUES (@playlistId, @mediaId)",
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

        public bool IsMediaInPlaylist(int mediaId, int playlistId)
        {
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM `media_playlist` " +
                                                        "WHERE MediaId = @mediaId AND PlaylistId = @playlistId",
                    _conn);

                command.Parameters.AddWithValue("@mediaId", mediaId);
                command.Parameters.AddWithValue("@playlistId", playlistId);

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

        public void RemoveMovieFromPlaylist(int mediaId, int playlistId)
        {
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM media_playlist WHERE PlaylistId = @playlistId AND MediaId = @mediaId",
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

        public void RemoveMovieFromAllPlaylists(int mediaId)
        {
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM media_playlist WHERE MediaId = @mediaId",
                    _conn);
                command.Parameters.AddWithValue("@mediaId", mediaId);

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
