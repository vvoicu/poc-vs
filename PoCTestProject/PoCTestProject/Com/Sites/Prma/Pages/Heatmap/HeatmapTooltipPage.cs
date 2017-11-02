using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PoCTestProject.Com.DataModels;
using PoCTestProject.Com.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoCTestProject.Com.Sites.Prma.Pages.Heatmap
{
    class HeatmapTooltipPage
    {
        private IWebDriver webdriver;
        
        public HeatmapTooltipPage(IWebDriver driver)
        {
            webdriver = driver;
        }

        private By tooltipContainer = By.CssSelector(".heatmap-data-cell-tooltip");

        /// <summary>
        /// Once you have opened a tooltip, details will be extracted from the positive statuses. The method will not gather the tooltip summary like totals, title and such
        /// </summary>
        /// <returns></returns>
        public IList<HeatmapTooltipModel> GrabTooltipData()
        {
            IList<HeatmapTooltipModel> results = new List<HeatmapTooltipModel>();

            new WebDriverWait(webdriver, TimeSpan.FromSeconds(Constants.WAIT_TIME_DEFAULT)).Until(ExpectedConditions.ElementToBeClickable(tooltipContainer));
            IList<IWebElement> positiveItems = webdriver.FindElements(By.CssSelector(".heatmap-data-cell-tooltip li[ng-repeat*='positiveStatuses']"));

            //tooltip row level data
            foreach(IWebElement currentItem in positiveItems)
            {
                IList<IWebElement> itemDetails = currentItem.FindElements(By.CssSelector("a"));

                HeatmapTooltipModel currentResult = new HeatmapTooltipModel();

                //row details data - such as count, color, vulnerabilities, urlLinks
                foreach (IWebElement itemNow in itemDetails)
                {
                    var itemRawText = itemNow.Text;
                    var itemUrl = itemNow.GetAttribute("href");

                    //tooltip sentence "1 requirement rated red" or "0 are key vulnerabilities"
                    var count = itemRawText.Split().First();
                    var text = itemRawText.Split().Last();

                    if (text.Contains("vulnerabilities"))
                    {
                        currentResult.vulnerabilityText = count;
                        currentResult.vulnerabilityLink = itemUrl;
                    }
                    else
                    {
                        currentResult.color = text;
                        currentResult.colorText = count;
                        currentResult.colorLink = itemUrl;
                    }
                }

                results.Add(currentResult);
            }
            return results;
        }
        
    }
}
