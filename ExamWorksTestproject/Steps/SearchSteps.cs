using ExamWorksTestproject.Base;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace ExamWorksTestproject.Steps
{
    [Binding]
    public class SearchSteps:SearchPage
    {

        [Given(@"I am on google page")]
        public void GivenIAmOnGooglePage()
        {
            NavigateToPage();
        }
        
        [Given(@"I search for '(.*)'")]
        public void GivenISearchForCarsInLondon(string Keyword)
        {
            SetText(Keyword);
        }
        
        [Then(@"search result is displayed '(.*)")]
        public void GivenSearchResultIsDisplayed(string Keyword)
        {
            Assert.IsNotNull(Hooks.driver.PageSource.Contains(Keyword));
        }
        
        [When(@"I navigate to each '(.*)' links and check title contains '(.*)'")]
        public void WhenINavigateToEachLinks(string links, string pagetitle)
        {
            NavigateToLinks(links, pagetitle);
        }
        
        [Then(@"I validate '(.*)' links available is greater than '(.*)'")]
        public void ThenIValidateLinksAvailable(string link, int count)
        {
            VerifyLinkCount(link, count);
        }
        
        [Then(@"validate car count is greater than '(.*)'")]
        public void ThenValidateCarCountIsGreaterThan(int count)
        {
            //ListofCarsCount(count.ToString());
        }
    }
}
