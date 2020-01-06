using System;
using System.Collections.Generic;
using System.Data;
using Data.DTO;
using Data.Interfaces;
using MySql.Data.MySqlClient;

namespace Data.SQLContext
{
    public class GenreSQLContext : IGenreContext
    {
        private readonly MySqlConnection _conn;

        public GenreSQLContext(string connectionString)
        {
            _conn = new MySqlConnection(connectionString);
        }

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

        public void AddGenreToMovie(GenreDTO genre)
        {
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand
                {
                    Connection = _conn,
                    CommandText = "INSERT INTO genre_movie(MovieId, GenreId) VALUES (@movieId, @genreId)"
                };

                command.Parameters.AddWithValue("@movieId", genre.MovieId);
                command.Parameters.AddWithValue("@genreId", genre.GenreId);

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

        public void CreateNewGenre(GenreDTO genre)
        {
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand
                {
                    Connection = _conn,
                    CommandText = "INSERT INTO genre(Genre) VALUES (@genre)"
                };

                command.Parameters.AddWithValue("@genre", genre.Genre);

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

        public bool DoesGenreExist(string genre)
        {
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM `genre` WHERE Genre = @genre",
                    _conn);

                command.Parameters.AddWithValue("@genre", genre);

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

        public void RemoveGenreById(int id)
        {
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM `genre_movie` WHERE GenreId = @genreId",
                    _conn);
                command.Parameters.AddWithValue("@genreId", id);
                command.ExecuteNonQuery();

                command = new MySqlCommand("DELETE FROM `genre` WHERE GenreId = @genreId",
                    _conn);
                command.Parameters.AddWithValue("@genreId", id);
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
