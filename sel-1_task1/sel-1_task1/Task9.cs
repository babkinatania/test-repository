using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace sel_1_task1
{
    [TestFixture]
    public class Task9
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            //driver = new InternetExplorerDriver();
            // driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
        }
        [Test]
        public void CountiesOrder()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.FindElement(By.LinkText("Countries")).Click();
            var CountriesList = driver.FindElements(By.CssSelector("[name='countries_form'] tr.row"));
            var number = driver.FindElements(By.CssSelector("[name='countries_form'] tr.row")).Count;
            var i = 0;
            string[] array = new string[number];
            string[] arraySorted = new string[number];
            foreach (IWebElement row in CountriesList)
            {
                var country = row.FindElement(By.CssSelector("a:not([title = 'Edit']")).GetAttribute("textContent");
                array[i] = country;
                arraySorted[i] = country;
                i = i + 1;
            }
            Array.Sort(arraySorted);
            array.SequenceEqual(arraySorted);
            Assert.IsTrue(array.SequenceEqual(arraySorted));
        }

        [Test]
        public void SubCountriesOrder()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.FindElement(By.LinkText("Countries")).Click();
            var number = driver.FindElements(By.CssSelector("[name='countries_form'] tr.row")).Count;
            for (int i = 1; i <= number; i++)
            {
                string selectorCountryZone = string.Format(("[name='countries_form'] tr.row:nth-of-type({0}) td:nth-of-type(6)"), i + 1);
                var zone = driver.FindElement(By.CssSelector(selectorCountryZone)).GetAttribute("textContent");
                if (zone != "0")
                {
                    string selectorCountryLink = string.Format(("[name='countries_form'] tr.row:nth-of-type({0}) a:not([title = 'Edit']"), i + 1);
                    driver.FindElement(By.CssSelector(selectorCountryLink)).Click();
                    // проверка что зоны расположены в алфавитном порядке
                    var counter = driver.FindElements(By.CssSelector("table#table-zones tr")).Count;
                    var z = 0;
                    string[] arrayZ = new string[counter - 2];
                    string[] arrayZSorted = new string[counter - 2];
                    for (int c = 1; c < counter - 1; c++)
                    {
                        var selectorZone = string.Format(("table#table-zones tr:nth-of-type({0}) td:nth-of-type(3)"), c + 1);
                        var zoneName = driver.FindElement(By.CssSelector(selectorZone)).GetAttribute("textContent");
                        arrayZ[z] = zoneName;
                        arrayZSorted[z] = zoneName;
                        z = z + 1;
                    }
                    Array.Sort(arrayZSorted);
                    arrayZ.SequenceEqual(arrayZSorted);
                    Assert.IsTrue(arrayZ.SequenceEqual(arrayZSorted));
                    //выход на страницу всех стран
                    driver.FindElement(By.LinkText("Countries")).Click();
                }

            }
        }

        [Test]
        public void ZonesOrder()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.FindElement(By.LinkText("Geo Zones")).Click();
            var number = driver.FindElements(By.CssSelector("[name='geo_zones_form'] tr.row")).Count;
            for (int i = 1; i <= number; i++)
            {
                string selectorGeoZone = string.Format(("[name='geo_zones_form'] tr.row:nth-of-type({0}) a:not([title='Edit'])"), i + 1); 
                driver.FindElement(By.CssSelector(selectorGeoZone)).Click();
                // Edit Geo Zone страница
                int counter = driver.FindElements(By.CssSelector("table#table-zones tr")).Count;
                var massSize = counter - 2; // массив для селектед элементов
                string[] array = new string[massSize];
                string[] arraySorted = new string[massSize];
                int count = 0;
                //string selectedCountry = "";
                for (int c = 1; c < counter - 1; c++)
                {
                    string selectorZoneName = string.Format(("table#table-zones tr:nth-of-type({0}) td:nth-of-type(3) select option[selected]"), c + 1);
                    var zoneName = driver.FindElement(By.CssSelector(selectorZoneName)).GetAttribute("textContent");
                    // добавили селектед элмент в массив
                    array[count] = zoneName;
                    arraySorted[count] = zoneName;
                    count = count + 1;
                }
                Array.Sort(arraySorted);
                array.SequenceEqual(arraySorted);
                Assert.IsTrue(array.SequenceEqual(arraySorted));
                // выход на страницу Geo Zones
                driver.FindElement(By.LinkText("Geo Zones")).Click();
            }
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
