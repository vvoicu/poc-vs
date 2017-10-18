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
        private ExtentReports extentReports;
        private ExtentHtmlReporter htmlReports;
        //private static ExtentReports extentReports;
        //private static ExtentHtmlReporter htmlReports;
        private ExtentTest testInstance;

        public CWebDriver(IObjectContainer objectContainer)
        {
            objectContainerPrivate = objectContainer;

            //init webDriver
            webdriver = SetWebdriver();

            //init reports
            SetReportsConfiguration();

            //objectContainerPrivate.RegisterInstanceAs<IWebDriver>(webdriver);
        }

        public IWebDriver GetDriver()
        {
            return webdriver;
        }

        public void CreateTest(String testName)
        {
            testInstance = extentReports.CreateTest(testName);
            // testInstance = extentReports.CreateTest(testName).CreateNode(DateTime.Now.ToString());
        }

        public ExtentTest GetTestReportInstance()
        {
            return testInstance;
        }

        public void LogStep(StepInfo stepInfo)
        {
            if (ConfigurationManager.AppSettings["step.screenshot"].Contains("true"))
            {
                //                testInstance.Log(Status.Pass, FormatUtils.formatCamelCaseText(stepInfo.StepDefinitionType + stepInfo.Text), MediaEntityBuilder.CreateScreenCaptureFromPath(generateScreenshot()).Build());
                testInstance.Log(Status.Pass, FormatUtils.formatCamelCaseText(stepInfo.StepDefinitionType + stepInfo.Text));

                testInstance.AddScreenCaptureFromPath(generateScreenshot());
            }
            else
            {
                testInstance.Log(Status.Pass, FormatUtils.formatCamelCaseText(stepInfo.StepDefinitionType + stepInfo.Text));
                //testInstance.AddScreenCaptureFromPath(generateScreenshot());
            }
        }

        public void Flush()
        {
            extentReports.Flush();
            //extentReports = null;
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
                htmlReports = new ExtentHtmlReporter(Thread.CurrentThread.ManagedThreadId + "-" + FormatUtils.GetTimestamp(DateTime.Now) + Constants.ExtentReportFile);
                //htmlReports = new ExtentHtmlReporter(ScenarioContext.Current.ScenarioInfo.Title + "-" + Constants.ExtentReportFile);
                //htmlReports = new ExtentHtmlReporter(Constants.ExtentReportFile);
                htmlReports.AppendExisting = true;
                extentReports = new ExtentReports();

                extentReports.AttachReporter(htmlReports);

                //htmlReports.Configuration().ReportName = "PoC x PRMA " + DateTime.Now;
                //htmlReports.Configuration().DocumentTitle = "PRMA Test Report";
                //htmlReports.Configuration().Theme = Theme.Dark;
            }

           
        }
    }
}
