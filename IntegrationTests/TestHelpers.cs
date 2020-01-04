using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
            driver.FindElement(By.Id("Username")).SendKeys(username);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.Id("Login")).Click();
        }

        public static void RegisterUser(IWebDriver driver, string guid)
        {
            driver.FindElement(By.Id("Username")).SendKeys(guid);
            driver.FindElement(By.Id("EmailAddress")).SendKeys(guid + "@mail.com");
            driver.FindElement(By.Id("Password")).SendKeys("testPassword");
            driver.FindElement(By.Id("Register")).Click();
        }
    }
}
