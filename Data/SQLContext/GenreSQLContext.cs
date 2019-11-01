using System;
using System.Collections.Generic;
using System.Data;
using Data.DTO;
using Data.Interfaces;
using Models;
using MySql.Data.MySqlClient;

namespace Data.SQLContext
{
    public class GenreSQLContext : IGenreContext
    {
        private readonly MySqlConnection _conn = SQLDatabaseConnection.GetConnection();

        public IEnumerable<GenreDTO> GetGenresByMovieId(int movieId)
        {
            List<GenreDTO> genres = new List<GenreDTO>();
            MySqlCommand command = new MySqlCommand();
            command.Connection = _conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetGenresByMovieId";
            try
            {
                command.Parameters.AddWithValue("@movieId", movieId);

                _conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    genres.Add(new GenreDTO(
                        reader.GetString(reader.GetOrdinal("Genre")),
                        reader.GetInt32(reader.GetOrdinal("GenreId"))));
                }
                _conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _conn.Close();
                throw;
            }
            return genres;
        }

        public IEnumerable<GenreDTO> GetAllGenres()
        {
            List<GenreDTO> genres = new List<GenreDTO>();
            MySqlCommand command = new MySqlCommand
            {
                Connection = _conn, CommandType = CommandType.StoredProcedure, CommandText = "sp_GetAllGenres"
            };

            try
            {
                _conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    genres.Add(new GenreDTO(
                        reader.GetString(reader.GetOrdinal("Genre")),
                        reader.GetInt32(reader.GetOrdinal("GenreId"))));
                }
                _conn.Close();
            }
            catch (Exception e)
            {
                _conn.Close();
                Console.WriteLine(e);
                throw;
            }
            return genres;
        }
    }
}
