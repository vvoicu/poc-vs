using PoCTestProject.Com.Configs;
using PoCTestProject.Com.Sites.Prma.Pages;
using TechTalk.SpecFlow;

namespace PoCTestProject.Com.Sites.Prma.Steps
{
    [Binding]
    class NavigationSteps
    {

        private CWebDriver webdriver;
        private NavigationPage navigationPage;

        public NavigationSteps(CWebDriver driver)
        {
            webdriver = driver;
        }

        [When(@"I navigate to the heatmap")]
        public void WhenINavigateToTheHeatmap()
        {
            navigationPage = new NavigationPage(webdriver.GetDriver());
            navigationPage.clickOnHeatmap();
        }

    }
}
