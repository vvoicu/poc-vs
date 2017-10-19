using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoCTestProject.Com.Configs
{
    class SingleReporter
    {

        private static SingleReporter instance;

        private ExtentReports extentReports;
        private ExtentHtmlReporter htmlReporter;

        private SingleReporter() { }

        public static SingleReporter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingleReporter();
                }
                return instance;
            }
        }
    }
}
