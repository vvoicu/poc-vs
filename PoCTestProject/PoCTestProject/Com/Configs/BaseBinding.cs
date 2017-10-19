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
        static ProcessThreadCollection initialThreadCount;

        public BaseBinding(IObjectContainer objectContainer)
        {
            initialThreadCount = Process.GetCurrentProcess().Threads;
            Console.WriteLine("Initial process number of threads: {0}", initialThreadCount.Count);
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

            Console.WriteLine("Main program: {0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().FullName);

            Console.WriteLine("Current thread id: {0}", Thread.CurrentThread.ManagedThreadId);

            ProcessThreadCollection currentThreads = Process.GetCurrentProcess().Threads;
            Console.WriteLine("Current process number of threads: {0}", currentThreads.Count);

            Console.WriteLine("Current process id: {0}", Process.GetCurrentProcess().Id);

            //foreach (Process winProc in Process.GetProcesses())
            //{
            //    Console.WriteLine("Process {0}: {1}. Thread count: {2}", winProc.Id, winProc.ProcessName, winProc.Threads.Count);
            //}
            foreach (ProcessThread winProc in currentThreads)
            {
                Console.WriteLine("id {0} state: {1}. start time: {2}, container: {3}", winProc.Id, winProc.ThreadState, winProc.StartTime, winProc.Container);
            }
            if (initialThreadCount.Count == currentThreads.Count)
                //write report to file
                report.Flush();
        }



    }
}
