using System;
using System.Collections.Generic;
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
        [SetUp]
        public void Setup()
        {
            _iMovieContext = new MovieMemoryContext();
            _movieLogic = new MovieLogic(_iMovieContext);
            _testMovie1 = new MovieModel(1, "TestTitle1", "TestDescription1", new DateTime(2019, 10, 23));
            _testMovie2 = new MovieModel(2, "TestTitle2", "TestDescription2", new DateTime(2019, 10, 15));
        }

        [Test]
        public void GetAllMoviesTest()
        {
            //arrange
            IEnumerable<int> expected = new List<int>
            {
                _testMovie1.MovieId,
                _testMovie2.MovieId
            };
            List<int> actual = new List<int>();

            //act
            foreach (var movie in _movieLogic.GetAllMovies())
            {
                actual.Add(movie.MovieId);
            }

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}