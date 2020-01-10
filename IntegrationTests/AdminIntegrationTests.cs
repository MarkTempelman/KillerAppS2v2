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
    class AdminIntegrationTests
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
        public void AddMovieThenDelete()
        {
            TestHelpers.LoadHome(_driver);

            TestHelpers.Login(_driver, "Admin", "admin");

            _driver.FindElement(By.Id("NavAddMovie")).Click();

            var guid = TestHelpers.GetRandomGuid();

            TestHelpers.WaitForPageLoad(_driver);

            TestHelpers.AddMovie(_driver, guid);

            TestHelpers.WaitForPageLoad(_driver);

            Assert.True(_driver.PageSource.Contains(guid));

            _driver.FindElement(By.Id($"Delete {guid}")).Click();

            TestHelpers.WaitForPageLoad(_driver);

            Assert.False(_driver.PageSource.Contains(guid));
        }

        [Test]
        public void AddMovieThenEditAndDelete()
        {
            var guid = TestHelpers.GetRandomGuid();

            TestHelpers.LoadHome(_driver);
            
            TestHelpers.Login(_driver, "Admin", "admin");

            _driver.FindElement(By.Id("NavAddMovie")).Click();

            TestHelpers.WaitForPageLoad(_driver);

            TestHelpers.AddMovie(_driver, guid);

            TestHelpers.WaitForPageLoad(_driver);

            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            _driver.FindElement(By.Id("Edit " + guid)).Click();

            TestHelpers.EditMovie(_driver, guid);

            TestHelpers.WaitForPageLoad(_driver);

            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            Assert.True(_driver.PageSource.Contains(
                guid + " This movie was generated and edited by the automated testing system."));

            _driver.FindElement(By.Id($"Delete {guid}")).Click();
        }

        [TearDown]
        public void TearDownTest()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}
