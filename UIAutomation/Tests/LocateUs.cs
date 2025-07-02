using NUnit.Framework;
using OpenQA.Selenium;
using UIAutomation.Framework;

namespace UIAutomation.Tests
{
    internal class LocateUs : TestBase
    {
        public static string strTestDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "TestData.csv");
        List<string> TestData = TestDataHelper.ReadInCSV("TestData.csv");


        [Test]
        [Retry(3)]
        public void VerifyLandingPage()
        {
            var searchBox = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "XPath", ".//*[@class='form__text form--large']");
            Assert.That(WebAssertions.IsWebElementDisplayed(searchBox), Is.True);
        }

        [Test]
        [Retry(3)]
        public void VerifyLocateUsMenu()
        {
            var locateUsMenu = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "LinkText", "Find locations");
            Assert.That(WebAssertions.IsWebElementDisplayed(locateUsMenu), Is.True);
        }

        [Test]
        [Retry(3)]
        public void ClickOnLocateUsMenu()
        {
            var locateUsMenu = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "LinkText", "Find locations");
            IdentifyWebElementsPerformAction.PerformWebdriverAction(webDriver,locateUsMenu,"Click", null);
            Assert.That(webDriver.Url.EndsWith("service-centre"), Is.True);
        }

        [Test]
        [Retry(3)]
        public void VerifyLocatorSearchTextBox()
        {
            var locateUsMenu = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "LinkText", "Find locations");
            IdentifyWebElementsPerformAction.PerformWebdriverAction(webDriver, locateUsMenu, "Click", null);
            var locatorTextBox = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "CssSelector", "[name='search-bar']");
            Assert.That(WebAssertions.IsWebElementDisplayed(locatorTextBox), Is.True);
        }

        [Test]
        [Retry(3)]
        public void VerifyLocatorSearchButton()
        {
            var locatorButton = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "CssSelector", "[aria-label='Search']");
            Assert.That(WebAssertions.IsWebElementDisplayed(locatorButton), Is.True);
        }

        [Test]
        [Retry(3)]
        public void SearchServiceLocations()
        {
            var locatorTextBox = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "CssSelector", "[aria-label='Search']");
            var locatorButton = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "Xpath", ".//*[@type='submit']");
            IdentifyWebElementsPerformAction.PerformWebdriverAction(webDriver,locatorTextBox,"Input", TestData[4]);
            locatorTextBox.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            Assert.That(webDriver.FindElement(By.PartialLinkText(TestData[5])).Displayed, Is.True);
        }

    }
}
