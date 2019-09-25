using OpenQA.Selenium;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace UIAutomation.Framework
{
    internal class DriverHelper
    {
        public IWebDriver InitialiseDriver(IWebDriver driver)
        {
            var folderName = AppDomain.CurrentDomain.BaseDirectory + @"\Drivers";
            string webDriverPath = Path.GetFullPath(folderName);

            switch(WebDriverConfigurationSettings.ConfigSetting(TestConstants.ConfigTypes.WebDriverConfiguration,TestConstants.ConfigTypesKey.Browser).ToLower())
            {
                case "chrome":
                    var chromeOpt = new ChromeOptions();
                    chromeOpt.AddArguments("--test-type", "--disable-extensions", "no-sandbox");
                    chromeOpt.AddAdditionalCapability("useAutomationExtension", false);
                    chromeOpt.Proxy = null;
                    driver = new ChromeDriver(chromeOpt);
                    break;

                case "ie":
                    var ieOptions = new InternetExplorerOptions();
                    ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    ieOptions.RequireWindowFocus = true;
                    ieOptions.EnsureCleanSession = true;
                    ieOptions.IgnoreZoomLevel = true;
                    ieOptions.AddAdditionalCapability(CapabilityType.AcceptSslCertificates,true);
                    driver = new InternetExplorerDriver(webDriverPath, ieOptions);
                    break;

                case "headless":
                    chromeOpt = new ChromeOptions();
                    chromeOpt.AddArguments("--headless");
                    driver = new ChromeDriver(chromeOpt);
                    break;

                default:
                    chromeOpt = new ChromeOptions();
                    chromeOpt.AddArguments("--test-type", "--disable-extensions", "no-sandbox");
                    chromeOpt.AddAdditionalCapability("useAutomationExtension", false);
                    chromeOpt.Proxy = null;
                    driver = new ChromeDriver(chromeOpt);
                    break;
            }
            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}
