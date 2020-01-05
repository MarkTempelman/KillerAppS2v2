using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace IntegrationTests
{
    class FavouritesIntegrationTests
    {
        private IWebDriver _driver;
        public string _homeURL;

        [SetUp]
        public void Setup()
        {
            _homeURL = "https://localhost:44359/";
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [Test]
        public void AddMovieToFavouritesThenRemoveFromFavourites()
        {
            TestHelpers.LoadHome(_driver);

            _driver.FindElement(By.Id("NavLogin")).Click();
            TestHelpers.Login(_driver, "User", "user");

            _driver.Navigate().GoToUrl("https://localhost:44359/Playlist/AddMovieToFavourites/1");

            _driver.FindElement(By.Id("NavFavourites")).Click();

            Assert.True(_driver.PageSource.Contains("Avengers"));

            _driver.Navigate().GoToUrl("https://localhost:44359/Playlist/RemoveMovieFromFavourites/1");
            _driver.FindElement(By.Id("NavFavourites")).Click();

            Assert.False(_driver.PageSource.Contains("Avengers"));
        }

        [TearDown]
        public void TearDownTest()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}
