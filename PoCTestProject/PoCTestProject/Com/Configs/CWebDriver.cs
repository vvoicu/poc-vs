using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using PoCTestProject.Com.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoCTestProject.Com.Configs
{
    public class CWebDriver
    {
        private readonly IObjectContainer objectContainerPrivate;
        private IWebDriver webdriver;
        private ExtentReports extentReports;
        private ExtentTest testInstance;
        private static int itemCount = 0;

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

        public void LogStep(String message)
        {
            testInstance.Log(Status.Info, FormatUtils.formatCamelCaseText(message));
            testInstance.AddScreenCaptureFromPath(generateScreenshot());
        }

        internal void Flush()
        {
            extentReports.Flush();
        }

        private string generateScreenshot()
        {

            itemCount++;
            Screenshot ss = ((ITakesScreenshot)webdriver).GetScreenshot();

            String shotName = itemCount + "-" + Constants.PICTURE_NAME + "-SS.png";
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
