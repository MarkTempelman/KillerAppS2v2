using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;
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

        public List<RatingDTO> GetAllRatingsFromMediaId(int id)
        {
            List<RatingDTO> ratings = new List<RatingDTO>();
            try
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM rating WHERE MediaId = @mediaId", _conn);
                command.Parameters.AddWithValue("mediaId", id);
                _conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ratings.Add(new RatingDTO(
                        reader.GetInt32("UserId"),
                        reader.GetInt32("MediaId"),
                        reader.GetInt32("Rating")
                        ));
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

            return ratings;
        }

        public RatingDTO GetPersonalRatingOfMedia(int userId, int mediaId)
        {
            RatingDTO rating = null;
            try
            {
                MySqlCommand command = new MySqlCommand("SELECT * WHERE UserId = @userId AND MediaId = @mediaId", _conn);
                command.Parameters.AddWithValue("userId", userId);
                command.Parameters.AddWithValue("mediaId", mediaId);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    rating = new RatingDTO(
                        reader.GetInt32("UserId"),
                        reader.GetInt32("MediaId"),
                        reader.GetInt32("Rating")
                        );
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

            return rating;
        }

        public void DeleteRatingsByMediaId(int id)
        {
            try
            {
                MySqlCommand command = new MySqlCommand("DELETE FROM `rating` WHERE MediaId = @mediaId", _conn);
                command.Parameters.AddWithValue("mediaId", id);
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

        public void DeleteRatingsByUserId(int id)
        {
            try
            {
                MySqlCommand command = new MySqlCommand("DELETE FROM `rating` WHERE UserId = @userId", _conn);
                command.Parameters.AddWithValue("userId", id);
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
