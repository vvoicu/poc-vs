using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PoCTestProject.Com.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoCTestProject.Com.Sites.Prma.Pages
{
    class RequirementsListPage
    {
        private IWebDriver webdriver;

        public RequirementsListPage(IWebDriver driver)
        {
            webdriver = driver;
        }
        
        public By countText = By.CssSelector(".pagination:first-child span.item-count");

        public int GrabItemCount()
        {
            int result = 0;

            //wait for page to load
            IWait<IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(webdriver, TimeSpan.FromSeconds(Constants.WAIT_TIME_DEFAULT));
            wait.Until(driver1 => ((IJavaScriptExecutor)webdriver).ExecuteScript("return document.readyState").Equals("complete"));
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(Constants.WAIT_TIME_DEFAULT)).Until(ExpectedConditions.ElementIsVisible(countText));

            //test moves to fast in some instances, need to wait for the field to be populated with the text
            System.Threading.Thread.Sleep(4000);
            
            String countRawText = webdriver.FindElement(countText).Text;
            Console.WriteLine("Full Table Count Text: {0}", countRawText);

            var count = countRawText.Split();
            result = Convert.ToInt32(count[count.Length - 2]);

            return result;
        }

    }
}
