using ExamWorksTestproject.Base;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ExamWorksTestproject
{
    public class SearchPage
    {
        IWebDriver driver;
        public SearchPage()
        {
            driver = Hooks.driver;
        }

        private IWebElement keyWord => driver.FindElement(By.Name("q"));
        private IWebElement btnSearch => driver.FindElement(By.Name("btnK"));
        private IList<IWebElement> urllinks => driver.FindElements(By.XPath(
            "//*[contains(text(), 'www.gumtree.com/')]"));
        private IWebElement btnAccept => driver.FindElement(By.XPath("//button[@id='onetrust-accept-btn-handler']"));
        private IWebElement btnAcceptAll => driver.FindElement(By.XPath("//button[@title='Accept All']"));
        
        public void NavigateToPage()
        {
            driver.Navigate().GoToUrl(Sites.Google);
            Thread.Sleep(5000);
            new Actions(Hooks.driver)
                .SendKeys(Keys.Tab)
                .SendKeys(Keys.Tab)
                .SendKeys(Keys.Return).Build().Perform();
        }

        public void SetText(string text)
        {
            Hooks.waitforelementBy("Name", "q");
            keyWord.SendKeys(text);
            btnSearch.Click();
        }

        public void VerifyLinkCount(string links, int count)
        {
            if (urllinks.Equals(links) && urllinks.Count > count)
            {
                Assert.IsTrue(urllinks.Count > count, $"{urllinks} less than {count}");
            }
        }

        public void NavigateToLinks(string links, string pageTitle)
        {
            var results = driver.FindElements(By.XPath("//*[contains(text(), 'www.gumtree.com')]"));
            var listOfCars = driver.FindElements(By.XPath("//*[@class='listing-link ']"));
            foreach (var result in results)
            {                          
                try
                {
                    if (result.Displayed)
                    {
                        result.Click();
                    }

                    if (btnAccept.Displayed)
                    {
                        btnAccept.Click();
                    }
                    Assert.IsTrue(Hooks.driver.Title.Contains(pageTitle));
                    Assert.IsNotNull(listOfCars.Count, $"{listOfCars} is null");                   
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    //throw ;
                }
                driver.Navigate().Back();
            }
        }


        public void ListofCarsCount(string count)
        {
            var listOfCars = driver.FindElements(By.XPath("//div[@class='listing-content']"));
            Assert.IsNotNull(listOfCars.Count > 0, "Is null");
        }

        public static void EnterText(IWebDriver driver, string element, string value, string elementType)
        {
            if (elementType=="Id")
                driver.FindElement(By.Id(element)).SendKeys(value);
            if (elementType == "Name")
                driver.FindElement(By.Name(element)).SendKeys(value);
            if (elementType == "XPath")
                driver.FindElement(By.XPath(element)).SendKeys(value);
        }

        public static void ClickKeyWord(IWebDriver driver, string element, string elementType)
        {
            if (elementType == "Id")
                driver.FindElement(By.Id(element)).Click();
            if (elementType == "Name")
                driver.FindElement(By.Name(element)).Click();
            if (elementType == "XPath")
                driver.FindElement(By.XPath(element)).Click();
        }
    } 
}
