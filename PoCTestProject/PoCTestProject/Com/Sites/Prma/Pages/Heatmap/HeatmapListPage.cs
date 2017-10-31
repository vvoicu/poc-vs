using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PoCTestProject.Com.DataModels;
using PoCTestProject.Com.Sites.Prma.Steps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoCTestProject.Com.Sites.Prma.Pages
{
    class HeatmapListPage
    {
        private IWebDriver webdriver;

        public HeatmapListPage(IWebDriver driver)
        {
            webdriver = driver;
        }

        // cell locators
        private By cellListLocator = By.CssSelector(".data-cells span.cell");
        private By statusElementsLocator = By.CssSelector("div[class*='status']");


        // cell tooltip locators
        private By cellTooltipLocator = By.CssSelector(".heatmap-data-cell-tooltip");
        private By tooltipHeaderLocator = By.CssSelector(".heatmap-data-cell-tooltip .header");


        public void ClickOnColouredCell()
        {

        }

        public void GrabCellsColorsData()
        {

            IList<HeatmapCellModel> results = new List<HeatmapCellModel>(); 

            new WebDriverWait(webdriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[ng-show='heatmap.showSummaries']")));
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(cellListLocator));
            IList<IWebElement> cellList = webdriver.FindElements(cellListLocator);

            foreach(IWebElement elementNow in cellList)
            {
                HeatmapCellModel itemData = GetCellInformation(elementNow);

                results.Add(itemData);
            }
        }

        internal Boolean ClickOnColouredCell(int colorCount)
        {
            Boolean isFound = false;
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[ng-show='heatmap.showSummaries']")));
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(cellListLocator));
            IList<IWebElement> cellList = webdriver.FindElements(cellListLocator);

            foreach (IWebElement elementNow in cellList)
            {
                HeatmapCellModel itemData = GetCellInformation(elementNow);

                if(itemData.CalculateVisibleColors() == colorCount)
                {
                    elementNow.Click();
                    isFound = true;
                    break;
                }
            }

            return isFound;
        }

        public HeatmapCellModel GetCellInformation(IWebElement cell)
        {
            HeatmapCellModel result = new HeatmapCellModel();

            new WebDriverWait(webdriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(cell));

            IList<IWebElement> statusAreas = cell.FindElements(statusElementsLocator);

            foreach(IWebElement color in statusAreas)
            {
                var colorRawKey = color.GetAttribute("class");
                var colorRawValue = color.GetAttribute("style");

                var colorKey = colorRawKey.Replace("status-", "");
                var colorValue = colorRawValue
                    .Replace("width: 100%; height: ", "")
                    .Replace("%;", "")
                    .Replace("width: 0px; height: ", "")
                    .Replace("px;","")
                    .Trim();
                
                result.colors.Add(colorKey, colorValue);
            }
            result.CalculateVisibleColors();

            return result;
        }



    }
}
