using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using UIAutomation.Framework;

namespace UIAutomation
{
    [SetUpFixture]
    public class ExtentReportFixture
    {
        public static ExtentReports Extent;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            var formattedDateTime = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            var reportFilePath = Path.Combine(Directory.GetParent(@"../../../../")?.FullName, "Logs", formattedDateTime);
            try
            {
                Directory.CreateDirectory(reportFilePath);
            }
            catch (Exception ex)
            {
                Log.Error("Couldn't create the directory in the file path {0} due to {1}",
                    reportFilePath, ex.Message);
            }
            var levelSwitch = new LoggingLevelSwitch(LogEventLevel.Verbose);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.File(reportFilePath + @"\Log",
                    outputTemplate: "{Timestamp: yyyy-MM-dd HH:mm:ss.fff} | {Level:u3} | {Message} | {NewLine}",
                    rollingInterval: RollingInterval.Day).CreateLogger();

            var htmlReport = new ExtentSparkReporter(Path.Combine(reportFilePath, "ExtentReport.html"));
            htmlReport.LoadXMLConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExtentConfig.xml"));
            Extent = new ExtentReports();
            Extent.AttachReporter(htmlReport);

            var sysInfo = new Dictionary<string, string>
            {
                { "Host Name", Environment.MachineName },
                { "Domain", Environment.UserDomainName },
                { "Username", Environment.UserName },
                {"Browser Name", WebDriverConfigurationSettings.ConfigSetting(TestConstants.ConfigTypes.WebDriverConfiguration, TestConstants.ConfigTypesKey.Browser)},
            };
            foreach (var (key, value) in sysInfo)
                Extent.AddSystemInfo(key, value);
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            Extent?.Flush();
            Log.CloseAndFlush();
        }
    }
}