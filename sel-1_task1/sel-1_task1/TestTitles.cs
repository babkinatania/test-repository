using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;


namespace sel_1_task1
{
    [TestFixture]
    public class TitlesleftSidebar
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new InternetExplorerDriver();
            //driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
        }


        [Test]
        public void TitlesInLeftSidebar()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            //  кол-во элементов в меню
            var countMenu = driver.FindElements(By.CssSelector("li#app-")).Count;
            for (int c = 0; c < countMenu; c++)
            {
                string selectorMenu = string.Format(("li#app-:nth-child({0})"), c + 1);
                var menu = driver.FindElement(By.CssSelector(selectorMenu));
                menu.Click();
                driver.FindElement(By.CssSelector("td#content h1"));

                if (AreElementsPresent(By.CssSelector("li#app- li")))
                {
                    //считаем кол-во элементов в подменю
                    var element = driver.FindElements(By.CssSelector("li#app- li"));
                    var countSubMenu = driver.FindElements(By.CssSelector("li#app- li")).Count;
                    //пройтись ко каждому элементу подменю
                    for (int i = 0; i < countSubMenu; i++)
                    {
                        string selector = string.Format("li#app- li:nth-child({0})", i + 1);
                        var e = driver.FindElement(By.CssSelector(selector));
                        e.Click();
                        driver.FindElement(By.CssSelector("td#content h1"));
                    }
                }
                // else { driver.Url = "https://www.google.nl/"; }
            }
        }

        private bool AreElementsPresent(By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }



        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
