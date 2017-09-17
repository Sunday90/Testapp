using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testapp.Pageclasses;
using Testapp.Utilities;
using OpenQA.Selenium;
using NLog;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Testapp.Tests
{
    /// <summary>
    /// Class with test case
    /// </summary>
    [TestFixture]
    class WorkWithTestAppTest
    {

        public static void Main(String[] args)
        {

        }

        private IWebDriver driver;      //WebDriver variable
        private static Logger logger = LogManager.GetCurrentClassLogger();       //Nlog variable

        MainPage mp;                    //Main page variable
        TechnicalReportsByGroupPage trbgp;           //Technical reports by group page variable
        DuckDuckGoSearchPage ddgsp;     //DuckDuckGo page variable


        /// <summary>
        /// Method runs before test suite
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {

            WorkWithPreferencies.LoadPreferenciesFromAppConfig();

            String webDriverPath = WorkWithPreferencies.webDriverPath;

            switch (WorkWithPreferencies.browserType)
            {
                case "chrome": driver = new ChromeDriver(webDriverPath);
                    break;
                case "firefox": FirefoxBinary binary = new FirefoxBinary(webDriverPath);
                    driver = new FirefoxDriver();
                    break;
                default: driver = new ChromeDriver(webDriverPath);
                    break;
            }
            

            mp = new MainPage(driver);
            trbgp = new TechnicalReportsByGroupPage(driver);
            ddgsp = new DuckDuckGoSearchPage(driver);

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }



        /// <summary>
        /// Check that DuckDuckGo search query starts from "site:w3.org"
        /// </summary>
        [Test]
        public void checkSearchQuery()
        {

            mp.Open();
            Assert.True(mp.CheckPageIsLoaded(), "Main page didn`t load!");

            mp.openTechReportsByGroup();
            Assert.True(trbgp.CheckPageIsLoaded(), "Technical reports by group page didn`t load!");

            string groupName = "Standards Only";
            trbgp.selectDocumentTypes(groupName);
            Assert.True(trbgp.СheckSubGroupSelected(groupName), "Subgroup " + groupName + " isn`t selected!");

            int groupsCount = trbgp.getGroupsCount();
            Assert.True(groupsCount > 0, "No one item is found in subgroup " + groupName + "!");

            Assert.True(groupsCount != 50, "Count of items in subgroup " + groupName + " is 50! Test failed by test requirements!");

            logger.Info(groupsCount.ToString());

            int itemsInSubGroupCount = trbgp.getGroupsCount();
            String groupNameForSearch = (groupsCount > 50) ? trbgp.getGroupNameByNumber(0) : trbgp.getGroupNameByNumber(itemsInSubGroupCount);

            Assert.True(groupNameForSearch.Trim().Length != 0, "Variable with group name is empty!");

            trbgp.goToMainPage();
            Assert.True(mp.CheckPageIsLoaded(), "Main page didn`t loaded!");

            mp.search(groupNameForSearch.Split(' ')[0]);
            Assert.True(ddgsp.CheckPageIsLoaded(), "DuckDuckGo page didn`t loaded!");

            string searchQuerytext = ddgsp.getSearchQueryText();
            logger.Info(searchQuerytext);

            Assert.True(searchQuerytext.StartsWith("site:w3.org"), "Duck duck go search query doesn`t start with \"site:w3.org\" string!");


        }


        /// <summary>
        /// Close driver
        /// </summary>
        [OneTimeTearDown]
        public void endTest()
        {
            driver.Quit();
        }
    }
}
