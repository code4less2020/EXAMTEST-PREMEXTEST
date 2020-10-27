using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace ExamWorksTestproject.Base
{
    [Binding]
    public class Hooks
    {
        public static IWebDriver driver;
        private const bool IsHeadless = true;
        WebDriverWait wait;
        [BeforeScenario]
        public void BeforeScenario()
        {        
            var option = new ChromeOptions();
            option.AddArgument("-start-maximized");
            option.AddArgument("--no-sandbox");
            if (IsHeadless.Equals(false)) option.AddArgument("--headless");
            driver = new ChromeDriver(option);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }

        public static WebDriverWait waitforelementBy(string elementType, string element)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.PollingInterval = TimeSpan.FromSeconds(2);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Message = "Element not located";
            if (elementType == "Id") 
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id(element)));
            if (elementType == "Name")
                wait.Until(ExpectedConditions.ElementIsVisible(By.Name(element)));
            if (elementType == "XPath")
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(element)));
            return wait;
        }
    }
}
