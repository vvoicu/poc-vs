using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoCTestProject.Com.Configs;
using PoCTestProject.Com.DataModels;
using PoCTestProject.Com.Sites.Prma.Pages;
using PoCTestProject.Com.Sites.Prma.Pages.Heatmap;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace PoCTestProject.Com.Sites.Prma.Steps
{
    [Binding]
    public class HeatmapSteps
    {
        private CWebDriver webdriver;
        HeatmapListPage heatmapPage;
        HeatmapTooltipPage heatmapTooltipPage;

        public HeatmapSteps(CWebDriver driver)
        {
            webdriver = driver;
        }


        [When(@"I select a coloured cell '(.*)'")]
        public void WhenISelectAColouredCell(string requirementsNumber)
        {
            //webdriver.LogStep(ScenarioContext.Current.StepContext.StepInfo);
            heatmapPage = new HeatmapListPage(webdriver.GetDriver());
            heatmapPage.GrabCellsColorsData();
        }

        [When(@"I select a coloured cell with '(.*)' colors")]
        public void WhenISelectAColouredCellWithColors(int colorCount)
        {
            //webdriver.LogStep(ScenarioContext.Current.StepContext.StepInfo);
            heatmapPage = new HeatmapListPage(webdriver.GetDriver());
            Boolean isFound = heatmapPage.ClickOnColouredCell(colorCount);

            Assert.IsTrue(isFound);
        }


        [When(@"I check the number of each requirement")]
        public void WhenICheckTheNumberOfEachRequirement()
        {
            //webdriver.LogStep(ScenarioContext.Current.StepContext.StepInfo);
            heatmapTooltipPage = new HeatmapTooltipPage(webdriver.GetDriver());

           IList<HeatmapTooltipModel> data = heatmapTooltipPage.GrabTooltipData();

            foreach(HeatmapTooltipModel dataNow in data)
            {
                Console.WriteLine("ItemData: {0}", dataNow.ToString());

                //check color data
                webdriver.GetDriver().Navigate().GoToUrl(dataNow.colorLink);
                RequirementsListPage requirementsListPage = new RequirementsListPage(webdriver.GetDriver());
                var actualColorCount = requirementsListPage.GrabItemCount();
                Assert.AreEqual(Convert.ToInt32(dataNow.colorText), actualColorCount);

                //check vulnerability data
                webdriver.GetDriver().Navigate().GoToUrl(dataNow.vulnerabilityLink);
                var actualVulnerabilityCount = requirementsListPage.GrabItemCount();
                Assert.AreEqual(Convert.ToInt32(dataNow.vulnerabilityText), actualVulnerabilityCount);

            }
        }
        
  
    }
}
