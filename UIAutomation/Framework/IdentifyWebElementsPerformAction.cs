using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

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
                        return dynamicElement;
                    case "class":
                        dynamicElement = dWait.Until(ExpectedConditions.ElementExists(By.ClassName(strIdentifier)));
                        return dynamicElement;
                    case "name":
                        dynamicElement = dWait.Until(ExpectedConditions.ElementExists(By.Name(strIdentifier)));
                        return dynamicElement;
                    case "xpath":
                        dynamicElement = dWait.Until(ExpectedConditions.ElementExists(By.XPath(strIdentifier)));
                        return dynamicElement;
                    case "cssselector":
                        dynamicElement = dWait.Until(ExpectedConditions.ElementExists(By.CssSelector(strIdentifier)));
                        return dynamicElement;
                    case "linktext":
                        dynamicElement = dWait.Until(ExpectedConditions.ElementExists(By.LinkText(strIdentifier)));
                        return dynamicElement;
                    case "partiallinktext":
                        dynamicElement = dWait.Until(ExpectedConditions.ElementExists(By.PartialLinkText(strIdentifier)));
                        return dynamicElement;
                    case "tagname":
                        dynamicElement = dWait.Until(ExpectedConditions.ElementExists(By.TagName(strIdentifier)));
                        return dynamicElement;
                    default:
                        return null;
                }
            }
            catch(Exception e)
            {
                string strTemp = strIdentifier + " - failed \\n";
                Assert.Fail(strTemp + e);
                return null;
            }
        }

        public static void PerformWebdriverAction(IWebDriver driver, IWebElement objWebElement, string strAction, string strData)
        {
            bool boolExecStep = false;
            string strExecption = "";
            try
            {
                switch(strAction.ToLower())
                {
                    case "input":
                        objWebElement.Click();
                        objWebElement.Clear();
                        objWebElement.SendKeys(strData);
                        boolExecStep = true;
                        break;

                    case "select":
                        objWebElement.Click();
                        SelectElement selector = new SelectElement(objWebElement);
                        selector.SelectByText(strData);
                        boolExecStep = true;
                        break;

                    case "click":
                        objWebElement.Click();
                        boolExecStep = true;
                        break;

                    case "focus":
                        Actions actFocus = new Actions(driver);
                        actFocus.MoveToElement(objWebElement);
                        boolExecStep = true;
                        break;

                    default:
                        boolExecStep = false;
                        break;
                }
            }
            catch(Exception e)
            {
                strExecption = e.ToString();
                boolExecStep = false;
                driver.Close();
                driver.Dispose();
            }
        }
    }
}
