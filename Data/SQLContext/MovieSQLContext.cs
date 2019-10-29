using System;
using System.Collections.Generic;
using System.Data;
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
            

            return movies;
        }

        private string GetQueryFromSearchModel(SearchModel search)
        {
            MySqlCommand command = new MySqlCommand();
            string query ="SELECT movie.MovieId, movie.Title, movie.Description, movie.ReleaseDate, movie.MediaId FROM `movie`, `genre_movie` ";

            query+= "WHERE movie.ReleaseDate BETWEEN @releasedAfter AND @releasedBefore ";
            command.CommandText = query;
            command.Parameters.AddWithValue("@releasedAfter", search.ReleasedAfter);
            command.Parameters.AddWithValue("@releasedBefore", search.ReleasedBefore);

            if (search.SearchTerm != null)
            {
                query += "AND movie.Title LIKE %@title% ";
            }

            if (search.Genre != null)
            {
                query += "AND movie.MovieId = genre_movie.MovieId AND genre_movie.GenreId = @genreId ";
            }

            if (search.SortBy == SortBy.Title)
            {
                query += "ORDER BY movie.Title ASC";
            }
            else if (search.SortBy == SortBy.Date)
            {
                query += "ORDER BY movie.ReleaseDate ASC";
            }

            return query;
        }

        public IEnumerable<GenreModel> GetGenresByMovieId(int movieId)
        {
            List<GenreModel> genres = new List<GenreModel>();
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
            MySqlCommand command = new MySqlCommand();
            command.Connection = _conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetAllGenres";

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
