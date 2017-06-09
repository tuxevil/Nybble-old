using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;

namespace ProjectBase.Utils.Membership
{
    public class BaseSqlMembershipProvider : SqlMembershipProvider
    {
        /// <summary>
        /// Overrides the ApplicationName to retrieve it from the QueryString if available.
        /// </summary>
        public override string ApplicationName
        {
            get
            {
                string appName = string.Empty;

                if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.QueryString["appName"] != null)
                    appName = (string)HttpContext.Current.Request.QueryString["appName"];

                if (!string.IsNullOrEmpty(appName))
                    return appName;
                else
                    return base.ApplicationName;
            }
        }
    }
}
