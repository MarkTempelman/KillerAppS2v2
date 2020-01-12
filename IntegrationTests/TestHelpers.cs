﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace IntegrationTests
{
    public class TestHelpers
    {
        private static string _homeURL = "https://localhost:44359/";

        public static void LoadHome(IWebDriver driver)
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(_homeURL);
        }

        public static void Login(IWebDriver driver, string username, string password)
        {
            driver.FindElement(By.Id("NavLogin")).Click();
            driver.FindElement(By.Id("Username")).SendKeys(username);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.Id("Login")).Click();
        }

        public static void RegisterUser(IWebDriver driver, string guid)
        {
            driver.FindElement(By.Id("NavRegister")).Click();
            driver.FindElement(By.Id("Username")).SendKeys(guid);
            driver.FindElement(By.Id("EmailAddress")).SendKeys(guid + "@mail.com");
            driver.FindElement(By.Id("Password")).SendKeys("testPassword");
            driver.FindElement(By.Id("Register")).Click();
        }

        public static void AddMovie(IWebDriver driver, string guid)
        {
            driver.FindElement(By.Id("Title")).SendKeys(guid);
            driver.FindElement(By.Id("Description")).SendKeys(guid + " This movie was generated by the automated testing system.");
            driver.FindElement(By.Id("CreateMovie")).Click();
        }

        public static void AddMovie(IWebDriver driver, string title, string description)
        {
            driver.FindElement(By.Id("Title")).SendKeys(title);
            driver.FindElement(By.Id("Description")).SendKeys(description);
            driver.FindElement(By.Id("CreateMovie")).Click();
        }

        public static void AddMovieSetup(IWebDriver driver)
        {
            LoadHome(driver);

            WaitForPageLoad(driver);

            Login(driver, "Admin", "admin");

            WaitForPageLoad(driver);

            driver.FindElement(By.Id("NavAddMovie")).Click();

            WaitForPageLoad(driver);
        }

        public static void EditMovie(IWebDriver driver, string guid)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Description"))).Clear();
            driver.FindElement(By.Id("Description")).SendKeys(guid + " This movie was generated and edited by the automated testing system.");
            driver.FindElement(By.Id("EditMovie")).Click();
        }

        public static string GetRandomGuid()
        {
            var guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            guid = guid.Remove(guid.Length - 2);
            return guid;
        }

        public static void WaitForPageLoad(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(webDriver => ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}
