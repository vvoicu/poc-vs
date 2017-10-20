using PoCTestProject.Com.Configs;
using System;
using System.Configuration;
using TechTalk.SpecFlow;

namespace PoCTestProject.Com.Sites.Prma.Steps
{
    [Binding]
    public class HeatmapSteps
    {
        private CWebDriver webdriver;

        public HeatmapSteps(CWebDriver driver)
        {
            webdriver = driver;
        }

        [When(@"I navigate to the heatmap URL")]
        public void WhenINavigateToTheHeatmapURL()
        {
            webdriver.LogStep(ScenarioContext.Current.StepContext.StepInfo);
            var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            webdriver.GetDriver().Navigate().GoToUrl(baseUrl + "/heatmap");
        }

        [When(@"I select a square with '(.*)' requirements")]
        public void WhenISelectASquareWithRequirements(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I check the number of each type of requirement")]
        public void WhenICheckTheNumberOfEachTypeOfRequirement()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I click on the total number of requirements link in 'Evidence synthesis'")]
        public void WhenIClickOnTheTotalNumberOfRequirementsLinkIn()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I am redirected to the requirements page")]
        public void ThenIAmRedirectedToTheRequirementsPage()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"each requirement is displayed with the correct colour")]
        public void ThenEachRequirementIsDisplayedWithTheCorrectColour()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
