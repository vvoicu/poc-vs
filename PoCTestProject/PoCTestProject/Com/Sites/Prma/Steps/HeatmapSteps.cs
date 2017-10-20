using PoCTestProject.Com.Configs;
using PoCTestProject.Com.Sites.Prma.Pages;
using System.Configuration;
using TechTalk.SpecFlow;

namespace PoCTestProject.Com.Sites.Prma.Steps
{
    [Binding]
    public class HeatmapSteps
    {
        private CWebDriver webdriver;
        HeatmapPage heatmapPage;

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

        [When(@"I select a coloured cell")]
        public void WhenISelectAColouredCell(int requirementsNumber)
        {
            webdriver.LogStep(ScenarioContext.Current.StepContext.StepInfo);
            heatmapPage = new HeatmapPage(webdriver.GetDriver());
            heatmapPage.ClickOnColouredCell();
        }
        
        [When(@"I check the number of each type of requirement")]
        public void WhenICheckTheNumberOfEachTypeOfRequirement()
        {
            webdriver.LogStep(ScenarioContext.Current.StepContext.StepInfo);
            heatmapPage = new HeatmapPage(webdriver.GetDriver());
        }
        
        [When(@"I click on the total number of requirements link")]
        public void WhenIClickOnTheTotalNumberOfRequirementsLink()
        {
            webdriver.LogStep(ScenarioContext.Current.StepContext.StepInfo);
            heatmapPage = new HeatmapPage(webdriver.GetDriver());
        }
        
        [Then(@"I am redirected to the requirements page")]
        public void ThenIAmRedirectedToTheRequirementsPage()
        {
            webdriver.LogStep(ScenarioContext.Current.StepContext.StepInfo);
            heatmapPage = new HeatmapPage(webdriver.GetDriver());
        }

    }
}
