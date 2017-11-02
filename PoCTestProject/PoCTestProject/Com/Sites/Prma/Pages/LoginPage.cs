using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PoCTestProject.Com.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoCTestProject.Com.Sites.Prma.Pages
{
    class LoginPage
    {
        private IWebDriver webdriver;

        public LoginPage(IWebDriver driver)
        {
            webdriver = driver;
        }

        private By userNameInput = By.CssSelector("input[name='email']");
        private By userPassInput = By.CssSelector("input[name='password']");
        private By loginButton = By.CssSelector("button");


        public void InputUserName(String userName)
        {
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(Constants.WAIT_TIME_DEFAULT)).Until(ExpectedConditions.ElementToBeClickable(userNameInput));
            webdriver.FindElement(userNameInput).SendKeys(userName);
        }

        public void InputUserPass(String userPass)
        {
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(Constants.WAIT_TIME_DEFAULT)).Until(ExpectedConditions.ElementToBeClickable(userPassInput));
            webdriver.FindElement(userPassInput).SendKeys(userPass);
        }

        public void ClickLogin()
        {
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(Constants.WAIT_TIME_DEFAULT)).Until(ExpectedConditions.ElementToBeClickable(loginButton));
            webdriver.FindElement(loginButton).Click();
        }
    }
}
