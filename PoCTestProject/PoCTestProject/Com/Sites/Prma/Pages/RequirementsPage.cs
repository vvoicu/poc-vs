using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoCTestProject.Com.Sites.Prma.Pages
{
    class RequirementsPage
    {
        private IWebDriver webdriver;

        public RequirementsPage(IWebDriver driver)
        {
            webdriver = driver;
        }

        private By userNameInput = By.CssSelector("input[name='email']");
        private By userPassInput = By.CssSelector("input[name='password']");
        private By loginButton = By.CssSelector("button");

        public void InputUserName(String userName)
        {
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(userNameInput));

            webdriver.FindElement(userNameInput).SendKeys(userName);
        }
    }
}
