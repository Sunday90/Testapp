using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using System.Configuration;
using Testapp.Utilities;
using OpenQA.Selenium.Support.UI;

namespace Testapp.Pageclasses
{
    /// <summary>
    /// Page with groups of technical reports
    /// </summary>
    public class TechnicalReportsByGroupPage : BasePage
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();      //Nlog variable

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="driver">WebDriver variable</param>
        public TechnicalReportsByGroupPage(IWebDriver driver)
        {
            this.driver = driver;
            this.URL = base.baseURL + "/TR/tr-groups-all";
            this.Title = "All Documents (sorted by group) - W3C";
        }


        By selectGroupKey = By.Id("groupkey");
        By showViewButton = By.Id("sbm-filter");
        By logoLink = By.XPath(".//h1[@class='logo']//a");

        By groups = By.XPath(".//div[@id='w3c_content_body']//h3[contains(@id,tr)]");

       private static List<IWebElement> groupsEls;      //List of groups elements


        /// <summary>
        /// Dictionary with two types of values. First is String - it`s a key to get access to info about any type of subgroups.
        /// Second value is object of internal class "ReportsGroupsType". It incapsulates information about each subgroup. 
        /// </summary>
        private Dictionary<String, ReportsGroupsType> selectDocumentTypesValues = new Dictionary<String, ReportsGroupsType>
                    {
                         { "All", new ReportsGroupsType("all", "All Documents (sorted by group)") },
                         { "Standards Only", new ReportsGroupsType("stds", "Standards Only (sorted by group)") },
                         { "Drafts Only", new ReportsGroupsType("drafts", "Drafts Only (sorted by group)") },
                         { "Nightly Drafts Only", new ReportsGroupsType("nightly", "Nightly Drafts (sorted by group)") },
                         { "Review Opportunities", new ReportsGroupsType("review", "Documents in Review Only (sorted by group)") }
                    };



        /// <summary>
        /// Internal class, it stores info about subgroups to more comfortable work with them
        /// </summary>
        private class ReportsGroupsType
        {
            public String SelectOptionValue;
            public String HOneText;


            /// <summary>
            /// Public constructor
            /// </summary>
            /// <param name="selectOptionValue">Value of option</param>
            /// <param name="hOneText">Text in H1 on the page</param>
            public ReportsGroupsType(String selectOptionValue, String hOneText)
            {
                this.SelectOptionValue = selectOptionValue;
                this.HOneText = hOneText;
            }
        }

        
        /// <summary>
        /// Select documents group in dropdown menu
        /// </summary>
        /// <param name="groupName">Name of group to select</param>
        public void selectDocumentTypes(String groupName)
        {
        SelectElement selectGroupKeyEl = new SelectElement(Search.FindEl(driver, selectGroupKey));
        ReportsGroupsType type = selectDocumentTypesValues[groupName];
        selectGroupKeyEl.SelectByValue(type.SelectOptionValue);

        Search.FindEl(driver, showViewButton).Click();
        }

        
        /// <summary>
        /// Get all groups to List
        /// </summary>
        private void findGroups ()
        {
             groupsEls = Search.FindEls(driver, groups);
        }

        /// <summary>
        /// Get groups in List count
        /// </summary>
        /// <returns>Return Count of groups</returns>
        public int getGroupsCount ()
        {
            findGroups();
            return groupsEls.Count;            
        }

        /// <summary>
        /// Get name of group in List by number
        /// </summary>
        /// <param name="number">Number of group in List</param>
        /// <returns></returns>
        public String getGroupNameByNumber (int number)
        {
            findGroups();
            int groupsCount = getGroupsCount();

            try
            {
                return groupsEls[number - 1].Text;
            }

            catch (IndexOutOfRangeException e)
            {
                String message = "Groups count is " + groupsCount + ", but requested a " + number.ToString() + " group in array!";
                logger.Error(message);
                throw new Exception(message);
            }

            catch (Exception e)
            {
                logger.Error(e.Message);
                throw new Exception();
            }
        }


        /// <summary>
        /// Check that subgroup is selected successfully
        /// </summary>
        /// <param name="groupName">Name of group to check</param>
        /// <returns>True - if group is selected successfully, else - False</returns>
        public bool СheckSubGroupSelected (String groupName)
        {
            ReportsGroupsType type = selectDocumentTypesValues[groupName];
            String HOneText = type.HOneText;

            return Search.checkElementExist(driver, By.XPath(".//h1[text()='" + HOneText + "']"));
        }


        /// <summary>
        /// Open main page
        /// </summary>
        public void goToMainPage ()
        {
            Search.FindEl(driver, logoLink).Click();
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
