using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using NybbleMembership.Business;
using NybbleMembership.Core;
using NybbleMembership.Core.Domain;
using ProjectBase.Utils.Cache;
using log4net;
using log4net.Core;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace NybbleMembership
{
    /// <summary>
    /// Manages the permission of the user for functionaliy on each site.
    /// </summary>
    /// <remarks>
    /// Requires the name a session factory called "membership" to work with the database and NHibernate connection.
    /// Also requires the site code being managed in an appSetting definition in configuration file called "Membership_SiteCode". 
    /// </remarks>
    public class PermissionManager
    {
        private const string ALLPERMISSIONS = "ALL";
        private static ILog logger = LogManager.GetLogger("PermissionManager");

        /// <summary>
        /// Validates the current object has permission to be executed
        /// </summary>
        /// <param name="o">current object</param>
        /// <returns>True if it has the corresponding permission</returns>
        public static bool Check(object o)
        {
            return Check(o, null);
        }

        /// <summary>
        /// Validates the current object has permission to be executed.
        /// If there is no permission setup for this object and user, return false to enforce security.
        /// </summary>
        /// <param name="o">current object</param>
        /// <param name="action">action to be validated</param>
        /// <returns>
        /// True if it has the corresponding permission.
        /// False if no valid permissions are set for this object.
        /// </returns>
        public static bool Check(object o, Enum action)
        {
            MembershipHelperUser mu = MembershipHelper.GetUser();
            if (mu == null)
                return false;

            // Check if there are permissions for this object cached for this user.
            string checkAction = ((action != null) ? action.ToString() : ALLPERMISSIONS);
            
            string path = string.Empty;
            if ((typeof(HtmlControl).IsInstanceOfType(o) || typeof(WebControl).IsInstanceOfType(o)) && (o as Control) != null)
            {
                if ((o as Control).Page != null)
                    path = (o as Control).Page.AppRelativeVirtualPath;
                else
                    path = (o as Control).AppRelativeTemplateSourceDirectory;
            }

            if (logger.IsDebugEnabled) logger.DebugFormat("Checking cache permission for: User:{0} Action:{1} Object:{2} Type:{3} Path:{4}", mu.UserId, checkAction, o, o.GetType().ToString(), path);
            string cachedKey = string.Format("PERM_{0}_{1}_{2}_{3}_{4}", mu.UserId, o, checkAction, o.GetType().ToString(), path);
            
            object result = CacheManager.GetCached(cachedKey);
            if (result == null)
            {
                if (logger.IsDebugEnabled) logger.Debug("Cache not found.");

                // Obtain the list of permissions of the logged user
                List<Permission> lst = (ControllerManager.Permission.ListForCurrentUserAndSite(mu.UserId, action, Configuration.SiteCode) as List<Permission>);
                lst.AddRange(ControllerManager.Permission.ListPermisionsByUser(mu.UserId, Configuration.SiteCode));

                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Reviewing list of possible permissions.");
                    foreach (Permission p in lst)
                        logger.DebugFormat("Permission found: {0}", p.ToString());
                    logger.Debug("End reviewing list of possible permissions.");
                }
                // Review which permission can be validated on the current object and validate each of them
                bool isChecked = false;
                foreach (Permission p in lst)
                    if (p.CanCheck(o) && p.Check(o, action))
                        isChecked = true;

                CacheManager.AddItem(cachedKey, isChecked);

                if (logger.IsDebugEnabled) 
                    logger.DebugFormat("Result: {0}", isChecked);

                return isChecked;
            }
            else
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("Cache found.");
                    logger.DebugFormat("Result: {0}", (bool)result);
                }

                return (bool)result;
            }
        }

        /// <summary>
        /// Validates if the object has permissions.
        /// Returns an exception when the object has no permissions.
        /// </summary>
        /// <param name="o">current object</param>
        /// <exception cref="NotValidPermissionException" />
        public static void Validate(object o)
        {
            if (!Check(o))
                throw new NotValidPermissionException();
        }

        /// <summary>
        /// Allows to add a permission to a user directly
        /// </summary>
        /// <param name="p"></param>
        public static void AddEntityPermision(Type classType, string identifier, string userName)
        {
            if (logger.IsDebugEnabled) logger.DebugFormat("Finding entity permission: {0} {1}", classType.Name, identifier);
            EntityPermission ep = ControllerManager.EntityPermission.FindOrCreate(classType.Name, identifier);
            UserMember um = ControllerManager.UserMember.GetById( MembershipHelper.GetUser(userName).UserId);

            if (!ep.Users.Contains(um))
            {
                if (logger.IsDebugEnabled) logger.DebugFormat("Adding entity permission: {0} {1}", classType.Name, identifier);
                ep.Users.Add(um);
                ControllerManager.EntityPermission.Save(ep);

                // Remove any cached information
                string cachedKey = string.Format("LISTIDENTIFIERS_{0}_{1}_{2}", classType.Name.ToString(), PermissionAction.Create.ToString(), MembershipHelper.GetUser(userName).UserId);
                object result = CacheManager.ExpireItem(cachedKey);
            }
        }

        public static void AddEntityPermision(Type classType, string identifier)
        {
            EntityPermission ep = ControllerManager.EntityPermission.FindOrCreate(classType.Name, identifier);
            ControllerManager.EntityPermission.Save(ep);
        }

        public static void UpdateEntityPermission(Type classType, string identifier, string newIdentifier)
        {
            EntityPermission ep = ControllerManager.EntityPermission.Find(classType.Name, identifier);
            if(ep != null)
                ControllerManager.EntityPermission.CreateOrUpdate(ep.Name, ep.Code, ep.Description, ep.PermissionAction, newIdentifier, ep.ClassName, ep.ID);
        }

        public static void RemovePermission(Type classType, string identifier, string userName)
        {
            if (logger.IsDebugEnabled) logger.DebugFormat("Finding entity permission: {0} {1}", classType.Name, identifier);
            EntityPermission ep = ControllerManager.EntityPermission.Find(classType.Name, identifier);
            UserMember um = ControllerManager.UserMember.GetById(MembershipHelper.GetUser(userName).UserId);

            if (ep != null)
            {
                if (ep.Users.Contains(um))
                {
                    if (logger.IsDebugEnabled) logger.DebugFormat("Removing entity permission: {0} {1}", classType.Name, identifier);
                    ep.Users.Remove(um);
                    ControllerManager.EntityPermission.Save(ep);

                    // Remove any cached information
                    string cachedKey = string.Format("LISTIDENTIFIERS_{0}_{1}_{2}", classType.Name.ToString(), PermissionAction.Create.ToString(), MembershipHelper.GetUser(userName).UserId);
                    object result = CacheManager.ExpireItem(cachedKey);
                }
            }
        }

        public static void RemovePermission(Type classType, string identifier)
        {
            EntityPermission ep = ControllerManager.EntityPermission.Find(classType.Name, identifier);
            if (ep != null)
                ControllerManager.EntityPermission.Delete(ep);
        }

        public static IList GetPermissionIdentifiers(Type classType, PermissionAction action)
        {
            // Check if there are permissions for this object cached for this user.
            if (logger.IsDebugEnabled) logger.DebugFormat("Finding entity permission list: {0} {1}", classType.Name, action);
            string cachedKey = string.Format("LISTIDENTIFIERS_{0}_{1}_{2}", classType.Name.ToString(), action.ToString(), MembershipHelper.GetUser().UserId);
            object result = CacheManager.GetCached(cachedKey);
            if (result == null)
            {
                result = ControllerManager.EntityPermission.ListIdentifiers(classType, MembershipHelper.GetUser().UserId, (PermissionAction)action);
                CacheManager.AddItem(cachedKey, result);
            }

            return (IList)result;
        }

        public static IList GetPermissionIdentifiersFromFunction(Type classType, PermissionAction action)
        {
            string cachedKey = string.Format("LISTIDENTIFIERSFromFunction_{0}_{1}_{2}", classType.Name.ToString(), action.ToString(), MembershipHelper.GetUser().UserId);
            object result = CacheManager.GetCached(cachedKey);
            if (result == null)
            {
                result = ControllerManager.EntityPermission.ListIdentifiersFromFunction(classType, MembershipHelper.GetUser().UserId, (PermissionAction)action);
                CacheManager.AddItem(cachedKey, result);
            }

            return (IList)result;

        }
        public static IList GetEntitysForAction(Type classType, PermissionAction action)
        {
            return ControllerManager.EntityPermission.ListIdentifiers(classType, action);
        }

        public static IList GetUsersForEntity(Object o, string identifier, PermissionAction action)
        {
            return ControllerManager.EntityPermission.ListUsersForEntity(o, identifier, action);
        }
    }
}
