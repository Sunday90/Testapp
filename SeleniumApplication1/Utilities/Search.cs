using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NLog;

namespace Testapp.Utilities
{
    /// <summary>
    /// Wrap searching
    /// </summary>
    public class Search
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();      //Nlog variable

        /// <summary>
        /// Find elements wrap
        /// </summary>
        /// <param name="driver">WebDriver variable</param>
        /// <param name="by">By variable</param>
        /// <returns>Collection of WebElements</returns>
        public static List<IWebElement> FindEls(IWebDriver driver, By by)
        {
            List<IWebElement> elements = null;
            elements = driver.FindElements(by).ToList<IWebElement>();
            return elements;

        }

        /// <summary>
        /// Fimd element wrap
        /// </summary>
        /// <param name="driver">WebDriver variable</param>
        /// <param name="by">By variable</param>
        /// <returns>WebElement</returns>
        public static IWebElement FindEl(IWebDriver driver, By by)
        {
            return FindEls(driver, by)[0];
        }

        /// <summary>
        /// Check is element exist
        /// </summary>
        /// <param name="driver">WebDriver variable</param>
        /// <param name="by">By variable</param>
        /// <returns>True - if element founded, else - False</returns>
        public static Boolean checkElementExist(IWebDriver driver, By by)
        {
            try
            {
                FindEl(driver, by);
                return true;
            }

            catch (Exception e)
            {
                logger.Error(e.Message);
                return false;
            }
        }


        /// <summary>
        /// Wait and find element
        /// </summary>
        /// <param name="driver">WebDriver variable</param>
        /// <param name="by">By variable</param>
        /// <returns>WebElement</returns>
        public static IWebElement waitAndFindElement(IWebDriver driver, By by)
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(3);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(500);

            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            IWebElement searchResult = fluentWait.Until(x => x.FindElement(by));
            return searchResult;
        }
    }
}