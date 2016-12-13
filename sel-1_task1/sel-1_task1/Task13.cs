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
    public class Task13
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
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
        }
        [Test]
        public void RemoveProductFromCart()
        {
            driver.Url = "http://localhost/litecart/en/";
            //add products
            for (int i = 1; i <= 3; i++)
            {
                driver.FindElement(By.CssSelector("div#box-most-popular li:nth-of-type(1)")).Click();
                //size dropdown for yellow duck
                if (IsElementPresent(driver, By.CssSelector("td.options>select")))
                {
                    driver.FindElement(By.CssSelector("select > option[value='Small']")).Click();
                }
                driver.FindElement(By.CssSelector("[name='add_cart_product']")).Click();
                // wait until cart counter is refreshed 
                string number = i + "";
                wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("div#cart span.quantity"), number));
                // go to main page 
                driver.Url = "http://localhost/litecart/en/";
            }
            driver.FindElement(By.LinkText("Checkout »")).Click();
            // deletion items from the cart
            var counter = driver.FindElements(By.CssSelector("div#order_confirmation-wrapper td.item")).Count;
            for (int i = 0; i < counter; i++)
            {
                var name = driver.FindElement(By.CssSelector("[name = 'cart_form'] a > strong")).Text; //product title
                string elementXpath = string.Format("//div[@id='order_confirmation-wrapper']//td[contains(., '{0}')]", name); // product in table
                var element = driver.FindElement(By.XPath(elementXpath));
                driver.FindElement(By.CssSelector("[name='remove_cart_item']")).Click();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.StalenessOf(element));
            }


        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }

        bool IsElementPresent(IWebDriver driver, By locator)
        {
            try
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
                return driver.FindElements(locator).Count > 0;
            }
            finally
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            }
        }


    }
}
