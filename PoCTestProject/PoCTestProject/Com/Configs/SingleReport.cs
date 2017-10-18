//using AventStack.ExtentReports;
//using AventStack.ExtentReports.Reporter;
//using AventStack.ExtentReports.Reporter.Configuration;
//using PoCTestProject.Com.Tools;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PoCTestProject.Com.Configs
//{
//    public sealed class SingleReport
//    {

//        //private static SingleReport instance;
//        private static object syncLock = new object();

//        private static ExtentReports extentReports;
//        private static ExtentHtmlReporter htmlReports;

//        private SingleReport() { }
//        static SingleReport() { }

//        public static ExtentReports GetInstance()
//        {
//            if (extentReports == null)
//            {
//                lock (syncLock)
//                {
//                    CreateInstance("ExtentReports.html");
//                }
//            }
//            return extentReports;
//        }


//        public static ExtentReports CreateInstance(String fileName)
//        {
//            htmlReports = new ExtentHtmlReporter(Constants.ExtentReportFile);
//            htmlReports.AppendExisting = true;
//            htmlReports.Configuration().ReportName = "PoC x PRMA " + DateTime.Now;
//            htmlReports.Configuration().DocumentTitle = "PRMA Test Report";
//            htmlReports.Configuration().Theme = Theme.Dark;

//            extentReports = new ExtentReports();
//            extentReports.AttachReporter(htmlReports);

//            return extentReports;
//        }
//    }
//}
