using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("tbody tr")));
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementIsVisible(countText));

            String countRawText = webdriver.FindElement(countText).Text;

            Console.WriteLine("Full Table Count Text: {0}", countRawText);

            var count = countRawText.Split();
            result = Convert.ToInt32(count[count.Length - 2]);

            return result;
        }

    }
}
