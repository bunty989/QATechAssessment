using AventStack.ExtentReports;
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
            _test.Log(Status.Info, "Verifying the landing page");
            Assert.That(WebAssertions.IsWebElementDisplayed(searchBox), Is.True);
            _test.Pass("<div style='color:green; font-weight :bold'> Landing page is displayed successfully </div>",
                AttachScreenShot(null));
        }

        [Test]
        [Retry(3)]
        public void VerifyLocateUsMenu()
        {
            var locateUsMenu = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "LinkText", "Find locations");
            _test.Log(Status.Info, "Verifying the Locate Us menu");
            Assert.That(WebAssertions.IsWebElementDisplayed(locateUsMenu), Is.True);
            _test.Pass("<div style='color:green; font-weight :bold'> Locate Us menu is displayed successfully </div>", AttachScreenShot(null));
        }

        [Test]
        [Retry(3)]
        public void ClickOnLocateUsMenu()
        {
            var locateUsMenu = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "LinkText", "Find locations");
            IdentifyWebElementsPerformAction.PerformWebdriverAction(webDriver,locateUsMenu,"Click", null);
            _test.Log(Status.Info, "Clicking on Locate Us menu");
            Assert.That(webDriver.Url.EndsWith("service-centre"), Is.True);
            _test.Pass("<div style='color:green; font-weight :bold'> Clicked on Locate Us menu successfully </div>", AttachScreenShot(null));
        }

        [Test]
        [Retry(3)]
        public void VerifyLocatorSearchTextBox()
        {
            var locateUsMenu = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "LinkText", "Find locations");
            IdentifyWebElementsPerformAction.PerformWebdriverAction(webDriver, locateUsMenu, "Click", null);
            _test.Log(Status.Info, "Clicking on the Locate us Menu");
            var locatorTextBox = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "CssSelector", "[name='search-bar']");
            Assert.That(WebAssertions.IsWebElementDisplayed(locatorTextBox), Is.True);
            _test.Pass("<div style='color:green; font-weight :bold'> Locator search text box is displayed successfully </div>", AttachScreenShot(null));
        }

        [Test]
        [Retry(3)]
        public void VerifyLocatorSearchButton()
        {
            var locatorButton = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "CssSelector", "[aria-label='Search']");
            _test.Log(Status.Info, "Verifying the Locator search button");
            Assert.That(WebAssertions.IsWebElementDisplayed(locatorButton), Is.True);
            _test.Pass("<div style='color:green; font-weight :bold'> Locator search button is displayed successfully </div>", AttachScreenShot(null));
        }

        [Test]
        [Retry(3)]
        public void SearchServiceLocations()
        {
            var locatorTextBox = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "CssSelector", "[aria-label='Search']");
            var locatorButton = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "Xpath", ".//*[@type='submit']");
            IdentifyWebElementsPerformAction.PerformWebdriverAction(webDriver,locatorTextBox,"Input", TestData[4]);
            _test.Log(Status.Info, "Entering the search text in the locator search text box");
            locatorTextBox.SendKeys(Keys.Enter);
            _test.Log(Status.Info, "Pressing Enter key to search for service locations");
            Assert.That(webDriver.FindElement(By.PartialLinkText(TestData[5])).Displayed, Is.True);
            _test.Pass("<div style='color:green; font-weight :bold'> Service locations are displayed successfully </div>", AttachScreenShot(null));
        }

    }
}
