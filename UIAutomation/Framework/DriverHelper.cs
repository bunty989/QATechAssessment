using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using Serilog;

namespace UIAutomation.Framework
{
    internal class DriverHelper
    {
        [ThreadStatic]
        private static IWebDriver driver;

        public static IWebDriver InitialiseDriver()
        {
            switch(WebDriverConfigurationSettings.ConfigSetting(TestConstants.ConfigTypes.WebDriverConfiguration,TestConstants.ConfigTypesKey.Browser).ToLower())
            {
                case "chrome":
                    var chromeOpt = new ChromeOptions();
                    chromeOpt.AddArguments("start-maximized", "--disable-gpu", "--no-sandbox");
                    chromeOpt.PageLoadStrategy = PageLoadStrategy.Normal;
                    chromeOpt.Proxy = null;
                    driver = new ChromeDriver(chromeOpt);
                    break;

                case "ie":
                    var ieOptions = new InternetExplorerOptions
                    {
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                        RequireWindowFocus = true,
                        EnsureCleanSession = true,
                        IgnoreZoomLevel = true,
                        AcceptInsecureCertificates = true
                    };
                    driver = new InternetExplorerDriver(ieOptions);
                    break;

                case "headless":
                    chromeOpt = new ChromeOptions();
                    chromeOpt.AddArguments("start-maximized", "--disable-gpu", "no-sandbox", "window-size=1280,800", "--headless=new");
                    chromeOpt.PageLoadStrategy = PageLoadStrategy.Normal;
                    driver = new ChromeDriver(chromeOpt);
                    break;

                case "firefox":
                    var ffOpt = new FirefoxOptions
                    {
                        AcceptInsecureCertificates = true,
                        PageLoadStrategy = PageLoadStrategy.Normal
                    };
                    ffOpt.AddArguments("-headless", "--width=1280", "--height=800");
                    driver = new FirefoxDriver(ffOpt);
                    break;

                case "edge":
                    var edgeOpt = new EdgeOptions
                    {
                        AcceptInsecureCertificates = true,
                        PageLoadStrategy = PageLoadStrategy.Normal
                    };
                    edgeOpt.AddArguments("start-maximized", "--disable-gpu", "--no-sandbox", "--headless=new");
                    edgeOpt.Proxy = null;
                    driver = new EdgeDriver(edgeOpt);
                    break;

                default:
                    chromeOpt = new ChromeOptions();
                    chromeOpt.AddArguments("start-maximized", "--disable-gpu", "--no-sandbox");
                    chromeOpt.Proxy = null;
                    chromeOpt.PageLoadStrategy = PageLoadStrategy.Normal;
                    driver = new ChromeDriver(chromeOpt);
                    break;
            }
            Log.Information("Started {0} WebDriver successfully", driver.GetType().Name);
            driver.Manage().Window.Maximize();
            driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(int.Parse(WebDriverConfigurationSettings.ConfigSetting
                    (TestConstants.ConfigTypes.WebDriverConfiguration, TestConstants.ConfigTypesKey.GlobalTimeout)));
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(int.Parse(WebDriverConfigurationSettings.ConfigSetting
                    (TestConstants.ConfigTypes.WebDriverConfiguration, TestConstants.ConfigTypesKey.ObjectSyncTimeOut)));
            return driver;
        }
    }
}
