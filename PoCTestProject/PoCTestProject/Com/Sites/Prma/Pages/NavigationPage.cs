using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace PoCTestProject.Com.Sites.Prma.Pages
{
    class NavigationPage
    {
        private IWebDriver webdriver;

        public NavigationPage(IWebDriver driver)
        {
            webdriver = driver;
        }

        public By navigationContainer = By.CssSelector("div.sidemenu");

        private void clickOnMenu(String menuLabel)
        {
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementIsVisible(navigationContainer));

            IList<IWebElement> menuList = webdriver.FindElements(By.CssSelector("div.sidemenu div.sidemenu div.section"));

            foreach (IWebElement menuNow in menuList)
            {
                if (menuNow.Text.ToLower().Contains(menuLabel.ToLower()))
                {
                    menuNow.Click();
                    break;
                }
            }
        }
        
        public void clickOnHeatmap()
        {
            clickOnMenu("heatmap");
        }

    }
}
