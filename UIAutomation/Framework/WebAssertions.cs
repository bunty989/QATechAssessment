using OpenQA.Selenium;
using System;

namespace UIAutomation.Framework
{
    public static class WebAssertions
    {
        public static bool IsWebElementDisplayed(IWebElement webElement)
        {
            try
            {
                if (webElement.Displayed)
                    return true;
                else 
                    return false;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
