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
    /// Main page of site
    /// </summary>
    public class MainPage : BasePage
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();      //Nlog variable

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="driver">WebDriver variable</param>
        public MainPage (IWebDriver driver)
        {
            this.driver = driver;
            this.URL = base.baseURL;
            this.Title = "World Wide Web Consortium (W3C)";
        }


        By linkTechReportsByGroup = By.LinkText("By group");                                
        By searchEditBox = By.XPath(".//div[@id='search-form']/input[@name='q']");
        By searchButton = By.Id("search-submit");


        /// <summary>
        /// Click on techreports by group link
        /// </summary>
        public void openTechReportsByGroup ()
        {
            Search.FindEl(driver, linkTechReportsByGroup).Click();
        }

        /// <summary>
        /// Search on site with DuckDuckGo
        /// </summary>
        /// <param name="searchQuery"></param>
        public void search (string searchQuery)
        {
            Search.FindEl(driver, searchEditBox).SendKeys(searchQuery);
            Search.FindEl(driver, searchButton).Click();
        }


        /// <summary>
        /// Overrided method to checking that page is loaded 
        /// </summary>
        /// <returns>True - if page is loaded, else - False</returns>
        public override bool CheckPageIsLoaded()
        {
            return (driver.Title.Trim().Equals(Title));
        }
    }
}
