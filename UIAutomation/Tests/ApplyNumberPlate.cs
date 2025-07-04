using AventStack.ExtentReports;
using NUnit.Framework;
using UIAutomation.Framework;

namespace UIAutomation.Tests 
{
    internal class ApplyNumberPlate : TestBase
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
            _test.Pass("<div style='color:green; font-weight:bold'> Landing page is displayed successfully </div>",
                AttachScreenShot(null));
        }

        [Test]
        [Retry(3)]
        public void VerifySearchButton()
        {
            var searchButton = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "XPath", ".//*[@class='button button--primary']");
            _test.Log(Status.Info, "Verifying the Search button");
            Assert.That(WebAssertions.IsWebElementDisplayed(searchButton), Is.True);
            _test.Pass("<div style='color:green; font-weight:bold'> Search button is displayed successfully </div>",
                AttachScreenShot(null));
        }

        [Test]
        [Retry(3)]
        public void SearchServices()
        {
            var searchBox = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "XPath", ".//*[@class='form__text form--large']");
            var searchButton = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "XPath", ".//*[@class='button button--primary']");
            IdentifyWebElementsPerformAction.PerformWebdriverAction(webDriver, searchBox, "Input", TestData[3]);
            _test.Log(Status.Info, "Inputting search query: " + TestData[3]);
            var locatorButton = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "CssSelector", "[aria-label='Search'][class^='button']");
            IdentifyWebElementsPerformAction.PerformWebdriverAction(webDriver, locatorButton, "Click", null);
            _test.Log(Status.Info, "Clicking on Search button");
            var searchResults = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "XPath", ".//*[@class='search__title']");
            _test.Log(Status.Info, "Verifying the search results");
            Assert.That(searchResults.Text.ToLowerInvariant().Contains(TestData[3].ToLowerInvariant()), Is.True);
            var searchQuery = TestData[3].ToLowerInvariant().Split(" ");
            Assert.That(webDriver.Url.ToLowerInvariant().EndsWith(string.Join("+", searchQuery)), Is.True);
            _test.Pass("<div style='color:green; font-weight:bold'> Search results are displayed successfully for query: " + TestData[3] + "</div>", 
                AttachScreenShot(null));
        }
    }
}
