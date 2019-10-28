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
                MySqlCommand command = CreateCommand("SELECT * FROM movie");

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

        public MovieModel GetMovieById(int id)
        {
            MovieModel movie = new MovieModel();
            string query = "SELECT * FROM movie WHERE movie.MovieId = @movieId";
            MySqlCommand command = new MySqlCommand(query, _conn);
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
