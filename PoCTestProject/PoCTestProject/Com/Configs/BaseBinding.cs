using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PoCTestProject.Com.Configs;
using System;
using TechTalk.SpecFlow;

namespace PoCTestProject.Com.Selenium
{
    [Binding]
    public class BaseBinding
    {

        private readonly IObjectContainer objectContainerPrivate;
        private CWebDriver webdriver;

        public BaseBinding(IObjectContainer objectContainer)
        {
            objectContainerPrivate = objectContainer;
        }

        [BeforeScenario]
        public void setUp()
        {
            webdriver = new CWebDriver(objectContainerPrivate);
           // webdriver.getDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            objectContainerPrivate.RegisterInstanceAs<CWebDriver>(webdriver);
        }

        [AfterScenario]
        public void tearDown()
        {
            webdriver.getDriver().Quit();
        }


    }
}
