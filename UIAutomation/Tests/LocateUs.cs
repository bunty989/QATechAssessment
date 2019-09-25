using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UIAutomation.Framework;

namespace UIAutomation.Tests
{
    internal class LocateUs : TestBase
    {
        public static string strTestDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"\Data", "TestData.csv");
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
        public void VerifyLocateUsMenu()
        {
            var locateUsMenu = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "LinkText", "Find locations");
            Assert.IsTrue(WebAssertions.IsWebElementDisplayed(locateUsMenu));
        }

        [Test, Order(2)]
        public void ClickOnLocateUsMenu()
        {
            var locateUsMenu = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "LinkText", "Find locations");
            IdentifyWebElementsPerformAction.PerformWebdriverAction(webDriver,locateUsMenu,"Click", "");
            Assert.IsTrue(webDriver.Url.EndsWith("service-centre"));
        }

        [Test, Order(4)]
        public void VerifyLocatorSearchTextBox()
        {
            var locatorTextBox = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "Id", "locatorTextSearch");
            Assert.IsTrue(WebAssertions.IsWebElementDisplayed(locatorTextBox));
        }

        [Test, Order(5)]
        public void VerifyLocatorSearchButton()
        {
            var locatorButton = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "Xpath", ".//*[@type='submit']");
            Assert.IsTrue(WebAssertions.IsWebElementDisplayed(locatorButton));
        }

        [Test, Order(6)]
        public void SearchServiceLocations()
        {
            var locatorTextBox = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "Id", "locatorTextSearch");
            var locatorButton = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "Xpath", ".//*[@type='submit']");
            IdentifyWebElementsPerformAction.PerformWebdriverAction(webDriver,locatorTextBox,"Input", TestData[4]);
            locatorTextBox.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            Assert.IsTrue(webDriver.FindElement(By.PartialLinkText(TestData[5])).Displayed);
        }

    }
}
