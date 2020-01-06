using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Data.Interfaces;
using Logic;
using Logic.Models;
using LogicTests.MemoryContext;
using NUnit.Framework;

namespace LogicTests
{
    public class GenreTests
    {
        private IGenreContext _genreContext;
        private GenreLogic _genreLogic;

        [SetUp]
        public void Setup()
        {
            _genreContext = new GenreMemoryContext();
            _genreLogic = new GenreLogic(_genreContext);
        }

        [Test]
        public void GetAllGenres()
        {
            List<int> expected = new List<int> {1, 2};
            List<int> actual = new List<int>();
            foreach (var genre in _genreLogic.GetAllGenres())
            {
                actual.Add(genre.GenreId);
            }

            Assert.AreEqual(expected, actual);
        }
    }
}
