using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UIAutomation.Framework;


namespace UIAutomation
{
    [TestFixture]
    public abstract class TestBase
    {
        protected static IWebDriver webDriver;

        [OneTimeSetUp]
        public virtual void Setup()
        {
            var Webdriver = new DriverHelper();
            webDriver = Webdriver.InitialiseDriver(webDriver);
        }

        [OneTimeTearDown]
        public virtual void TearDownFixture()
        {
            webDriver.Quit();
            webDriver.Dispose();
        }
    }
}