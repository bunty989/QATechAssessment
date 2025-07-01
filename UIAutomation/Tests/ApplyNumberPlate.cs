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
            Assert.That(WebAssertions.IsWebElementDisplayed(searchBox), Is.True);
        }

        [Test]
        [Retry(3)]
        public void VerifySearchButton()
        {
            var searchButton = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "XPath", ".//*[@class='button button--primary']");
            Assert.That(WebAssertions.IsWebElementDisplayed(searchButton), Is.True);
        }

        [Test]
        [Retry(3)]
        public void SearchServices()
        {
            var searchBox = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "XPath", ".//*[@class='form__text form--large']");
            var searchButton = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "XPath", ".//*[@class='button button--primary']");
            IdentifyWebElementsPerformAction.PerformWebdriverAction(webDriver, searchBox, "Input", TestData[3]);
            var locatorButton = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "CssSelector", "[aria-label='Search'][class^='button']");
            IdentifyWebElementsPerformAction.PerformWebdriverAction(webDriver, locatorButton, "Click", null);
            var searchResults = IdentifyWebElementsPerformAction.InitialiseDynamicWebElement(webDriver, "XPath", ".//*[@class='search__title']");
            Assert.That(searchResults.Text.ToLowerInvariant().Contains(TestData[3].ToLowerInvariant()), Is.True);
            var searchQuery = TestData[3].ToLowerInvariant().Split(" ");
            Assert.That(webDriver.Url.ToLowerInvariant().EndsWith(string.Join("+", searchQuery)), Is.True);
        }
    }
}
