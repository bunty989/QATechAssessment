using OpenQA.Selenium;
using System;
using System.Threading;

namespace UIAutomation.Framework
{
    public static class WebPageStateChecker
    {
        public static void PollForReadyState(IWebDriver driver, int intTimeout)
        {
            var timeOnFunctionCall = DateTime.Now;
            string strState;
            while((DateTime.Now - timeOnFunctionCall).TotalSeconds < intTimeout)
            {
                var js = driver as IJavaScriptExecutor;
                try
                {
                    strState = (string)js.ExecuteScript("return document.readystate");
                    if (strState == "complete")
                        break;
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                catch (Exception)
                {
                    return;
                }
            }
        }
    }
}
