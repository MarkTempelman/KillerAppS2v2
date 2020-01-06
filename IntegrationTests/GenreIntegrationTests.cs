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
    public class GenreIntegrationTests
    {
        private IWebDriver _driver;
        private string _homeURL;

        [SetUp]
        public void Setup()
        {
            _homeURL = "https://localhost:44359/";
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [Test]
        public void AddNewGenreThenRemove()
        {
            var guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            guid = guid.Remove(guid.Length - 2);

            TestHelpers.LoadHome(_driver);

            _driver.FindElement(By.Id("NavLogin")).Click();

            TestHelpers.Login(_driver, "Admin", "admin");

            _driver.FindElement(By.Id("NavAddGenre")).Click();

            _driver.FindElement(By.Id("Genre")).SendKeys(guid);

            _driver.FindElement(By.Id("CreateGenre")).Click();

            _driver.FindElement(By.Id("NavManageGenres")).Click();

            Assert.True(_driver.PageSource.Contains(guid));

            _driver.FindElement(By.Id(guid)).Click();

            Assert.False(_driver.PageSource.Contains(guid));
        }

        [TearDown]
        public void TearDownTest()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}
