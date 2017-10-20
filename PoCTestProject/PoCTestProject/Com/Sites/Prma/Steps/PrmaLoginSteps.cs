using NUnit.Framework;
using OpenQA.Selenium;
using PoCTestProject.Com.Configs;
using PoCTestProject.Com.Sites.Prma.Pages;
using PoCTestProject.Com.Tools;
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
            webdriver.LogStep(ScenarioContext.Current.StepContext.StepInfo);
            var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            webdriver.GetDriver().Navigate().GoToUrl(baseUrl + "/login");
        }

        [Given(@"I enter valid credentials")]
        public void GivenIEnterValidCredentials()
        {
            webdriver.LogStep(ScenarioContext.Current.StepContext.StepInfo);
            var userName = ConfigurationManager.AppSettings["adminUser"];
            var userPass = ConfigurationManager.AppSettings["adminPass"];
            LoginPage loginPage = new LoginPage(webdriver.GetDriver());
            loginPage.InputUserName(userName);
            loginPage.InputUserPass(userPass);
            loginPage.ClickLogin();
            
        }

        [When(@"I go to heatmap")]
        public void WhenIGoToHeatmap()
        {
            webdriver.LogStep(ScenarioContext.Current.StepContext.StepInfo);
            var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            webdriver.GetDriver().Navigate().GoToUrl(baseUrl + "/heatmap");
            Assert.AreSame("are", "there");
        }

    }
}
