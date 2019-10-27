﻿using System;
using System.Collections.Generic;
using System.Linq;
using Data.Interfaces;
using Data.SQLContext;
using Models;

namespace Logic
{
    public class MovieLogic
    {
        private readonly IMovieContext _iMovieContext;

        public MovieLogic()
        {
            _iMovieContext = new MovieSQLContext();
        }

        public MovieLogic(IMovieContext movieContext)
        {
            _iMovieContext = movieContext;
        }

        public IEnumerable<GenreModel> GetAllGenres() 
        {
            return _iMovieContext.GetAllGenres();
        }
        public IEnumerable<MovieModel> GetAllMovies()
        {
            return GetGenresForMovies(_iMovieContext.GetAllMovies());
        }

        private IEnumerable<MovieModel> GetGenresForMovies(IEnumerable<MovieModel> movies)
        {
            foreach (MovieModel movie in movies)
            {
                movie.Genres.AddRange(_iMovieContext.GetGenresByMovieId(movie.MovieId));
            }
            return movies;
        }

        public IEnumerable<MovieModel> GetMoviesBySearchModel(IEnumerable<MovieModel> movies, SearchModel search)
        {
            if (search.Genre != null)
            {
                movies = FilterMoviesByGenre(movies, search.Genre);
            }

            movies = GetMoviesReleasedAfter(movies, search.ReleasedAfter);
            movies = GetMoviesReleasedBefore(movies, search.ReleasedBefore);

            if (search.SearchTerm != null)
            {
                movies = GetMoviesByTitle(movies, search.SearchTerm);
            }

            if (search.SortBy == SortBy.Title)
            {
                movies = movies.OrderBy(m => m.Title);
            }
            else if (search.SortBy == SortBy.Date)
            {
                movies = movies.OrderBy(m => m.ReleaseDate);
            }
            return movies;
        }

        private IEnumerable<MovieModel> FilterMoviesByGenre(IEnumerable<MovieModel> movies, GenreModel genre)
        {
            return (from movie in movies from movieGenre in movie.Genres where movieGenre.GenreId == genre.GenreId select movie);
        }

        private IEnumerable<MovieModel> GetMoviesReleasedAfter(IEnumerable<MovieModel> movies, DateTime releasedAfter)
        {
            return movies.Where(m => m.ReleaseDate >= releasedAfter);
        }

        private IEnumerable<MovieModel> GetMoviesReleasedBefore(IEnumerable<MovieModel> movies, DateTime releasedBefore)
        {
            return movies.Where(m => m.ReleaseDate <= releasedBefore);
        }

        private IEnumerable<MovieModel> GetMoviesByTitle(IEnumerable<MovieModel> movies, string searchTerm)
        {
            return movies.Where(movie => movie.Title.ToLower().Contains(searchTerm.ToLower()));
        }
    }
}
