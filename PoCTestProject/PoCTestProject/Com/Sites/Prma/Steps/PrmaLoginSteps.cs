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
        private CWebDriver webdriver;

        public PrmaLoginSteps(CWebDriver driver)
        {
            webdriver = driver;
        }

        [Given(@"I navigate to the login URL")]
        public void GivenINavigateToTheLoginURL()
        {
            var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            webdriver.GetDriver().Navigate().GoToUrl(baseUrl);
            webdriver.LogStep(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
        
        [Given(@"I enter valid credentials")]
        public void GivenIEnterValidCredentials()
        {
            var userName = ConfigurationManager.AppSettings["adminUser"];
            var userPass = ConfigurationManager.AppSettings["adminPass"];
            LoginPage loginPage = new LoginPage(webdriver.GetDriver());
            loginPage.InputUserName(userName);
            loginPage.InputUserPass(userPass);
            loginPage.ClickLogin();
            webdriver.LogStep(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }
}
