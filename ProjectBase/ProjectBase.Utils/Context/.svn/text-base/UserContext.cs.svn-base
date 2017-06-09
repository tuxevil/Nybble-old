using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;

namespace ProjectBase.Utils
{
    public class UserContext : IUserContext
    {
        public UserContext()
        {
            if (HttpContext.Current == null)
                return;

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return;

            if (HttpContext.Current.User.Identity.AuthenticationType == "Forms")
            {
                MembershipUser mu = System.Web.Security.Membership.GetUser();
                if (mu != null)
                {
                    identifier = mu.ProviderUserKey;
                    userName = mu.UserName;
                    lastAccess = mu.LastActivityDate;
                    return;
                }
            }

            userName = HttpContext.Current.User.Identity.Name;
        }

        public UserContext(object identifier, string userName, DateTime lastAccess)
        {
            this.identifier = identifier;
            this.userName = userName;
            this.lastAccess = lastAccess;
        }

        #region Properties

        private object identifier;
        public object Identifier
        {
            get { return identifier; }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
        }

        private DateTime lastAccess;
        public DateTime LastAccess
        {
            get { return lastAccess; }
        }

        #endregion
    }
}
