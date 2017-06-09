using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Threading;
using ProjectBase.Data;
using ProjectBase.Utils;

namespace NybbleMembership.Common
{
    public class Config
    {
        // Constantes
        private const string CONST_MEMBERSHIP = "membership";
        private const string CONST_MEMBERSHIP_SITECODE = "Membership_SiteCode";

        // Constantes para QueryString
        public const string QS_PRODUCT = "prd";
        public const string QS_SELECTION = "s";

        // Constantes para VIEWSTATE
        public const string VW_PRODUCT = "prd";

        // Constantes para SESSION
        public const string SE_PRODUCT = "prd";

        public static string FactoryConfigPath
        {
            get
            {
                OpenSessionInViewSection openSessionInViewSection = ConfigurationManager.GetSection("nhibernateSettings") as OpenSessionInViewSection;
                Check.Ensure(openSessionInViewSection != null, "The nhibernateSettings section was not found with ConfigurationManager.");
                Check.Ensure(openSessionInViewSection.SessionFactories[CONST_MEMBERSHIP] != null, "No session factory defined for " + CONST_MEMBERSHIP);
                return openSessionInViewSection.SessionFactories[CONST_MEMBERSHIP].FactoryConfigPath;
            }
        }

        public static string SiteCode
        {
            get
            {
                string appSetting = ConfigurationManager.AppSettings[CONST_MEMBERSHIP_SITECODE];
                Check.Ensure(appSetting != null, "The " + CONST_MEMBERSHIP_SITECODE + " application setting was not found in the configuration file.");
                return appSetting;
            }
        }
    }
}
