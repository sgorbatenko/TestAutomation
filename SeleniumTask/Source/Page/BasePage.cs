using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTask.Source.Page
{
    public class BasePage
    {
        public void AcceptCookies(IWebDriver driver)
        {
            driver.FindElement(By.CssSelector(".cookie-consent__agree")).Click();
        }

    }
}
