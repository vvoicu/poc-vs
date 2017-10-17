using AventStack.ExtentReports;
using BoDi;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
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
        public void SetUp()
        {
            //initialize webdriver
            webdriver = new CWebDriver(objectContainerPrivate);
            webdriver.GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            //initialize test name
            webdriver.CreateTest(ScenarioContext.Current.ScenarioInfo.Title);
            webdriver.GetTestReportInstance().AssignCategory(ScenarioContext.Current.ScenarioInfo.Tags);

            //manage instances 
            objectContainerPrivate.RegisterInstanceAs<CWebDriver>(webdriver);
            
        }

        [AfterScenario]
        public void TearDown()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                //write report to file if errors are found
                webdriver.GetTestReportInstance().Log(Status.Fail, "Test ended with " + Status.Fail + " \n Stacktrace: " + ScenarioContext.Current.TestError);
            }
            else
                webdriver.GetTestReportInstance().Log(Status.Pass, "Test ended with " + Status.Pass);

            //write report to file
            webdriver.Flush();

            //close driver
            webdriver.GetDriver().Quit();
        }

    }
}
