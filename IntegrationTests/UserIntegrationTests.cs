using System;
using System.IO;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace IntegrationTests
{
    public class UserIntegrationTests
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
        public void LoginAsUser()
        {
            TestHelpers.LoadHome(_driver);

            TestHelpers.Login(_driver, "User", "user");

            Assert.True(_driver.PageSource.Contains("Log out"));
            Assert.False(_driver.PageSource.Contains("Add Movie"));
        }

        [Test]
        public void LoginAsAdmin()
        {
            TestHelpers.LoadHome(_driver);

            TestHelpers.Login(_driver, "Admin", "admin");

            _driver.Navigate().GoToUrl(_homeURL);

            Assert.True(_driver.PageSource.Contains("Log out"));
            Assert.True(_driver.PageSource.Contains("Add Movie"));
        }

        [Test]
        public void LoginThenLogout()
        {
            TestHelpers.LoadHome(_driver);

            TestHelpers.Login(_driver, "User", "user");

            _driver.FindElement(By.Id("NavLogOut")).Click();

            Assert.True(_driver.PageSource.Contains("Log in"));
        }

        [Test]
        public void RegisterThenLogin()
        {
            TestHelpers.LoadHome(_driver);

            var guid = TestHelpers.GetRandomGuid();

            TestHelpers.RegisterUser(_driver, guid);

            TestHelpers.Login(_driver, guid, "testPassword");

            Assert.True(_driver.PageSource.Contains("Log out"));
        }

        [Test]
        public void RegisterThenDeleteUser()
        {
            var guid = TestHelpers.GetRandomGuid();

            TestHelpers.LoadHome(_driver);

            TestHelpers.RegisterUser(_driver, guid);

            TestHelpers.Login(_driver, "Admin", "admin");

            _driver.FindElement(By.Id("NavManageUsers")).Click();

            TestHelpers.WaitForPageLoad(_driver);

            Thread.Sleep(1000);

            Assert.True(_driver.PageSource.Contains(guid));

            Thread.Sleep(1000);

            _driver.FindElement(By.Id("Delete " + guid)).Click();

            TestHelpers.WaitForPageLoad(_driver);

            Assert.False(_driver.PageSource.Contains(guid));
        }

        [Test]
        public void RegisterThenMakeUserAdmin()
        {
            var guid = TestHelpers.GetRandomGuid();

            TestHelpers.LoadHome(_driver);

            TestHelpers.RegisterUser(_driver, guid);

            TestHelpers.Login(_driver, "Admin", "admin");

            _driver.FindElement(By.Id("NavManageUsers")).Click();

            TestHelpers.WaitForPageLoad(_driver);

            Thread.Sleep(1000);

            Assert.True(_driver.PageSource.Contains("MakeAdmin " + guid));

            Thread.Sleep(1000);

            _driver.FindElement(By.Id("MakeAdmin " + guid)).Click();

            Assert.False(_driver.PageSource.Contains("MakeAdmin " + guid));
        }


        [TearDown]
        public void TearDownTest()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}