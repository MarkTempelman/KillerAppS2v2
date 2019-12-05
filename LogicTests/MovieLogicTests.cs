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
        private MovieLogic _movieLogic;
        private GenreLogic _genreLogic;
        private SearchLogic _searchLogic;
        private PlaylistLogic _playlistLogic;
        private MediaLogic _mediaLogic;
        private MovieModel _testMovie1;
        private MovieModel _testMovie2;
        private GenreModel _testGenre1;
        private GenreModel _testGenre2;
        private SearchModel _search;


        [SetUp]
        public void Setup()
        {
            _iMovieContext = new MovieMemoryContext();
            _iGenreContext = new GenreMemoryContext();
            
            _genreLogic = new GenreLogic(_iGenreContext);
            _searchLogic = new SearchLogic(_genreLogic);
            _playlistLogic = new PlaylistLogic(_iPlaylistContext, _iMediaContext);
            _mediaLogic = new MediaLogic(_iMediaContext);
            _movieLogic = new MovieLogic(_iMovieContext, _genreLogic, _searchLogic, _playlistLogic, _mediaLogic);
            _testGenre1 = new GenreModel("test1", 1);
            _testGenre2 = new GenreModel("test2", 2);
            _testMovie1 = new MovieModel(1, "TestTitle2", "TestDescription1", new DateTime(2019, 10, 23), 1);
            _testMovie2 = new MovieModel(2, "TestTitle1", "TestDescription2", new DateTime(2019, 10, 15), 2);
        }

        [Test]
        public void TC01GetAllMoviesTest()
        {
            IEnumerable<int> expected = new List<int> {1, 2};
            List<int> actual = _movieLogic.GetAllMovies().Select(movie => movie.MovieId).ToList();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TC03SortByReleaseDate()
        {
            _search = new SearchModel {SortBy = SortBy.Date};
            List<int> expected = new List<int> {2, 1};

            List<int> actual = _movieLogic.GetMoviesBySearchModel(_search).Select(movie => movie.MovieId).ToList();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TC04SortByTitle()
        {
            _search = new SearchModel { SortBy = SortBy.Title };
            List<int> expected = new List<int> { 2, 1 };

            List<int> actual = _movieLogic.GetMoviesBySearchModel(_search).Select(movie => movie.MovieId).ToList();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TC05FilterByDate()
        {
            _search = new SearchModel
            {
                ReleasedAfter = new DateTime(2019, 10, 16),
                ReleasedBefore = new DateTime(2019, 10, 25)
            };

            var actual = _movieLogic.GetMoviesBySearchModel(_search);
            
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
        public void TC06FilterByGenre()
        {
            _search = new SearchModel()
            {
                Genre = _testGenre1
            };
        }

        [Test]
        public void GetMovieById()
        {
            var expected = 1;
            var actual = _movieLogic.GetMovieById(1).MovieId;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetMoviesBySearchModel_ExistingTitle()
        {
            SearchModel search = new SearchModel {SearchTerm = "TestTitle1"};
            List<int> expected = new List<int> {2};

            List<int>actual = new List<int>();

            foreach (var movie in _movieLogic.GetMoviesBySearchModel(search))
            {
                actual.Add(movie.MovieId);
            }
            
            Assert.AreEqual(expected, actual);
        }
    }
}