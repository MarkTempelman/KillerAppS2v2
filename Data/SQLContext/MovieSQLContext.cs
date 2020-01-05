using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DTO;
using Data.Interfaces;
using Enums;
using MySql.Data.MySqlClient;

namespace Data.SQLContext
{
    public class MovieSQLContext : IMovieContext
    {
        private readonly MySqlConnection _conn;

        public MovieSQLContext(string connectionString)
        {
            _conn = new MySqlConnection(connectionString);
        }
        public IEnumerable<MovieDTO> GetAllMovies()
        {
            List<MovieDTO> movies = new List<MovieDTO>();
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
                    string imagePath = null;
                    if (!reader.IsDBNull(reader.GetOrdinal("ImagePath")))
                    {
                        imagePath = reader.GetString("ImagePath");
                    }
                    movies.Add(new MovieDTO(
                        reader.GetInt32(reader.GetOrdinal("MovieId")),
                        reader.GetString(reader.GetOrdinal("Title")),
                        reader.GetString(reader.GetOrdinal("Description")),
                        reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                        reader.GetInt32(reader.GetOrdinal("MediaId"))
                    )
                    {
                        ImagePath = imagePath
                    });
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

        public IEnumerable<MovieDTO> GetMoviesBySearchModel(SearchDTO search)
        {
            List<MovieDTO> movies = new List<MovieDTO>();
            try
            {
                MySqlCommand command = GetCommandFromSearchModel(search);
                command.Connection = _conn;
                
                _conn.Open();

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string imagePath = null;
                    if (!reader.IsDBNull(reader.GetOrdinal("ImagePath")))
                    {
                        imagePath = reader.GetString("ImagePath");
                    }
                    movies.Add(new MovieDTO(
                        reader.GetInt32(reader.GetOrdinal("MovieId")),
                        reader.GetString(reader.GetOrdinal("Title")),
                        reader.GetString(reader.GetOrdinal("Description")),
                        reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                        reader.GetInt32(reader.GetOrdinal("MediaId"))
                    )
                    {
                        ImagePath = imagePath
                    });
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

        private MySqlCommand GetCommandFromSearchModel(SearchDTO search)
        {
            MySqlCommand command = new MySqlCommand();
            string query = "SELECT * FROM `movie`, `genre_movie` WHERE movie.ReleaseDate BETWEEN @releasedAfter AND @releasedBefore ";

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

        public MovieDTO GetMovieById(int id)
        {
            MovieDTO movie = new MovieDTO();
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
                    string imagePath = null;
                    if (!reader.IsDBNull(reader.GetOrdinal("ImagePath")))
                    {
                        imagePath = reader.GetString("ImagePath");
                    }
                    movie = new MovieDTO(
                        reader.GetInt32(reader.GetOrdinal("MovieId")),
                        reader.GetString(reader.GetOrdinal("Title")),
                        reader.GetString(reader.GetOrdinal("Description")),
                        reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                        reader.GetInt32(reader.GetOrdinal("MediaId"))
                        
                    )
                    {
                        ImagePath = imagePath
                    };
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

        public void CreateNewMovie(MovieDTO movie)
        {
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_CreateNewMovie";

                command.Parameters.AddWithValue("@title", movie.Title);
                command.Parameters.AddWithValue("@description", movie.Description);
                command.Parameters.AddWithValue("@releaseDate", movie.ReleaseDate);
                command.Parameters.AddWithValue("@genreId", movie.Genres.First().GenreId);
                command.Parameters.AddWithValue("@imagePath", movie.ImagePath);

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

        public void EditMovie(MovieDTO movie)
        {
            try
            {
                _conn.Open();
                MySqlCommand command = new MySqlCommand("UPDATE movie " +
                                                        "SET Title = @title, Description = @description, ReleaseDate = @releaseDate " +
                                                        "WHERE MovieId = @movieId", _conn);

                command.Parameters.AddWithValue("@title", movie.Title);
                command.Parameters.AddWithValue("@description", movie.Description);
                command.Parameters.AddWithValue("@releaseDate", movie.ReleaseDate);
                command.Parameters.AddWithValue("@movieId", movie.MovieId);

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

        public MovieDTO GetMovieFromMediaId(int mediaId)
        {
            MovieDTO movie = new MovieDTO {MovieId = 0};
            try
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM movie WHERE MediaId = @mediaId",
                    _conn);

                command.Parameters.AddWithValue("@mediaId", mediaId);

                _conn.Open();

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    movie = new MovieDTO(
                        reader.GetInt32("MovieId"),
                        reader.GetString("Title"),
                        reader.GetString("Description"),
                        reader.GetDateTime("ReleaseDate"),
                        reader.GetInt32("MediaId")
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
            return movie;
        }
    }
}
