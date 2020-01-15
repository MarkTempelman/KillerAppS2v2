using System;
using System.Collections.Generic;
using System.Linq;
using Data.DTO;
using Data.Interfaces;
using Data.SQLContext;
using Logic.Models;

namespace Logic
{
    public class MovieLogic
    {
        private readonly IMovieContext _iMovieContext;
        private readonly GenreLogic _genreLogic;
        private readonly SearchLogic _searchLogic;
        private readonly PlaylistLogic _playlistLogic;
        private readonly MediaLogic _mediaLogic;
        private readonly RatingLogic _ratingLogic;

        public MovieLogic(IMovieContext movieContext, GenreLogic genreLogic, SearchLogic searchLogic, 
            PlaylistLogic playlistLogic, MediaLogic mediaLogic, RatingLogic ratingLogic)
        {
            _iMovieContext = movieContext;
            _genreLogic = genreLogic;
            _searchLogic = searchLogic;
            _playlistLogic = playlistLogic;
            _mediaLogic = mediaLogic;
            _ratingLogic = ratingLogic;
        }

        public IEnumerable<MovieModel> GetAllMovies()
        {
            return _ratingLogic.AddRatingsToMovies(
                _genreLogic.AddGenresToMovies(
                    _iMovieContext.GetAllMovies()
                        .Select(ToMovieModel))
                    .ToList());
        }

        public MovieModel GetMovieById(int id)
        {
            return ToMovieModel(_iMovieContext.GetMovieById(id));
        }

        public IEnumerable<MovieModel> GetMoviesBySearchModel(SearchModel search)
        {
            return _ratingLogic.AddRatingsToMovies(
                _genreLogic.AddGenresToMovies(
                    _iMovieContext.GetMoviesBySearchModel(
                        _searchLogic.ToSearchDTO(search))
                        .Select(ToMovieModel))
                    .ToList());
        }

        public void CreateNewMovie(MovieModel movieModel)
        {
            _iMovieContext.CreateNewMovie(ToMovieDTO(movieModel));
        }

        public void EditMovie(MovieModel movieModel)
        {
            _iMovieContext.EditMovie(ToMovieDTO(movieModel));
        }

        public List<MovieModel> GetMoviesFromFavourites(int userId)
        {
            List<MovieModel> movies = _ratingLogic.AddRatingsToMovies(
                _playlistLogic.GetMediaIdsFromFavourites(userId)
                .Select(mediaId => ToMovieModel(_iMovieContext.GetMovieFromMediaId(mediaId)))
                .ToList());
            foreach (var movie in movies)
            {
                movie.IsFavourite = true;
            }
            return movies;
        }

        public List<MovieModel> CheckIfMoviesAreFavourites(List<MovieModel> movies, int userId)
        {
            foreach (var movie in movies)
            {
                if (_playlistLogic.IsMediaInPlaylist(
                    _mediaLogic.GetMediaIdFromMovieId(movie.MovieId),
                    _playlistLogic.GetPlaylistIdFromUserId(userId)))
                {
                    movie.IsFavourite = true;
                }
            }

            return movies;
        }

        public void DeleteMovieById(int movieId)
        {
            int mediaId = _mediaLogic.GetMediaIdFromMovieId(movieId);
            _iMovieContext.DeleteMovieById(movieId);
            _playlistLogic.RemoveMediaFromAllPlaylists(mediaId);
            _ratingLogic.DeleteRatingByMediaId(mediaId);
            _mediaLogic.DeleteMediaById(mediaId);
        }

        private MovieDTO ToMovieDTO(MovieModel movieModel)
        {
            MovieDTO movieDTO = new MovieDTO(movieModel.MovieId, movieModel.Title, movieModel.Description, movieModel.ReleaseDate, movieModel.MediaId);
            foreach (var genre in movieModel.Genres)
            {
                movieDTO.Genres.Add(_genreLogic.ToGenreDTO(genre));
            }
            movieDTO.ImagePath = movieModel.ImagePath;
            return movieDTO;
        }

        private MovieModel ToMovieModel(MovieDTO movieDTO)
        {
            MovieModel movieModel = new MovieModel(movieDTO.MovieId, movieDTO.Title, movieDTO.Description, movieDTO.ReleaseDate, movieDTO.MediaId);
            if (movieDTO.ImagePath != null)
            {
                movieModel.ImagePath = movieDTO.ImagePath;
            }
            return movieModel;
        }
    }
}
