using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace IntegrationTests
{
    public class UserIntegrationTests
    {
        private IWebDriver _driver;
        public string _homeURL;

        [SetUp]
        public void Setup()
        {
            _homeURL = "https://localhost:44359/";
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        private void LoadHome()
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(_homeURL);
        }

        private void Login(string username, string password)
        {
            _driver.FindElement(By.Id("Username")).SendKeys(username);
            _driver.FindElement(By.Id("Password")).SendKeys(password);
            _driver.FindElement(By.Id("Login")).Click();
        }

        private void RegisterUser(string guid)
        {
            _driver.FindElement(By.Id("Username")).SendKeys(guid);
            _driver.FindElement(By.Id("EmailAddress")).SendKeys(guid + "@mail.com");
            _driver.FindElement(By.Id("Password")).SendKeys("testPassword");
            _driver.FindElement(By.Id("Register")).Click();
        }

        [Test]
        public void LoginAsUser()
        {
            LoadHome();

            _driver.FindElement(By.Id("NavLogin")).Click();

            Login("User", "user");

            Assert.True(_driver.PageSource.Contains("Log out"));
        }

        [Test]
        public void LoginAsAdmin()
        {
            LoadHome();

            _driver.FindElement(By.Id("NavLogin")).Click();

            Login("Admin", "admin");

            _driver.Navigate().GoToUrl(_homeURL);

            Assert.True(_driver.PageSource.Contains("Log out"));
            Assert.True(_driver.PageSource.Contains("Add Movie"));
        }

        [Test]
        public void LoginThenLogout()
        {
            LoadHome();

            _driver.FindElement(By.Id("NavLogin")).Click();

            Login("User", "user");

            _driver.FindElement(By.Id("NavLogOut")).Click();

            Assert.True(_driver.PageSource.Contains("Log in"));
        }

        [Test]
        public void RegisterThenLogin()
        {
            LoadHome();

            var guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            guid = guid.Remove(guid.Length - 2);

            _driver.FindElement(By.Id("NavRegister")).Click();
            RegisterUser(guid);

            _driver.FindElement(By.Id("NavLogin")).Click();
            Login(guid, "testPassword");

            Assert.True(_driver.PageSource.Contains("Log out"));
        }
    }
}