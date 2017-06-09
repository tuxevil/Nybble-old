using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace NybbleMembership.Core
{
    public class Configuration
    {
        public static string SiteCode
        {
            get
            {
                if (ConfigurationManager.AppSettings["Membership_SiteCode"] == null)
                    throw new NullReferenceException("Membership_SiteCode AppSetting is undefined.");

                return ConfigurationManager.AppSettings["Membership_SiteCode"];
            }
        }


        public static string MailTemplatePath
        {
            get { return "/res/mail/"; }
        }
    }
}
