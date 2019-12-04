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

        public MovieLogic(IMovieContext movieContext, GenreLogic genreLogic, SearchLogic searchLogic, PlaylistLogic playlistLogic, MediaLogic mediaLogic)
        {
            _iMovieContext = movieContext;
            _genreLogic = genreLogic;
            _searchLogic = searchLogic;
            _playlistLogic = playlistLogic;
            _mediaLogic = mediaLogic;
        }

        public IEnumerable<MovieModel> GetAllMovies()
        {
            return _genreLogic.AddGenresToMovies(_iMovieContext.GetAllMovies().Select(ToMovieModel));
        }

        public MovieModel GetMovieById(int id)
        {
            return ToMovieModel(_iMovieContext.GetMovieById(id));
        }

        public IEnumerable<MovieModel> GetMoviesBySearchModel(SearchModel search)
        {
            return _iMovieContext.GetMoviesBySearchModel(_searchLogic.ToSearchDTO(search)).Select(ToMovieModel);
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
            List<MovieModel> movies = _playlistLogic.GetMediaIdsFromFavourites(userId)
                .Select(mediaId => ToMovieModel(_iMovieContext.GetMovieFromMediaId(mediaId)))
                .ToList();
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

        private MovieDTO ToMovieDTO(MovieModel movieModel)
        {
            MovieDTO movieDTO = new MovieDTO(movieModel.MovieId, movieModel.Title, movieModel.Description, movieModel.ReleaseDate, movieModel.MediaId);
            foreach (var genre in movieModel.Genres)
            {
                movieDTO.Genres.Add(_genreLogic.ToGenreDTO(genre));
            }
            return movieDTO;
        }

        private MovieModel ToMovieModel(MovieDTO movieDTO)
        {
            return new MovieModel(movieDTO.MovieId, movieDTO.Title, movieDTO.Description, movieDTO.ReleaseDate, movieDTO.MediaId);
        }
    }
}
