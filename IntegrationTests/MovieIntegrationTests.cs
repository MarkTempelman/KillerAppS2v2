using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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

        [Test]
        public void AddMovieThenDelete()
        {
            var guid = TestHelpers.GetRandomGuid();
            TestHelpers.AddMovieSetup(_driver);

            TestHelpers.AddMovie(_driver, guid);

            TestHelpers.WaitForPageLoad(_driver);

            Assert.True(_driver.PageSource.Contains(guid));

            Thread.Sleep(1000);

            _driver.FindElement(By.Id($"Delete {guid}")).Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until(webDriver => !webDriver.PageSource.Contains("guid"));

            Assert.False(_driver.PageSource.Contains(guid));
        }

        [Test]
        public void TryAddMovieWithoutTitle()
        {
            TestHelpers.AddMovieSetup(_driver);

            TestHelpers.AddMovie(_driver, "", "test Description");

            TestHelpers.WaitForPageLoad(_driver);

            Assert.True(_driver.PageSource.Contains("The Title field is required."));
        }

        [Test]
        public void TryAddMovieWithoutDesc()
        {
            TestHelpers.AddMovieSetup(_driver);

            TestHelpers.AddMovie(_driver, "test", "");

            TestHelpers.WaitForPageLoad(_driver);

            Assert.True(_driver.PageSource.Contains("The Description field is required."));
        }

        [Test]
        public void AddMovieThenEditDescriptionAndDelete()
        {
            var guid = TestHelpers.GetRandomGuid();

            TestHelpers.AddMovieSetup(_driver);

            TestHelpers.AddMovie(_driver, guid);

            TestHelpers.WaitForPageLoad(_driver);

            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            Thread.Sleep(1000);

            _driver.FindElement(By.Id("Edit " + guid)).Click();

            TestHelpers.EditMovie(_driver, null, guid + " This movie was generated and edited by the automated testing system.");

            TestHelpers.WaitForPageLoad(_driver);

            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            Assert.True(_driver.PageSource.Contains(
                guid + " This movie was generated and edited by the automated testing system."));

            _driver.FindElement(By.Id($"Delete {guid}")).Click();
        }

        [Test]
        public void AddMovieThenEditTitleAndDelete()
        {
            var guid = TestHelpers.GetRandomGuid();

            TestHelpers.AddMovieSetup(_driver);

            TestHelpers.AddMovie(_driver, guid);

            TestHelpers.WaitForPageLoad(_driver);

            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            Thread.Sleep(1000);

            _driver.FindElement(By.Id("Edit " + guid)).Click();

            TestHelpers.EditMovie(_driver, "Edited " + guid, null);

            TestHelpers.WaitForPageLoad(_driver);

            Thread.Sleep(1000);

            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            Assert.True(_driver.PageSource.Contains("Edited " + guid));

            _driver.FindElement(By.Id($"Delete Edited {guid}")).Click();
        }
    }
}
