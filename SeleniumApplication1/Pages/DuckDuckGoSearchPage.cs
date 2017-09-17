using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using System.Configuration;
using Testapp.Utilities;

namespace Testapp.Pageclasses
{
    /// <summary>
    /// DuckDuckGo page
    /// </summary>
    public class DuckDuckGoSearchPage : BasePage
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();      //Nlog variable

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="driver">WebDriver variable</param>
        public DuckDuckGoSearchPage(IWebDriver driver)
        {
            this.driver = driver;
            this.URL = "https://duckduckgo.com/";
            this.Title = "DuckDuckGo";
        }

        
        By searchEditBox = By.Id("search_form_input");      


        /// <summary>
        /// Get text from search editbox field
        /// </summary>
        /// <returns>Text from field</returns>
        public string getSearchQueryText()
        {
            return Search.FindEl(driver, searchEditBox).GetAttribute("value");
        }


        /// <summary>
        /// Overrided method to checking that page is loaded 
        /// </summary>
        /// <returns>True - if page is loaded, else - False</returns>
        public override bool CheckPageIsLoaded()
        {
            return (driver.Title.Contains(Title));
        }

    }
}
