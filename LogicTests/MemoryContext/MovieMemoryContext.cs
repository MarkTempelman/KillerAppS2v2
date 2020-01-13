using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.DTO;
using Data.Interfaces;
using Enums;
using Logic.Models;

namespace LogicTests.MemoryContext
{
    public class MovieMemoryContext : IMovieContext
    {
        public IEnumerable<MovieDTO> GetAllMovies()
        {
            IEnumerable<MovieDTO> movies = new List<MovieDTO>
            {
                new MovieDTO(1, "TestTitle2", "TestDescription1", new DateTime(2019, 10, 23),1),
                new MovieDTO(2, "TestTitle1", "TestDescription2", new DateTime(2019, 10, 15), 2)
            };
            return movies;
        }

        public MovieDTO GetMovieById(int id)
        {
            MovieDTO movie = new MovieDTO();
            if (id == 1)
            {
                movie = new MovieDTO(1, "TestTitle2", "TestDescription1", new DateTime(2019, 10, 23), 1);
            }
            else if (id == 2)
            {
                movie = new MovieDTO(2, "TestTitle1", "TestDescription2", new DateTime(2019, 10, 15), 2);
            }

            return movie;
        }

        public IEnumerable<MovieDTO> GetMoviesBySearchModel(SearchDTO search)
        {
            List<MovieDTO> movies = new List<MovieDTO>();
            movies.AddRange(GetAllMovies());
            IGenreContext iGenreContext = new GenreMemoryContext();
            foreach (var movie in movies)
            {
                movie.Genres
                    .AddRange(iGenreContext.GetGenresByMovieId(movie.MovieId));
            }

            if (search.Genre != null)
            {
                List<MovieDTO> newMovies = new List<MovieDTO>();
                foreach (var movie in movies)
                {
                    bool containsGenre = false;
                    foreach (var genre in movie.Genres.Where(genre => genre.GenreId == search.Genre.GenreId))
                    {
                        containsGenre = true;
                    }

                    if (containsGenre)
                    {
                        newMovies.Add(movie);
                    }
                }

                movies = newMovies;
            }

            movies = movies.Where(m => m.ReleaseDate > search.ReleasedAfter).ToList();
            movies = movies.Where(m => m.ReleaseDate < search.ReleasedBefore).ToList();

            if (search.SearchTerm != null)
            {
                movies = movies.Where(m => m.Title.ToLower().Contains(search.SearchTerm.ToLower())).ToList();
            }

            if (search.SortBy == SortBy.Title)
            {
                movies = movies.OrderBy(m => m.Title).ToList();
            }
            else if (search.SortBy == SortBy.Date)
            {
                movies = movies.OrderBy(m => m.ReleaseDate).ToList();
            }
            return movies;
        }

        public void CreateNewMovie(MovieDTO movie)
        {
            throw new NotImplementedException();
        }

        public void EditMovie(MovieDTO movie)
        {
            throw new NotImplementedException();
        }

        public MovieDTO GetMovieFromMediaId(int mediaId)
        {
            throw new NotImplementedException();
        }

        public void DeleteMovieById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
