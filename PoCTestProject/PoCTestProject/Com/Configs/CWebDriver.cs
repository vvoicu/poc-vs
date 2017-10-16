using AventStack.ExtentReports;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
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

        public CWebDriver(IObjectContainer objectContainer)
        {
            objectContainerPrivate = objectContainer;
            webdriver = new ChromeDriver();
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
