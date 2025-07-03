using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Serilog;

namespace UIAutomation.Framework
{
    public static class IdentifyWebElementsPerformAction
    {
        public static IWebElement InitialiseDynamicWebElement(IWebDriver driver, string strIdentifierType, string strIdentifier)
        {
            var dWait = new WebDriverWait(driver,TimeSpan.FromSeconds(int.Parse(WebDriverConfigurationSettings.ConfigSetting(TestConstants.ConfigTypes.WebDriverConfiguration,TestConstants.ConfigTypesKey.ObjectIdentificationTimeOut))));
            try
            {
                IWebElement dynamicElement;
                switch(strIdentifierType.ToLower())
                {
                    case "id":
                        dynamicElement = dWait.Until(ExpectedConditions.ElementExists(By.Id(strIdentifier)));
                        var webElements = new List<IWebElement>(driver.FindElements(By.Id(strIdentifier)));
                        if (webElements.Count > 1)
                        {
                            foreach (var webE in webElements)
                            {
                                if (webE.Displayed)
                                {
                                    return webE;
                                }
                            }
                        }
                        break;
                    case "class":
                        dynamicElement = dWait.Until(ExpectedConditions.ElementExists(By.ClassName(strIdentifier)));
                        break;
                    case "name":
                        dynamicElement = dWait.Until(ExpectedConditions.ElementExists(By.Name(strIdentifier)));
                        break;
                    case "xpath":
                        dynamicElement = dWait.Until(ExpectedConditions.ElementExists(By.XPath(strIdentifier)));
                        break;
                    case "cssselector":
                        dynamicElement = dWait.Until(ExpectedConditions.ElementExists(By.CssSelector(strIdentifier)));
                        break;
                    case "linktext":
                        dynamicElement = dWait.Until(ExpectedConditions.ElementExists(By.LinkText(strIdentifier)));
                        break;
                    case "partiallinktext":
                        dynamicElement = dWait.Until(ExpectedConditions.ElementExists(By.PartialLinkText(strIdentifier)));
                        break;
                    case "tagname":
                        dynamicElement = dWait.Until(ExpectedConditions.ElementExists(By.TagName(strIdentifier)));
                        break;
                    default:
                        return null;
                }
                var webElementName = dynamicElement?.GetAttribute("name");
                var webElementValue = dynamicElement?.GetAttribute("value");
                var elementDisplayedText = string.IsNullOrEmpty(webElementValue) ? webElementName : webElementValue;
                Log.Debug("WebElement {0} is identified successfully", elementDisplayedText);
                return dynamicElement;
            }
            catch(Exception e)
            {
                var strTemp = strIdentifier + " - failed \\n";
                Log.Error(strTemp + Environment.NewLine + e.Message);
                Assert.Fail(strTemp + e);
                return null;
            }
        }

        public static void PerformWebdriverAction(IWebDriver driver, IWebElement objWebElement, string strAction, string strData)
        {
            bool boolExecStep = false;
            string strException = "";
            try
            {
                switch(strAction.ToLower())
                {
                    case "input":
                        objWebElement.Click();
                        objWebElement.Clear();
                        objWebElement.SendKeys(strData);
                        boolExecStep = true;
                        Log.Debug("Input action performed on element: with data: {0}", strData);
                        break;

                    case "select":
                        objWebElement.Click();
                        SelectElement selector = new SelectElement(objWebElement);
                        selector.SelectByText(strData);
                        boolExecStep = true;
                        Log.Debug("Select action performed on element: with data: {0}", strData);
                        break;

                    case "click":
                        objWebElement.Click();
                        boolExecStep = true;
                        Log.Debug("Click action performed on element: ");
                        break;

                    case "focus":
                        Actions actFocus = new Actions(driver);
                        actFocus.MoveToElement(objWebElement);
                        boolExecStep = true;
                        Log.Debug("Focus action performed on element: ");
                        break;

                    default:
                        boolExecStep = false;
                        break;
                }
            }
            catch(Exception e)
            {
                strException = e.ToString();
                boolExecStep = false;
                Log.Error("Unable to perform Action on WebElement {0} due to {1}",
                                objWebElement.GetAttribute("name"),
                                strException);
                driver.Close();
                driver.Dispose();
                Assert.Fail($"Unable to perform Action on WebElement {objWebElement.GetAttribute("name")} due to {strException}");
            }
        }
    }
}
