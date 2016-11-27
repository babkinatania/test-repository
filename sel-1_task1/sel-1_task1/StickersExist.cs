using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace sel_1_task1
{
    [TestFixture]
    public class StickersExist
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            // driver = new InternetExplorerDriver();
            // driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
        }

        [Test]
        public void ProductStickers()
        {
            driver.Url = "http://localhost/litecart/en/";
            var ProductsAmount = driver.FindElements(By.CssSelector("div.content li.product.column.shadow.hover-light"));

            foreach (IWebElement product in ProductsAmount)
            {
                var n = product.FindElements(By.CssSelector("div.sticker")).Count;
                Assert.IsTrue(n == 1);
            }

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }

        private bool AreElementsPresent(By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }

        private bool IsElementPresent(By locator)
        {
            try
            {
                driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException ex)
            {
                return false;
            }
        }
    }
}
