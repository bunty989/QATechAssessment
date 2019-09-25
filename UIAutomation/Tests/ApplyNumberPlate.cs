using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UIAutomation.Framework;

namespace UIAutomation.Tests 
{
    internal class ApplyNumberPlate : TestBase
    {
        public static string strTestDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"\Data","TestData.csv");
        List<string> TestData = TestDataHelper.ReadInCSV("TestData.csv");

        [OneTimeSetUp]
        public void Init()
        {
            
            webDriver.Navigate().GoToUrl(WebDriverConfigurationSettings.ConfigSetting(TestConstants.ConfigTypes.WebDriverConfiguration, TestConstants.ConfigTypesKey.Url));
            WebPageStateChecker.PollForReadyState(webDriver, Int16.Parse(WebDriverConfigurationSettings.ConfigSetting(TestConstants.ConfigTypes.WebDriverConfiguration, TestConstants.ConfigTypesKey.GlobalTimeout)));
        }

        [Test, Order(1)]
        public void VerifyLandingPage()
        {
            var searchBox = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "XPath", ".//*[@class='form__text form--large']");
            Assert.IsTrue(WebAssertions.IsWebElementDisplayed(searchBox));
        }

        [Test, Order(2)]
        public void VerifySearchButton()
        {
            var searchButton = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "XPath", ".//*[@class='button button--primary']");
            Assert.IsTrue(WebAssertions.IsWebElementDisplayed(searchButton));
        }

        [Test, Order(3)]
        public void SearchServices()
        {
            var searchBox = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "XPath", ".//*[@class='form__text form--large']");
            var searchButton = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "XPath", ".//*[@class='button button--primary']");
            IdentifyWebElementsPerformAction.PerformWebdriverAction(webDriver, searchBox, "Input", TestData[3]);
            webDriver.FindElement(By.XPath(".//*[@class='autocomplete__option']")).Click();
            var searchResults = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "XPath", ".//*[@class='search__title']");
            Assert.IsTrue(searchResults.Text.Contains(TestData[3].ToLower()));
            String[] searchQuery = TestData[3].ToLower().Split(" ");
            Assert.IsTrue(webDriver.Url.EndsWith(String.Join("+", searchQuery)));
        }
    }
}
