using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Models;
using MySql.Data.MySqlClient;

namespace Data.SQLContext
{
    public class MovieSQLContext : IMovieContext
    {
        private readonly MySqlConnection _conn = SQLDatabaseConnection.GetConnection();

        private MySqlCommand CreateCommand(string query)
        {
            MySqlCommand command = new MySqlCommand(query, _conn);
            return command;
        }

        public IEnumerable<MovieModel> GetAllMovies()
        {
            List<MovieModel> movies = new List<MovieModel>();
            try
            {
                MySqlCommand command = CreateCommand("SELECT MovieId, Title, Description, ReleaseDate FROM movie");

                _conn.Open();

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    movies.Add(new MovieModel(
                        reader.GetInt32(reader.GetOrdinal("MovieId")),
                        reader.GetString(reader.GetOrdinal("Title")),
                        reader.GetString(reader.GetOrdinal("Description")),
                        reader.GetDateTime(reader.GetOrdinal("ReleaseDate"))
                    ));
                }
                _conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _conn.Close();
                throw;
            }
            return movies;
        }

        public IEnumerable<GenreModel> GetGenresByMovieId(int movieId)
        {
            List<GenreModel> genres = new List<GenreModel>();
            string query = "SELECT Genre, genre.GenreId FROM `genre`, `genre_movie` WHERE genre.GenreId = genre_movie.GenreId AND genre_movie.MovieId = @MovieId";
            MySqlCommand command = new MySqlCommand(query, _conn);
            try
            {
                command.Parameters.AddWithValue("@movieId", movieId);

                _conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    genres.Add(new GenreModel(
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

        public IEnumerable<GenreModel> GetAllGenres()
        {
            List<GenreModel> genres = new List<GenreModel>();
            string query = "SELECT Genre, GenreId FROM `genre`";
            MySqlCommand command = new MySqlCommand(query, _conn);
            try
            {
                _conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    genres.Add(new GenreModel(
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
