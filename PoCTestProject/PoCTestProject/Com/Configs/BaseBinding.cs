using BoDi;
using NUnit.Framework;
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
            //initialize webdriver
            webdriver = new CWebDriver(objectContainerPrivate);
            webdriver.getDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            //initialize test name
            webdriver.createTest(ScenarioContext.Current.ScenarioInfo.Title);
            webdriver.getTestReportInstance().AssignCategory(ScenarioContext.Current.ScenarioInfo.Tags);

            //manage instances 
            objectContainerPrivate.RegisterInstanceAs<CWebDriver>(webdriver);
        }

        [AfterScenario]
        public void tearDown()
        {
            //close driver
            webdriver.getDriver().Quit();
            //string message = ScenarioContext.Current.TestError.Message;
            //write report details
            webdriver.getTestReportInstance().Pass("");

            //write report to file
            webdriver.Flush();
        }
    }
}
