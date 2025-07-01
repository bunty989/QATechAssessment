using NUnit.Framework;
using OpenQA.Selenium;
using System;
using UIAutomation.Framework;


namespace UIAutomation
{
    [TestFixture]
    public abstract class TestBase
    {
        [ThreadStatic]
        protected static IWebDriver webDriver;

        [SetUp]
        public virtual void Setup()
        {
            var Webdriver = new DriverHelper();
            webDriver = Webdriver.InitialiseDriver(webDriver);
        }

        [SetUp]
        public virtual void OpenApp()
        {            
            webDriver.Navigate().GoToUrl(WebDriverConfigurationSettings.ConfigSetting(TestConstants.ConfigTypes.WebDriverConfiguration, TestConstants.ConfigTypesKey.Url));
            WebPageStateChecker.PollForReadyState(webDriver, int.Parse(WebDriverConfigurationSettings.ConfigSetting(TestConstants.ConfigTypes.WebDriverConfiguration, TestConstants.ConfigTypesKey.GlobalTimeout)));
        }

        [TearDown]
        public virtual void TearDownFixture()
        {
            webDriver.Quit();
            webDriver.Dispose();
        }
    }
}