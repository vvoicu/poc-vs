using AventStack.ExtentReports;
using BoDi;
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
        private static ExtentReports report;

        public BaseBinding(IObjectContainer objectContainer)
        {
            objectContainerPrivate = objectContainer;
        }

        [BeforeScenario]
        public void SetUp()
        {
            webdriver = new CWebDriver(objectContainerPrivate);
            webdriver.GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            //initialize test name
            webdriver.CreateTest(ScenarioContext.Current.ScenarioInfo.Title);
            webdriver.GetTestReportInstance().AssignCategory(ScenarioContext.Current.ScenarioInfo.Tags);

            report = webdriver.GetExtentReport();
            //manage instances 
            objectContainerPrivate.RegisterInstanceAs<CWebDriver>(webdriver);

        }

        [BeforeStep]
        public void BeforeReportStep()
        {
            webdriver.LogStep(ScenarioContext.Current.StepContext.StepInfo, "Step Start:");
        }
        
        [AfterStep]
        public void AfterReportStep()
        {
            webdriver.LogStep(ScenarioContext.Current.StepContext.StepInfo, "Step Finish:");
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

            //close driver
            webdriver.GetDriver().Quit();
            //write data to report file
            report.Flush();
        }
        
    }
}
