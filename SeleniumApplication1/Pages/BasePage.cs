using NLog;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testapp.Utilities;

namespace Testapp.Pageclasses
{
    /// <summary>
    /// Abstract class of base page
    /// </summary>
    public abstract class BasePage
    {

        public String URL;      //Variable with URL
        public String Title     //Variable of page title
        protected String baseURL;   //Base test`s pages URL
        private static Logger logger = LogManager.GetCurrentClassLogger();      //Nlog variable
        public IWebDriver driver;      //WebDriver variable

        /// <summary>
        /// Open currect page
        /// </summary>
        public void Open()
        {
            if (URL != null)
            {
                driver.Navigate().GoToUrl(this.URL);
            }
        }

        /// <summary>
        /// Abstract method to check that page is loaded correctly. Must be overrided in child classes
        /// </summary>
        /// <returns>True - if page is loaded, else - False</returns>
        public abstract bool CheckPageIsLoaded();
    }
}
