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
    class MovieIntegrationTests
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
        public void LoadMovieList()
        {
            TestHelpers.LoadHome(_driver);

            Assert.True(_driver.PageSource.Contains("Avengers"));
        }

        [Test]
        public void MoreMovieInfo()
        {
            TestHelpers.LoadHome(_driver);

            _driver.Url = "https://localhost:44359/Movie/MovieInfo/1";

            Assert.True(_driver.PageSource.Contains("Back to List"));
        }

        [Test]
        public void SearchForName()
        {
            TestHelpers.LoadHome(_driver);

            _driver.FindElement(By.Id("SearchTerm")).SendKeys("Avengers");
            _driver.FindElement(By.Id("Search")).Click();

            Assert.True(_driver.PageSource.Contains("Avengers"));
            Assert.False(_driver.PageSource.Contains("Joker"));
        }

        [TearDown]
        public void TearDownTest()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}
