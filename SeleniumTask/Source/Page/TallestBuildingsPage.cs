using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace SeleniumTask.Source.Page
{
    public class TallestBuildingsPage : BasePage
    { 
        private IWebDriver _driver;
        [FindsBy(How = How.CssSelector, Using = "#lists-pages-select-container .select-lists-pages")]
        private SelectElement _listPagesSelect;

        [FindsBy(How = How.CssSelector, Using = "#buildingsTable tbody")]
        private IWebElement _buildingsTable;

        public TallestBuildingsPage(IWebDriver driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl("https://www.skyscrapercenter.com/buildings?list=tallest100-construction");
            PageFactory.InitElements(_driver, this);
        }
        
        public void SelectFromDropdown(string option)
        {
            _listPagesSelect.SelectByText(option);
        }

        public int GetNumberOfRecords()
        {
            return _buildingsTable.FindElements(By.TagName("tr")).Count();
        }
        public int GetMaximumFloorValue()
        {
            int max = -1;
            var rows = _buildingsTable.FindElements(By.TagName("tr"));
            foreach (var row in rows)
            {
                int number;
                bool isParsable = Int32.TryParse(row.FindElements(By.TagName("td"))[(int)Header.Floors].Text, out number);
                if (isParsable && number > max)
                    max = number;
            }
            return max;
        }

        public IWebElement GetRowByText(string text)
        {
            return _buildingsTable.FindElement(By.XPath("//a[contains(text(),'" + text + "')]/../../.."));
        }

    }
    enum Header
    {
        Rank,
        Name,
        City,
        Status,
        Completion,
        Height,
        Floors,
        Material,
        Function
    }
}
