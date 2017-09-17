using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Testapp.Utilities
{

    /// <summary>
    /// Class with operation to load preferencies from TestCaseSetting.setting
    /// </summary>
    class WorkWithPreferencies
    {
        public static String browserType = "chrome";
        public static String baseURL = "";
        public static String webDriverPath = "";


        /// <summary>
        /// Load preferencies from TestCaseSetting.setting
        /// </summary>
        public static void LoadPreferenciesFromAppConfig()
        {
            browserType = TestCaseSettings.Default.browserType;
            baseURL = TestCaseSettings.Default.baseURL;
            webDriverPath = TestCaseSettings.Default.webDriverPath;
        }
    }
}
