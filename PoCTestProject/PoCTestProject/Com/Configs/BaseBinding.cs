﻿using BoDi;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using PoCTestProject.Com.Configs;
using RelevantCodes.ExtentReports;
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
               // webdriver.GetTestReportInstance().Log(LogStatus.Fail, "Test ended with " + LogStatus.Fail + " \n Stacktrace: " + ScenarioContext.Current.TestError);
                webdriver.EndTest();
            }
            else
            {
               // webdriver.GetTestReportInstance().Log(LogStatus.Pass, "Test ended with " + LogStatus.Pass);
                webdriver.EndTest();
            }

            //write report to file
            webdriver.Flush();

            //close driver
            webdriver.GetDriver().Quit();
        }
    }
}
