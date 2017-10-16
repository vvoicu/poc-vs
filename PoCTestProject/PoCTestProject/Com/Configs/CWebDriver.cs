﻿using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using PoCTestProject.Com.Tools;
using System;
using System.Configuration;
using TechTalk.SpecFlow;

namespace PoCTestProject.Com.Configs
{

    public class CWebDriver
    {
        private readonly IObjectContainer objectContainerPrivate;
        
        //Selenium Related properties
        private IWebDriver webdriver;
        private static int itemCount = 0;

        //ExtentReports Related properties
        private ExtentReports extentReports;
        private ExtentTest testInstance;

        public string GetTimestamp { get; private set; }

        public CWebDriver(IObjectContainer objectContainer)
        {
            objectContainerPrivate = objectContainer;
            
            //init webDriver
            webdriver = SetWebdriver();

            //init Reports
            var htmlReports = new ExtentHtmlReporter(Constants.ExtentReportFile);
            extentReports = new ExtentReports();
            extentReports.AttachReporter(htmlReports);
        }

        public IWebDriver GetDriver()
        {
            return webdriver;
        }

        public void CreateTest(String testName)
        {
            testInstance = extentReports.CreateTest(testName);
        }

        public ExtentTest GetTestReportInstance()
        {
            return testInstance;
        }

        public void LogStep(StepInfo stepInfo)
        {
            if (ConfigurationManager.AppSettings["step.screenshot"].Contains("true"))
            {
                testInstance.Log(Status.Pass, FormatUtils.formatCamelCaseText(stepInfo.StepDefinitionType + stepInfo.Text), MediaEntityBuilder.CreateScreenCaptureFromPath(generateScreenshot()).Build());
            }
            else
            {
                testInstance.Log(Status.Pass, FormatUtils.formatCamelCaseText(stepInfo.StepDefinitionType + stepInfo.Text));
                //testInstance.AddScreenCaptureFromPath(generateScreenshot());
            }
        }

        internal void Flush()
        {
            extentReports.Flush();
        }

        private string generateScreenshot()
        {

            itemCount++;
            Screenshot ss = ((ITakesScreenshot)webdriver).GetScreenshot();
            string timeStamp = FormatUtils.GetTimestamp(DateTime.Now);

            String shotName = itemCount + "-" + Constants.PICTURE_NAME + "-" + timeStamp + ".png";
            String shotNamePath = System.IO.Path.Combine(Constants.ReportPath, @shotName);

            ss.SaveAsFile(shotNamePath, ScreenshotImageFormat.Png);

            return shotNamePath;
        }

        private IWebDriver SetWebdriver()
        {
            if (ConfigurationManager.AppSettings["webdriver.driver"].Contains("firefox"))
            {
                var profile = new FirefoxProfile();
                profile.SetPreference("webdriver_assume_untrusted_issuer", true);
                return new FirefoxDriver(profile);
            }
            else
            if (ConfigurationManager.AppSettings["webdriver.driver"].Contains("iexplorer"))
            {
                return new InternetExplorerDriver();
            }
            else
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--start-maximized");
                options.AddArgument("--ignore-certificate-errors");
                return new ChromeDriver(options);
            }
        }
    }
}
