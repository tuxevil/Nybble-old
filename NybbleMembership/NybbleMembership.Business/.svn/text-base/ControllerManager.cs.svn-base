using System;
using System.Collections.Generic;
using System.Text;
using NybbleMembership.Business.Controller;
using NybbleMembership.Common;
using NybbleMembership.Core.Domain;

namespace NybbleMembership.Business
{
    public class ControllerManager
    {
        public static SiteController Site
        {
            get { return new SiteController(Config.FactoryConfigPath);}
        }

        public static RolController Rol
        {
            get { return new RolController(Config.FactoryConfigPath); }
        }

        public static FunctionController Function
        {
            get { return new FunctionController(Config.FactoryConfigPath); }
        }

        public static UserMemberController UserMember
        {
            get { return new UserMemberController(Config.FactoryConfigPath); }
        }

        public static UserProfileController UserProfile
        {
            get { return new UserProfileController(Config.FactoryConfigPath); }
        }
        
        public static PermissionController<Permission> Permission
        {
            get { return new PermissionController<Permission>(Config.FactoryConfigPath); }
        }

        public static PagePermissionController PagePermission
        {
            get { return new PagePermissionController(Config.FactoryConfigPath); }
        }

        public static MethodPermissionController MethodPermission
        {
            get { return new MethodPermissionController(Config.FactoryConfigPath); }
        }

        public static EntityPermissionController EntityPermission
        {
            get { return new EntityPermissionController(Config.FactoryConfigPath); }
        }

        public static WebControlPermissionController WebControlPermission
        {
            get { return new WebControlPermissionController(Config.FactoryConfigPath); }
        }

        public static ExecutePermissionController ExecutePermission
        {
            get { return new ExecutePermissionController(Config.FactoryConfigPath); }
        }
    }
}
