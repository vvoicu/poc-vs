using PoCTestProject.Com.Configs;
using PoCTestProject.Com.Sites.Prma.Pages;
using TechTalk.SpecFlow;

namespace PoCTestProject.Com.Sites.Prma.Steps
{
    [Binding]
    public class RequirementsSteps
    {
        private CWebDriver webdriver;
        RequirementsPage requirementsPage;

        public RequirementsSteps(CWebDriver driver)
        {
            webdriver = driver;
        }


        [Then(@"each requirement is displayed with the correct colour")]
        public void ThenEachRequirementIsDisplayedWithTheCorrectColour()
        {
            webdriver.LogStep(ScenarioContext.Current.StepContext.StepInfo);
            requirementsPage = new RequirementsPage(webdriver.GetDriver());
        }

    }
}
