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
    public class Task11
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
        public void RegistrationNewUser()
        {
            string myEmail = Guid.NewGuid().ToString() + "@test.com";
            driver.Url = "http://localhost/litecart/en/";
            driver.FindElement(By.LinkText("New customers click here")).Click();
            // filling out the fields 
            driver.FindElement(By.Name("firstname")).SendKeys("Tania");
            driver.FindElement(By.Name("lastname")).SendKeys("Babkina");
            driver.FindElement(By.Name("address1")).SendKeys("Radyanska str.");
            driver.FindElement(By.Name("postcode")).SendKeys("64309");
            driver.FindElement(By.Name("city")).SendKeys("Izyum-gorod-geroy");
            driver.FindElement(By.Name("address1")).SendKeys("Radyanska str.");
            driver.FindElement(By.CssSelector("span.select2-selection.select2-selection--single")).Click();
            driver.FindElement(By.XPath("//li[contains(text(),'Ukraine')]")).Click();
            driver.FindElement(By.Name("email")).SendKeys(myEmail);
            driver.FindElement(By.Name("phone")).SendKeys("+380634338755");
            driver.FindElement(By.Name("password")).SendKeys("1111");
            driver.FindElement(By.Name("confirmed_password")).SendKeys("1111");
            driver.FindElement(By.Name("create_account")).Click();
            //log out
            driver.FindElement(By.LinkText("Logout")).Click();
            //log in
            driver.FindElement(By.Name("email")).SendKeys(myEmail);
            driver.FindElement(By.Name("password")).SendKeys("1111");
            driver.FindElement(By.Name("login")).Click();
            //log out
            driver.FindElement(By.LinkText("Logout")).Click();
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }


}
