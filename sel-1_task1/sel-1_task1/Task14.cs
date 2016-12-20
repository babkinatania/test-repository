using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Collections.Generic;

namespace sel_1_task1
{
    [TestFixture]
    public class Task14
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
           //driver = new InternetExplorerDriver();
           //driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
        }
        [Test]
        public void TestNewWindow()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.FindElement(By.LinkText("Countries")).Click();
            driver.FindElement(By.LinkText("Add New Country")).Click();
            var NewWindowLinksList = driver.FindElements(By.CssSelector("form a[target='_blank']"));
            foreach (IWebElement row in NewWindowLinksList)
            {
                string originalWindow = driver.CurrentWindowHandle;
                ICollection<string> oldWindows = driver.WindowHandles;
                row.Click();
                // ожидание появления нового окна
                var newWindow = wait.Until(k => WaitNewWindow(k, oldWindows));
                // переключаемся в новое окно
                driver.SwitchTo().Window(newWindow);
                // закрываем новое окно
                driver.Close();
                // переключаемся в старое окно
                driver.SwitchTo().Window(originalWindow);
            }
        }

        string WaitNewWindow(IWebDriver driver, ICollection<string> oldWindows)
        {
            var handles = driver.WindowHandles.ToList();
            handles.RemoveAll(e => oldWindows.Contains(e));

            foreach (var i in handles)
            {
                return i;
            }

            return null;
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
