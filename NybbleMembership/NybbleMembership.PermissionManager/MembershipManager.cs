using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using NybbleMembership.Business;
using NybbleMembership.Core;
using NybbleMembership.Core.Domain;
using System.Web;
using ProjectBase.Utils.Cache;

namespace NybbleMembership
{
    #region MembershipHelperUser 

    /// <summary>
    /// Membership Helper User
    /// </summary>
    public class MembershipHelperUser
    {
        private Guid userId;
        private string userName;
        private bool isAdministrator;
        private string[] roles;
        private string email;
        private string firstName;
        private string lastName;

        public MembershipHelperUser() 
        {
            FormsIdentity fi = HttpContext.Current.User.Identity as FormsIdentity;
            if (fi != null && fi.Ticket.UserData != string.Empty)
            {
                //Parse User Data
                string[] fields = fi.Ticket.UserData.Split('|');
                userId = new Guid(fields[0]);
                isAdministrator = Convert.ToBoolean(fields[1]);
                if (!string.IsNullOrEmpty(fields[2]))
                    roles = fields[2].Split(',');
                firstName = fields[3];
                lastName = fields[4];
            }

            userName = fi.Ticket.Name;
        }

        public MembershipHelperUser(MembershipUser mu)
        {
            userId = (Guid)mu.ProviderUserKey;
            isAdministrator = MembershipManager.IsAdministrator(mu.UserName);

            IList<Rol> lst = MembershipManager.GetUserRoles(mu.UserName);
            if (lst.Count > 0) 
            {
                string roleNames = string.Empty;
                foreach (Rol r in lst)
                    roleNames += r.Name + ",";

                roleNames = roleNames.Remove(roleNames.Length - 1);
                roles = roleNames.Split(',');
            }
 
            userName = mu.UserName;
            email = mu.Email;
            
            UserProfile up = ControllerManager.UserProfile.GetById(userId);
            firstName = up.FirstName;
            lastName = up.LastName;
        }

        public Guid UserId
        {
            get { return userId; }
        }

        public bool IsAdministrator
        {
            get { return isAdministrator; }
        }

        public bool IsInRole(string userRole) 
        {
            foreach (string rol in roles)
                if (rol.ToLower() == userRole.ToLower())
                    return true;

            return false;
        }

        public string FirstName
        {
            get { return firstName; }
        }

        public string LastName
        {
            get { return lastName; }
        }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public string UserName
        {
            get { return userName; }
        }

        public string Email
        {
            get
            {
                // Obtain email if not there.
                if (string.IsNullOrEmpty(email))
                    email = Membership.GetUser(UserName).Email;

                return email;
            }
        }
    }

    #endregion

    #region MembershipHelper Class

    /// <summary>
    /// Helper to manage user state through application lifecycle
    /// </summary>
    public class MembershipHelper 
    {
        public static MembershipHelperUser GetUser()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
                return new MembershipHelperUser();
            return null;
        }

        public static MembershipHelperUser GetUser(object userId)
        { 
            string cachedKey = string.Format("UserById_{0}", userId);
            object result = CacheManager.GetCached(cachedKey);

            if (result == null)
            {
                MembershipUser mu = Membership.GetUser(userId);
                if (mu != null)
                {
                    result = new MembershipHelperUser(mu);
                    CacheManager.AddItem(cachedKey, result);
                }
                else
                    result = null;

            }
            return result as MembershipHelperUser;
        }

        public static MembershipHelperUser GetUser(string userName)
        {
            string cachedKey = string.Format("UserByName_{0}", userName);
            object result = CacheManager.GetCached(cachedKey);

            if (result == null)
            {
                MembershipUser mu = Membership.GetUser(userName);
                if (mu != null)
                {
                    result = new MembershipHelperUser(mu);
                    CacheManager.AddItem(cachedKey, result);
                }
                else
                    result = null;
            }
            return result as MembershipHelperUser;
        }

        public static void UpdateAuthenticationTicketForUser(string userName)
        {
            MembershipUser mu = Membership.GetUser(userName);
            if (mu == null)
                return;

            Guid userId = (Guid)mu.ProviderUserKey;
            UserProfile up = ControllerManager.UserProfile.GetById(userId);
            if (up == null)
                return;

            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(userName, true);

            string roles = GetUserRolJoined(userName);

            string userDataString =
                mu.ProviderUserKey + "|"
                + MembershipManager.IsAdministrator(userName) + "|"
                + roles+ "|" + up.FirstName + "|" + up.LastName;

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, userDataString);
            authCookie.Value = FormsAuthentication.Encrypt(newTicket);

            HttpContext.Current.Response.Cookies.Add(authCookie);
        }

        private static string GetUserRolJoined(string userName)
        {
            IList<Rol> lst = MembershipManager.GetUserRoles(userName);

            if (lst.Count > 0)
            {
                string roles = string.Empty;
                foreach (Rol r in lst)
                    roles += r.Name + ",";

                roles = roles.Remove(roles.Length - 1);
                return roles;
            }
            else
                return string.Empty;
        }
    }

    #endregion

    /// <summary>
    /// Manager to obtain information from a user in the Membership Database
    /// </summary>
    public class MembershipManager
    {
        public static MembershipHelperUser GetUser(object userId)
        {
            MembershipUser mu = Membership.GetUser(userId);
            if (mu != null)
                return new MembershipHelperUser(mu);
            return null;
        }

        public static MembershipHelperUser GetUser(string userName)
        {
            MembershipUser mu = Membership.GetUser(userName);
            if (mu != null)
                return new MembershipHelperUser(mu);
            return null;
        }

        public static bool IsAdministrator()
        {
            MembershipUser user = Membership.GetUser();
            if (user != null)
                return ControllerManager.Rol.IsAdministrator(user.ProviderUserKey, Configuration.SiteCode);
            else return false;
        }

        public static bool IsAdministrator(string userName)
        {
            MembershipUser user = Membership.GetUser(userName);
            if (user != null)
                return ControllerManager.Rol.IsAdministrator(user.ProviderUserKey, Configuration.SiteCode);
            else 
                return false;
        }


        public static IList<Rol> GetRoles()
        {
            return ControllerManager.Rol.List(Configuration.SiteCode);
        }

        public static IList<Rol> GetUserRoles(string userName)
        {
            MembershipUser user = Membership.GetUser(userName);
            if (user != null)
                return ControllerManager.Rol.GetByUser(user.ProviderUserKey, Configuration.SiteCode);
            else
                return null;
        }

        public static bool AddUserToRole(string userName, string roleName)
        {
            MembershipUser user = Membership.GetUser(userName);
            if (user != null)
                return ControllerManager.Rol.AddUserToRole(user.ProviderUserKey, roleName, Configuration.SiteCode);
            else
                return false;

        }

        public static bool RemoveUserFromRole(string userName, string roleName)
        {
            MembershipUser user = Membership.GetUser(userName);
            if (user != null)
                return ControllerManager.Rol.RemoveUserFromRole(user.ProviderUserKey, roleName, Configuration.SiteCode);
            else
                return false;
        }

        public static IList<UserMember> ListBySite()
        {
            return ControllerManager.UserMember.ListBySite(Configuration.SiteCode);
        }

        public static IList<UserMember> GetUsersByRole(string roleName)
        {
            return ControllerManager.UserMember.GetUsersByRole(roleName, Configuration.SiteCode);
        }

        public static IList<UserMember> GetAdministrators()
        {
            return ControllerManager.UserMember.GetAdministrators();
        }
    }
}
