using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace UIAutomation.Framework
{
    internal class DriverHelper
    {
        public IWebDriver InitialiseDriver(IWebDriver driver)
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
                    var ieOptions = new InternetExplorerOptions();
                    ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    ieOptions.RequireWindowFocus = true;
                    ieOptions.EnsureCleanSession = true;
                    ieOptions.IgnoreZoomLevel = true;
                    ieOptions.AcceptInsecureCertificates = true;
                    driver = new InternetExplorerDriver(ieOptions);
                    break;

                case "headless":
                    chromeOpt = new ChromeOptions();
                    chromeOpt.AddArguments("start-maximized", "--disable-gpu", "--no-sandbox", "window-size=1280,800", "--headless=new");
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
            driver.Manage().Window.Maximize();
            driver.Manage().Window.Size = new System.Drawing.Size(1280, 800);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(int.Parse(WebDriverConfigurationSettings.ConfigSetting
                    (TestConstants.ConfigTypes.WebDriverConfiguration, TestConstants.ConfigTypesKey.GlobalTimeout)));
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(int.Parse(WebDriverConfigurationSettings.ConfigSetting
                    (TestConstants.ConfigTypes.WebDriverConfiguration, TestConstants.ConfigTypesKey.ObjectSyncTimeOut)));
            return driver;
        }
    }
}
