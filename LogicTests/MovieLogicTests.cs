using System;
using System.Collections.Generic;
using System.Linq;
using Data.Interfaces;
using Data.MemoryContext;
using Logic;
using Models;
using NUnit.Framework;

namespace LogicTests
{
    public class MovieLogicTests
    {
        private IMovieContext _iMovieContext;
        private MovieLogic _movieLogic;
        private MovieModel _testMovie1;
        private MovieModel _testMovie2;
        private SearchModel _search;
        [SetUp]
        public void Setup()
        {
            _iMovieContext = new MovieMemoryContext();
            _movieLogic = new MovieLogic(_iMovieContext);
            _testMovie1 = new MovieModel(1, "TestTitle2", "TestDescription1", new DateTime(2019, 10, 23), 1);
            _testMovie2 = new MovieModel(2, "TestTitle1", "TestDescription2", new DateTime(2019, 10, 15), 2);
        }

        [Test]
        public void GetAllMoviesTest()
        {
            IEnumerable<int> expected = new List<int>
            {
                _testMovie1.MovieId,
                _testMovie2.MovieId
            };
            List<int> actual = new List<int>();

            foreach (var movie in _movieLogic.GetAllMovies())
            {
                actual.Add(movie.MovieId);
            }

            Assert.AreEqual(expected, actual);
        }

        //[Test]
        //public void FilterMoviesByGenreTest()
        //{
        //    _search = new SearchModel {Genre = new GenreModel("TestGenre1", 1), ReleasedAfter = DateTime.MinValue, ReleasedBefore = DateTime.MaxValue};
        //    var expected = _search.Genre.GenreId;

        //    var actual = _movieLogic.GetMoviesBySearchModel(_movieLogic.GetAllMovies(), _search).First().Genres.First().GenreId;
            
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void GetMoviesReleasedAfterTest()
        //{
        //    _search = new SearchModel{ReleasedAfter = new DateTime(2019, 10, 16), ReleasedBefore = DateTime.MaxValue};
        //    var expected = new DateTime(2019, 10, 16);

        //    var actual = _movieLogic.GetMoviesBySearchModel(_movieLogic.GetAllMovies(), _search)
        //        .Min(m => m.ReleaseDate);

        //    Assert.GreaterOrEqual(actual, expected);
        //}

        //[Test]
        //public void GetMoviesReleasedBeforeTest()
        //{
        //    _search = new SearchModel { ReleasedAfter = DateTime.MinValue, ReleasedBefore = new DateTime(2019, 10, 22)};
        //    var expected = new DateTime(2019, 10, 22);

        //    var actual = _movieLogic.GetMoviesBySearchModel(_movieLogic.GetAllMovies(), _search)
        //        .Max(m => m.ReleaseDate);

        //    Assert.GreaterOrEqual(expected, actual);
        //}

    }
}