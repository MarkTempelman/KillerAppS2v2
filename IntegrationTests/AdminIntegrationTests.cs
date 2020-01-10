﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

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

            Thread.Sleep(1000);

            _driver.FindElement(By.Id($"Delete {guid}")).Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until(webDriver => !webDriver.PageSource.Contains("guid"));

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

            Thread.Sleep(3000);

            _driver.FindElement(By.Id("Edit " + guid)).Click();

            TestHelpers.EditMovie(_driver, guid);

            TestHelpers.WaitForPageLoad(_driver);

            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            Assert.True(_driver.PageSource.Contains(
                guid + " This movie was generated and edited by the automated testing system."));

            _driver.FindElement(By.Id($"Delete {guid}")).Click();
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

            Thread.Sleep(3000);

            Assert.True(_driver.PageSource.Contains(guid));

            Thread.Sleep(3000);

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

            Thread.Sleep(3000);

            Assert.True(_driver.PageSource.Contains("MakeAdmin " + guid));

            Thread.Sleep(3000);

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
