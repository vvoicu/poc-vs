using OpenQA.Selenium;
using PoCTestProject.Com.Configs;
using PoCTestProject.Com.Sites.Prma.Pages;
using System;
using System.Configuration;
using TechTalk.SpecFlow;

namespace PoCTestProject.Com.Sites.Prma.Steps
{
    [Binding]
    public class PrmaLoginSteps
    {
        private IWebDriver webdriver;

        public PrmaLoginSteps(CWebDriver driver)
        {
            webdriver = driver.getDriver();
        }

        [Given(@"I navigate to the login URL")]
        public void GivenINavigateToTheLoginURL()
        {
            var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            webdriver.Navigate().GoToUrl(baseUrl);
            Console.WriteLine("GivenINavigateToTheLoginURL");
        }
        
        [Given(@"I enter valid credentials")]
        public void GivenIEnterValidCredentials()
        {
            LoginPage loginPage = new LoginPage(webdriver);
            loginPage.inputUserName("aaaa");
            loginPage.inputUserPass("bbbb");
            loginPage.clickLogin();
            Console.WriteLine("GivenIEnterValidCredentials");
        }
    }
}
