using AventStack.ExtentReports;
using BoDi;
using PoCTestProject.Com.Configs;
using System;
using System.Diagnostics;
using System.Threading;
using TechTalk.SpecFlow;

namespace PoCTestProject.Com.Selenium
{
    [Binding]
    public class BaseBinding
    {

        private readonly IObjectContainer objectContainerPrivate;
        private CWebDriver webdriver;
        private static ExtentReports report;
        static int concurrentThreads = 0;

        public BaseBinding(IObjectContainer objectContainer)
        {
            objectContainerPrivate = objectContainer;
        }

        [BeforeScenario]
        public void SetUp()
        {
            //Thread.
            //initialize webdriver
            webdriver = new CWebDriver(objectContainerPrivate);
            webdriver.GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            //initialize test name
            webdriver.CreateTest(ScenarioContext.Current.ScenarioInfo.Title);
            webdriver.GetTestReportInstance().AssignCategory(ScenarioContext.Current.ScenarioInfo.Tags);

            report = webdriver.GetExtentReport();
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

            //close driver
            webdriver.GetDriver().Quit();


            int threadsRemaining = Interlocked.Increment(ref concurrentThreads);


            ProcessThreadCollection currentThreads = Process.GetCurrentProcess().Threads;
            foreach(Process processNow in currentThreads)
            {
                Console.WriteLine("Process: {0}", Process.GetProcessById(123));

            }


            System.Diagnostics.Debug.WriteLine("----*****----Here: " + currentThreads.Count);
            Trace.WriteLine("----*****----Here: " + currentThreads.Count);
            Console.WriteLine("----*****----currentThreads: {0}", currentThreads.Count);
            Console.WriteLine("----*****----threadsRemaining: {0}", threadsRemaining);
            // if (currentThreads.Count == 1)
            //write report to file
            report.Flush();
        }
    }
}
