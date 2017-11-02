using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using PoCTestProject.Com.Tools;
using System;
using System.Configuration;
using System.Threading;
using TechTalk.SpecFlow;

namespace PoCTestProject.Com.Configs
{

    public class CWebDriver 
    {
        private readonly IObjectContainer objectContainerPrivate;

        //Selenium Related properties
        private IWebDriver webdriver;
        private int itemCount = 0;

        //ExtentReports Related properties
        private static ExtentReports extentReports;
        private static ExtentHtmlReporter htmlReports;
        private ExtentTest testInstance;

        public CWebDriver(IObjectContainer objectContainer)
        {
            objectContainerPrivate = objectContainer;

            //init webDriver
            webdriver = SetWebdriver();

            //init reports
            SetReportsConfiguration();
        }

        internal ExtentReports GetExtentReport()
        {
            return extentReports;
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

        public void LogStep(StepInfo stepInfo, string prefix)
        {
            if (ConfigurationManager.AppSettings["step.screenshot"].Contains("true"))
            {
                testInstance.Log(Status.Pass, prefix + " " + FormatUtils.formatCamelCaseText(stepInfo.StepDefinitionType + stepInfo.Text));
                testInstance.AddScreenCaptureFromPath(generateScreenshot());
            }
            else
            {
                testInstance.Log(Status.Pass, prefix + " " + FormatUtils.formatCamelCaseText(stepInfo.StepDefinitionType + stepInfo.Text));
            }
        }

        public void LogStep(StepInfo stepInfo)
        {
            LogStep(stepInfo, "");
        }

        public void Flush()
        {
            extentReports.Flush();
        }

        private string generateScreenshot()
        {
            itemCount++;
            Screenshot ss = ((ITakesScreenshot)webdriver).GetScreenshot();
            string timeStamp = FormatUtils.GetTimestamp(DateTime.Now);

            String shotName = ScenarioContext.Current.ScenarioInfo.Title + "-" + itemCount + "-" + timeStamp + ".png";
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

        private void SetReportsConfiguration()
        {
            if (extentReports == null)
            {
                //init Reports
                //htmlReports = new ExtentHtmlReporter(Constants.ExtentReportFile);
                htmlReports = new ExtentHtmlReporter(Thread.CurrentThread.ManagedThreadId + "-" + FormatUtils.GetTimestamp(DateTime.Now) + "-" + Constants.ExtentReportFile);

                //THERE IS A BUG IN EXTENTREPORTS - when fixed multiple test threads will be able to aggregate in a single report
                htmlReports.AppendExisting = true;
                extentReports = new ExtentReports();

                extentReports.AttachReporter(htmlReports);

                htmlReports.Configuration().ReportName = "PoC x PRMA " + DateTime.Now;
                htmlReports.Configuration().DocumentTitle = "PRMA Test Report";
                //htmlReports.Configuration().Theme = Theme.Dark;
            }
        }
    }
}
