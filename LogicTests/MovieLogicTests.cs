using System;
using System.Collections.Generic;
using System.Linq;
using Data.Interfaces;
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
        private MovieLogic _movieLogic;
        private MovieModel _testMovie1;
        private MovieModel _testMovie2;
        private SearchModel _search;
        [SetUp]
        public void Setup()
        {
            _iMovieContext = new MovieMemoryContext();
            _iGenreContext = new GenreMemoryContext();
            _movieLogic = new MovieLogic(_iMovieContext, _iGenreContext);
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
            List<int> actual = _movieLogic.GetAllMovies().Select(movie => movie.MovieId).ToList();

            Assert.AreEqual(expected, actual);
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