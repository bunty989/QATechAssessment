using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using UIAutomation.Framework;
using Log = Serilog.Log;

namespace UIAutomation
{
    [TestFixture]
    public abstract class TestBase
    {   
        [ThreadStatic]
        protected static IWebDriver webDriver;
        [ThreadStatic]
        protected static ExtentReports Extent;
        [ThreadStatic]
        protected static ExtentTest _test;


        [SetUp]
        public virtual void Setup()
        {
            webDriver = DriverHelper.InitialiseDriver();
            Log.Information("Initialised WebDriver: {0}", webDriver.GetType().Name);
            Extent = ExtentReportFixture.Extent;
            _test = Extent.CreateTest(TestContext.CurrentContext.Test.Name)
                .AssignCategory(GetType().Name)
                .Info("Test Started");
        }

        [SetUp]
        public virtual void OpenApp()
        {
            webDriver.Navigate().GoToUrl(WebDriverConfigurationSettings.ConfigSetting(TestConstants.ConfigTypes.WebDriverConfiguration, TestConstants.ConfigTypesKey.Url));
            WebPageStateChecker.PollForReadyState(webDriver, int.Parse(WebDriverConfigurationSettings.ConfigSetting(TestConstants.ConfigTypes.WebDriverConfiguration, TestConstants.ConfigTypesKey.GlobalTimeout)));
            Log.Information("Navigated to {0} successfully", webDriver.Url);
        }

        [TearDown]
        public virtual void TearDownFixture()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = "" + TestContext.CurrentContext.Result.StackTrace + "";
            var errorMessage = TestContext.CurrentContext.Result.Message;
            if (status == TestStatus.Failed)
            {
                _test.Fail("<div style='color:red; font-weight :bold'> " + errorMessage + "<br> Stacktrace: " + stacktrace + " </div>", 
                    AttachScreenShot("Failure Screenshot"));
                Log.Error("Test failed with error: {0}", errorMessage);
            }
            else
            {
                Log.Information("Test completed successfully.");
            }
            webDriver.Quit();
            webDriver.Dispose();
        }

        public static Media AttachScreenShot(string name)
        {
            var base64 = TakesScreenShot();
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64, name).Build();
        }

        private static string TakesScreenShot()
        {
            return (webDriver as ITakesScreenshot)?.GetScreenshot().AsBase64EncodedString;
        }
    }
}