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
    public class Task12
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
        public void CreateProduct()
        {
            string productName = Guid.NewGuid().ToString() + "-Tania";
            //
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.FindElement(By.LinkText("Catalog")).Click();
            driver.FindElement(By.LinkText("Add New Product")).Click();
            // fillig out General tab 
            driver.FindElement(By.LinkText("General")).Click();
            driver.FindElement(By.XPath("//label[contains(text(),'Enabled')]")).Click();
            driver.FindElement(By.CssSelector("[name='name[en]']")).SendKeys(productName);
            driver.FindElement(By.CssSelector("[name='code']")).SendKeys("1");
            driver.FindElement(By.CssSelector("input[name='quantity']")).Clear();
            driver.FindElement(By.CssSelector("input[name='quantity']")).SendKeys("10");
            // add an image to a product 
            // driver.FindElement(By.CssSelector("input[name='new_images[]']")).SendKeys("C:\\myimage\\prod.png");

            // fillig out Information tab 
            driver.FindElement(By.LinkText("Information")).Click();
            driver.FindElement(By.XPath("//select[@name='manufacturer_id']/option[contains(text(),'ACME Corp')]")).Click();
            driver.FindElement(By.CssSelector("input[name='keywords']")).SendKeys("Key Word");
            driver.FindElement(By.CssSelector("input[name='short_description[en]']")).SendKeys("Short descritpion of my product");
            driver.FindElement(By.CssSelector("span.input-wrapper div.trumbowyg-editor")).SendKeys("A wide descritpion of my product. bla bla bla");
            driver.FindElement(By.CssSelector("input[name='head_title[en]']")).SendKeys("Product Head title");
            driver.FindElement(By.CssSelector("input[name='meta_description[en]']")).SendKeys("Product Meta Description");

            // fillig out Prices tab
            driver.FindElement(By.LinkText("Prices")).Click();
            driver.FindElement(By.CssSelector("input[name='purchase_price']")).Clear();
            driver.FindElement(By.CssSelector("input[name='purchase_price']")).SendKeys("9.99");
            driver.FindElement(By.XPath("//select[@name='purchase_price_currency_code']/option[@value='EUR']")).Click();

            //Save
            driver.FindElement(By.CssSelector("[name='save']")).Click();

            //Check title of added product
            driver.FindElement(By.LinkText("Catalog")).Click();
            string finalProduct = string.Format("//table[@class='dataTable']//a[contains(text(),'{0}')]", productName);
            driver.FindElement(By.XPath(finalProduct));
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }


}