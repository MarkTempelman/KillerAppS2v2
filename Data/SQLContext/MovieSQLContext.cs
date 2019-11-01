using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Models;
using Models.Enums;
using MySql.Data.MySqlClient;

namespace Data.SQLContext
{
    public class MovieSQLContext : IMovieContext
    {
        private readonly MySqlConnection _conn = SQLDatabaseConnection.GetConnection();

        public IEnumerable<MovieModel> GetAllMovies()
        {
            List<MovieModel> movies = new List<MovieModel>();
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllMovies";

                _conn.Open();

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    movies.Add(new MovieModel(
                        reader.GetInt32(reader.GetOrdinal("MovieId")),
                        reader.GetString(reader.GetOrdinal("Title")),
                        reader.GetString(reader.GetOrdinal("Description")),
                        reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                        reader.GetInt32(reader.GetOrdinal("MediaId"))
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

        public IEnumerable<MovieModel> GetMoviesBySearchModel(SearchModel search)
        {
            List<MovieModel> movies = new List<MovieModel>();
            try
            {
                MySqlCommand command = GetCommandFromSearchModel(search);
                command.Connection = _conn;
                
                _conn.Open();

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    movies.Add(new MovieModel(
                        reader.GetInt32(reader.GetOrdinal("MovieId")),
                        reader.GetString(reader.GetOrdinal("Title")),
                        reader.GetString(reader.GetOrdinal("Description")),
                        reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                        reader.GetInt32(reader.GetOrdinal("MediaId"))
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

        private MySqlCommand GetCommandFromSearchModel(SearchModel search)
        {
            MySqlCommand command = new MySqlCommand();
            string query ="SELECT movie.MovieId, movie.Title, movie.Description, movie.ReleaseDate, movie.MediaId FROM `movie`, `genre_movie` ";

            query+= "WHERE movie.ReleaseDate BETWEEN @releasedAfter AND @releasedBefore ";
            command.CommandText = query;
            command.Parameters.AddWithValue("@releasedAfter", search.ReleasedAfter);
            command.Parameters.AddWithValue("@releasedBefore", search.ReleasedBefore);

            if (search.SearchTerm != null)
            {
                query += "AND movie.Title LIKE @searchTerm ";
                command.CommandText = query;
                command.Parameters.AddWithValue("@searchTerm", "%" + search.SearchTerm + "%");
            }

            if (search.Genre != null)
            {
                query += "AND movie.MovieId = genre_movie.MovieId AND genre_movie.GenreId = @genreId ";
                command.CommandText = query;
                command.Parameters.AddWithValue("@genreId", search.Genre.GenreId);
            }

            query += "GROUP BY movie.MovieId ";

            if (search.SortBy == SortBy.Title)
            {
                query += "ORDER BY movie.Title ASC";
            }
            else if (search.SortBy == SortBy.Date)
            {
                query += "ORDER BY movie.ReleaseDate ASC";
            }
            command.CommandText = query;

            return command;
        }

        public MovieModel GetMovieById(int id)
        {
            MovieModel movie = new MovieModel();
            MySqlCommand command = new MySqlCommand();
            command.Connection = _conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetMovieById";
            command.Parameters.AddWithValue("@movieId", id);

            try
            {
                _conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    movie = new MovieModel(
                        reader.GetInt32(reader.GetOrdinal("MovieId")),
                        reader.GetString(reader.GetOrdinal("Title")),
                        reader.GetString(reader.GetOrdinal("Description")),
                        reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                        reader.GetInt32(reader.GetOrdinal("MediaId"))
                    );
                }
                _conn.Close();
                if (movie.Title != null)
                {
                    return movie;
                }

                return null;
            }
            catch (Exception e)
            {
                _conn.Close();
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
