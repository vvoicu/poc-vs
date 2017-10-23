using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

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
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[ng-show='heatmap.showSummaries']")));

            new WebDriverWait(webdriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(cellListLocator));
            IList<IWebElement> cellList = webdriver.FindElements(cellListLocator);
            cellList.Where(cell => !cell.GetAttribute("class").Contains("empty") || 
                            !cell.FindElement(statusNoneLocator).GetAttribute("style").Contains("height: 100%")).
                            ToList().ForEach(cell => cell.Click());
        }

        public void GetCellInformation()
        {
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(cellTooltipLocator));

        }



    }
}
