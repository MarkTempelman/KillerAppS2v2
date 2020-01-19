using System;
using System.Collections.Generic;
using System.Linq;
using Data.Interfaces;
using Data.SQLContext;
using Enums;
using LogicTests.MemoryContext;
using Logic;
using Logic.Models;
using NUnit.Framework;
using Renci.SshNet;

namespace LogicTests
{
    public class MovieLogicTests
    {
        private IMovieContext _iMovieContext;
        private IGenreContext _iGenreContext;
        private IPlaylistContext _iPlaylistContext;
        private IMediaContext _iMediaContext;
        private IRatingContext _iRatingContext;
        private MovieLogic _movieLogic;
        private GenreCollection _genreCollection;
        private SearchLogic _searchLogic;
        private PlaylistLogic _playlistLogic;
        private MediaLogic _mediaLogic;
        private GenreModel _testGenre1;
        private SearchModel _search;
        private RatingLogic _ratingLogic;


        [SetUp]
        public void Setup()
        {
            _iMovieContext = new MovieMemoryContext();
            _iGenreContext = new GenreMemoryContext();
            _iPlaylistContext = new PlaylistMemoryContext();
            _iMediaContext = new MediaMemoryContext();
            _iRatingContext = new RatingMemoryContext();
            _genreCollection = new GenreCollection(_iGenreContext);
            _searchLogic = new SearchLogic(_genreCollection);
            _playlistLogic = new PlaylistLogic(_iPlaylistContext, _iMediaContext);
            _mediaLogic = new MediaLogic(_iMediaContext);
            _ratingLogic = new RatingLogic(_iRatingContext, _mediaLogic);
            _movieLogic = new MovieLogic(_iMovieContext, _genreCollection, _searchLogic, _playlistLogic, _mediaLogic, _ratingLogic);
            _testGenre1 = new GenreModel("test1", 1, 0, _iGenreContext);
        }

        [Test]
        public void GetAllMoviesTest()
        {
            IEnumerable<int> expected = new List<int> {1, 2};
            List<int> actual = _movieLogic.GetAllMovies(0).Select(movie => movie.MovieId).ToList();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SortByReleaseDate()
        {
            _search = new SearchModel {SortBy = SortBy.Date};
            List<int> expected = new List<int> {2, 1};

            List<int> actual = _movieLogic.GetMoviesBySearchModel(_search, 0).Select(movie => movie.MovieId).ToList();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SortByTitle()
        {
            _search = new SearchModel { SortBy = SortBy.Title };
            List<int> expected = new List<int> { 2, 1 };

            List<int> actual = _movieLogic.GetMoviesBySearchModel(_search, 0).Select(movie => movie.MovieId).ToList();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FilterByDate()
        {
            _search = new SearchModel
            {
                ReleasedAfter = new DateTime(2019, 10, 16),
                ReleasedBefore = new DateTime(2019, 10, 25)
            };

            var actual = _movieLogic.GetMoviesBySearchModel(_search, 0);
            
            Assert.Multiple(() =>
                {
                    foreach (var movie in actual)
                    {
                        Assert.Greater(movie.ReleaseDate, new DateTime(2019, 10, 16));
                        Assert.Less(movie.ReleaseDate, new DateTime(2019, 10, 25));
                    }
                });
        }

        [Test]
        public void FilterByGenre()
        {
            _search = new SearchModel()
            {
                Genre = _testGenre1
            };

            var actual = _movieLogic.GetMoviesBySearchModel(_search, 0);

            Assert.AreEqual(_testGenre1.GenreId, actual.First().Genres.First().GenreId);
        }

        [Test]
        public void ExistingTitleSearch()
        {
            SearchModel search = new SearchModel { SearchTerm = "TestTitle1" };
            List<int> expected = new List<int> { 2 };

            List<int> actual = new List<int>();

            foreach (var movie in _movieLogic.GetMoviesBySearchModel(search, 0))
            {
                actual.Add(movie.MovieId);
            }

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PartialTitleSearch()
        {
            SearchModel search = new SearchModel { SearchTerm = "Test" };
            List<int> expected = new List<int> { 2, 1 };

            List<int> actual = new List<int>();

            foreach (var movie in _movieLogic.GetMoviesBySearchModel(search, 0))
            {
                actual.Add(movie.MovieId);
            }

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void NonExistantTitleSearch()
        {
            SearchModel search = new SearchModel { SearchTerm = "aefa" };
            List<int> expected = new List<int>();

            List<int> actual = new List<int>();

            foreach (var movie in _movieLogic.GetMoviesBySearchModel(search, 0))
            {
                actual.Add(movie.MovieId);
            }

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetMovieById()
        {
            var expected = 1;
            var actual = _movieLogic.GetMovieById(1, 0).MovieId;
            Assert.AreEqual(expected, actual);
        }

        
    }
}