using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace PoCTestProject.Com.Sites.Prma.Pages
{
    class HeatmapPage
    {
        private IWebDriver webdriver;

        public HeatmapPage(IWebDriver driver)
        {
            webdriver = driver;
        }

        // cell locators
        private By cellListLocator = By.CssSelector(".data-cells .cell");
        private By statusRedLocator = By.CssSelector(".status-red");
        private By statusAmberLocator = By.CssSelector(".status-amber");
        private By statusGreenLocator = By.CssSelector(".status-green");
        private By statusNoneLocator = By.CssSelector(".status-none");
        private By statusUnassignedLocator = By.CssSelector(".status-unassigned");

        // cell tooltip locators
        private By cellTooltipLocator = By.CssSelector(".heatmap-data-cell-tooltip");
        private By tooltipHeaderLocator = By.CssSelector(".heatmap-data-cell-tooltip .header");


        public void ClickOnColouredCell()
        {
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(cellListLocator));
            IList<IWebElement> cellList = webdriver.FindElements(cellListLocator);

            foreach (IWebElement cell in cellList)
            {
                if (!cell.GetAttribute("class").Contains("empty"))
                {
                    cell.Click();
                    if (!cell.FindElement(statusNoneLocator).GetAttribute("style").Contains("height: 100%"))
                    {
                        break;
                    }
                }
            }
        }

        public void GetCellInformation()
        {
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(cellTooltipLocator));

        }



    }
}
