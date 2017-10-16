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

        public CWebDriver(IObjectContainer objectContainer)
        {
            objectContainerPrivate = objectContainer;
            //init webDriver
            webdriver = setWebdriver();

            //init Reports
            var htmlReports = new ExtentHtmlReporter(Constants.EXTENT_REPORT_FILE);
            extentReports = new ExtentReports();
            extentReports.AttachReporter(htmlReports);
        }

        internal void Flush()
        {
            extentReports.Flush();
        }

        private void setReportConfig()
        {


        }

        private IWebDriver setWebdriver()
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

        public void createTest(String testName)
        {
            testInstance = extentReports.CreateTest(testName);
        }

        internal void Pass()
        {
            testInstance.Pass("we Dids it");
        }

        internal void assignCategory(string v)
        {
            testInstance.AssignCategory(v);
        }

        public void logInfo(String message)
        {
            testInstance.Log(Status.Info, message);
        }

        public IWebDriver getDriver()
        {
            return webdriver;
        }

        public void setDriver(IWebDriver driver)
        {
            webdriver = driver;
        }


    }
}
