using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTask.Source.Page;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTask.Test
{
    public class TallestBuildingsTest
    {
        private IWebDriver _driver;
        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
          
        }

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
        }

        [Test]
        public void TestTask()
        {           
            TallestBuildingsPage tbPage = new TallestBuildingsPage(_driver);

            tbPage.AcceptCookies(_driver);

            tbPage.SelectFromDropdown("100 Tallest Completed Buildings in the World");                   

            Assert.That(tbPage.GetRowByText("Lotte World Tower").FindElements(By.TagName("td"))[(int)Header.Floors].Text, Is.EqualTo("123"));

            Assert.That(tbPage.GetNumberOfRecords(), Is.EqualTo(100));

            Assert.That(tbPage.GetMaximumFloorValue(), Is.EqualTo(163));
        }
    }
}
