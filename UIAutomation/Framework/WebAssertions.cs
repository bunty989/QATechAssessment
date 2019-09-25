using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

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
