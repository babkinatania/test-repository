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
    public class Task10
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            // driver = new InternetExplorerDriver();
            // driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
        }
        [Test]
        public void CampaignsProduct()
        {
            driver.Url = "http://localhost/litecart/en/";
            // --- main page --- 
            string productName = driver.FindElement(By.CssSelector("div#box-campaigns ul li:nth-of-type(1)>a>div.name")).Text;
            var productPrices = driver.FindElements(By.CssSelector("div#box-campaigns ul li:nth-of-type(1) div.price-wrapper *"));
            IWebElement priceOld = productPrices[0];
            IWebElement priceDiscount = productPrices[1];
            // price old on main page
            string priceOldText = priceOld.Text; // old price main page 
            string priceOldClass = priceOld.GetAttribute("class"); // regular-price 
            Assert.IsTrue(priceOldClass == "regular-price");
            string priceOldTag = priceOld.TagName;
            Assert.IsTrue(priceOldTag == "s");

            // price with discount on main page
            string priceDiscountText = priceDiscount.Text; // price with discount main page
            string priceDiscountClass = priceDiscount.GetAttribute("class"); // campaign-price
            Assert.IsTrue(priceDiscountClass == "campaign-price");
            string priceDiscountTag = priceDiscount.TagName;
            Assert.IsTrue(priceDiscountTag == "strong");


            // go to product page
            driver.FindElement(By.CssSelector("div#box-campaigns ul li:nth-of-type(1) a.link")).Click();
            // --- product page --- 
            string productTitle = driver.FindElement(By.CssSelector("div#box-product h1.title")).Text;
            Assert.IsTrue(productName == productTitle); // compare Title with main page
            // - -
            var productPagePrices = driver.FindElements(By.CssSelector("div.information div.price-wrapper *"));
            IWebElement el1 = productPagePrices[0];
            IWebElement el2 = productPagePrices[1];
            string priceOldClass2 = el1.GetAttribute("class"); // regular-price
            Assert.IsTrue(priceOldClass2 == "regular-price");
            string priceOldTag2 = el1.TagName; // s
            Assert.IsTrue(priceOldTag2 == "s");
            string priceOldText2 = el1.Text; // $20
            Assert.IsTrue(priceOldText == priceOldText2);

            string priceDiscountClass2 = el2.GetAttribute("class"); // campaign-price 
            Assert.IsTrue(priceDiscountClass2 == "campaign-price");
            string priceDiscountTag2 = el2.TagName; // strong
            Assert.IsTrue(priceDiscountTag2 == "strong");
            string priceDiscountText2 = el2.Text; // $18
            Assert.IsTrue(priceDiscountText == priceDiscountText2);
        }
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
