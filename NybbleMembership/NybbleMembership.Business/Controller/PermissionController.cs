using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using NHibernate;
using NHibernate.Criterion;
using NybbleMembership.Core;
using NybbleMembership.Core.Domain;
using ProjectBase.Data;
using System.Reflection;

namespace NybbleMembership.Business.Controller
{
    public class PermissionController<T> :  AbstractNHibernateDao<T, int>
    {
        public PermissionController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<T> List()
        {
            return GetAll();
        }

        protected bool Exist(string name, string code)
        {
            ICriteria crit = GetCriteria();
            crit.Add(Expression.Eq("Name", name));
            crit.Add(Expression.Eq("Code", code));
            crit.SetMaxResults(1);
            return crit.UniqueResult() != null;
        }

        protected Permission CreateOrUpdate(string name, string code, string description, Enum action, int id)
        {
            Permission p;
            if (id == 0)
            {
                if (Exist(name, code))
                    throw new Exception("Duplicated Name for this Permission");

                //p = Assembly.GetExecutingAssembly().CreateInstance(typeof(T).FullName) as Permission;
                p = typeof(T).InvokeMember("ctor", BindingFlags.CreateInstance, null, null, null) as Permission;

            }
            else
                p = ControllerManager.Permission.GetById(id);

            p.Name = name;
            p.Code = code;
            p.Description = description;
            p.PermissionAction = action;

            return p;
        }

        public void Erase(int id)
        {
            Delete(this.GetById(id));
            NHibernateSession.Flush();
        }

        public IList<Permission> ListForCurrentUserAndSite(object userId, Enum action, string siteCode)
        {
            Site s = ControllerManager.Site.Get(siteCode);
            if (s == null)
                throw new NullReferenceException(string.Format("There is no site with the code '{0}' defined.",siteCode));

            if (userId == null)
                throw new NullReferenceException("There is no Membership User logged."); 

            string hql = "select distinct P from HierarchyFunctionsView HFV join HFV.ParentFunction PF join HFV.ChildFunction F join F.Permissions P join PF.Roles R join R.Users U where U.ID = :UserID and PF.Site = :Site";

            if (action != null)
                hql += " and P.PermissionAction = :Action";

            IQuery q = NHibernateSession.CreateQuery(hql).SetEntity("Site", s).SetGuid("UserID", (Guid)userId);
            if (action != null)
                q.SetEnum("PermissionAction", action);
   
            return q.List<Permission>();
        }

        public List<Permission> ListPermisionsByUser(object userId, string siteCode)
        {
            Site s = ControllerManager.Site.Get(siteCode);
            if (s == null)
                throw new NullReferenceException(string.Format("There is no site with the code '{0}' defined.", siteCode));

            if (userId == null)
                throw new NullReferenceException("There is no Membership User logged.");

            string hql = "select distinct P from Permission P join P.Users U join U.Sites S where U.ID = :UserID and S = :Site";

            IQuery q = NHibernateSession.CreateQuery(hql).SetEntity("Site", s).SetGuid("UserID", (Guid)userId);

            return q.List<Permission>() as List<Permission>;
        }
    }


    public class PagePermissionController : PermissionController<PagePermission>
    {
        public PagePermissionController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public void Create(string name, string code, string description, Enum action, string pageName, string folderName)
        {
            CreateOrUpdate(name, code, description, action, pageName, folderName);
        }

        public void Edit(int id, string name, string code, string description, Enum action, string pageName, string folderName)
        {
            CreateOrUpdate(name, code, description, action, pageName, folderName,id);
        }

        private void CreateOrUpdate(string name, string code, string description, Enum action, string pageName, string folderName)
        {
            CreateOrUpdate(name, code, description, action, pageName, folderName,0);
        }

        public PagePermission CreateOrUpdate(string name, string code, string description, Enum action, string pageName, string folderName, int id)
        {
            PagePermission p = base.CreateOrUpdate(name, code, description, action, id) as PagePermission;
            p.PageName = pageName;
            p.FolderName = folderName;
            Save(p);
            return p;
        }

    }

    public class MethodPermissionController : PermissionController<MethodPermission>
    {
        public MethodPermissionController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public void Create(string name, string code, string description, Enum action, string methodName, string className)
        {
            CreateOrUpdate(name, code, description, action, methodName, className);
        }

        public void Edit(int id, string name, string code, string description, Enum action, string methodName, string className)
        {
            CreateOrUpdate(name, code, description, action, methodName, className, id);
        }

        private void CreateOrUpdate(string name, string code, string description, Enum action, string methodName, string className)
        {
            CreateOrUpdate(name, code, description, action, methodName, className, 0);
        }

        public MethodPermission CreateOrUpdate(string name, string code, string description, Enum action, string methodName, string className, int id)
        {
            MethodPermission p = base.CreateOrUpdate(name, code, description, action, id) as MethodPermission;
            p.MethodName = methodName;
            p.ClassName = className;
            Save(p);
            return p;
        }
    }

    public class EntityPermissionController : PermissionController<EntityPermission>
    {
        public EntityPermissionController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public void Create(string name, string code, string description, Enum action, string identifier, string className)
        {
            CreateOrUpdate(name, code, description, action, identifier, className);
        }

        public void Edit(int id, string name, string code, string description, Enum action, string identifier, string className)
        {
            CreateOrUpdate(name, code, description, action, identifier, className, id);
        }

        private void CreateOrUpdate(string name, string code, string description, Enum action, string identifier, string className)
        {
            CreateOrUpdate(name, code, description, action, identifier, className, 0);
        }

        public EntityPermission CreateOrUpdate(string name, string code, string description, Enum action, string identifier, string className, int id)
        {
            EntityPermission p = base.CreateOrUpdate(name, code, description, action, id) as EntityPermission;
            p.Identifier = identifier;
            p.ClassName = className;
            Save(p);
            return p;
        }

        public bool ListIdentifiers(Type classType, Object userId, PermissionAction action, out IList list)
        {
            string hql = "SELECT DISTINCT EP.Identifier FROM EntityPermission EP JOIN EP.Functions F JOIN F.Roles R JOIN R.Users U WHERE ClassName = :ClassName AND U.ID = :UserID AND EP.PermissionAction = :Action AND EP.Identifier IS NULL";
            IQuery q = NHibernateSession.CreateQuery(hql).SetString("ClassName", classType.Name).SetGuid("UserID", (Guid)userId).SetEnum("Action", action);
            list = q.List();

            if(list.Count > 0)
                return true;

            list = ListIdentifiers(classType, userId, action);
            return false;
        }


        public IList ListIdentifiers(Type classType, Object userId, PermissionAction action)
        {
            string hql = "SELECT DISTINCT EP.Identifier FROM EntityPermission EP JOIN EP.Users U WHERE ClassName = :ClassName AND U.ID = :UserID AND EP.PermissionAction = :Action";

            IQuery q = NHibernateSession.CreateQuery(hql).SetString("ClassName", classType.Name).SetGuid("UserID", (Guid)userId).SetEnum("Action", action);

            return q.List();
        }

        public IList ListIdentifiers(Type classType, PermissionAction action)
        {
            string hql = "SELECT DISTINCT EP.Identifier FROM EntityPermission EP WHERE ClassName = :ClassName AND EP.PermissionAction = :Action AND EP.Identifier IS NOT NULL";
            //string hql = "SELECT DISTINCT EP.Identifier FROM EntityPermission EP JOIN EP.Functions F JOIN F.Roles R JOIN R.Users U WHERE ClassName = :ClassName AND U.ID = :UserID AND EP.PermissionAction = :Action";

            IQuery q = NHibernateSession.CreateQuery(hql).SetString("ClassName", classType.Name).SetEnum("Action", action);

            return q.List();
        }

        public IList ListIdentifiersFromFunction(Type classType, Object userId, PermissionAction action)
        {
            string hql = "SELECT DISTINCT EP.Identifier FROM EntityPermission EP JOIN EP.Functions F JOIN F.Roles R JOIN R.Users U WHERE ClassName = :ClassName AND U.ID = :UserID AND EP.PermissionAction = :Action";
            IQuery q = NHibernateSession.CreateQuery(hql).SetString("ClassName", classType.Name).SetGuid("UserID", (Guid)userId).SetEnum("Action", action);

            return q.List();
        }

        public IList ListUsersForEntity(Object o, string identifier, PermissionAction action)
        {
            string hql = "SELECT DISTINCT U.Email FROM EntityPermission EP JOIN EP.Users U WHERE EP.ClassName = :ClassName AND (EP.Identifier = :Identifier OR EP.Identifier is NULL)AND EP.PermissionAction = :Action";

            IQuery q = NHibernateSession.CreateQuery(hql).SetString("ClassName", o.GetType().Name).SetString("Identifier", identifier).SetEnum("Action", action);

            return q.List();
        }

        public EntityPermission FindOrCreate(string className, string identifier)
        {
            EntityPermission ep = Find(className, identifier);
            MembershipUser um = Membership.GetUser();
            
            if (um == null)
                throw new Exception("Debe estar logueado para poder realizar esta acción.");

            if (ep == null)
            {
                ep = new EntityPermission(className, identifier);
                //Hardcoded, pincha si no se le agrega  un permissionaction y demas campos no nullables
                ep.PermissionAction = PermissionAction.Create;
                ep.Name = className + "-" + identifier;
                ep.Code = "PL-" + identifier;
                ep.Description = "Permiso para " + identifier + " por usuario";
                ep.TimeStamp.CreatedBy = um;
                ep.TimeStamp.CreatedOn = DateTime.Today;
                ep.TimeStamp.ModifiedBy = um;
                ep.TimeStamp.ModifiedOn = DateTime.Today;
            }
            else
            {
                ep.TimeStamp.ModifiedBy = um;
                ep.TimeStamp.ModifiedOn = DateTime.Today;
            }

            return ep;
        }

        public EntityPermission Find(string className, string identifier)
        {
            ICriteria crit = GetCriteria();
            crit.Add(Expression.Eq("ClassName", className));
            crit.Add(Expression.Eq("Identifier", identifier));
            return crit.UniqueResult<EntityPermission>();
        }

    }

    public class WebControlPermissionController : PermissionController<WebControlPermission>
    {
        public WebControlPermissionController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public void Create(string name, string code, string description, Enum action, string relativePath, string controlIdentifier)
        {
            CreateOrUpdate(name, code, description, action, controlIdentifier, relativePath);
        }

        public void Edit(int id, string name, string code, string description, Enum action, string relativePath, string controlIdentifier)
        {
            CreateOrUpdate(name, code, description, action, controlIdentifier, relativePath, id);
        }

        private void CreateOrUpdate(string name, string code, string description, Enum action, string controlIdentifier, string relativePath)
        {
            CreateOrUpdate(name, code, description, action, controlIdentifier, relativePath, 0);
        }

        public WebControlPermission CreateOrUpdate(string name, string code, string description, Enum action, string controlIdentifier, string relativePath, int id)
        {
            WebControlPermission p = base.CreateOrUpdate(name, code, description, action, id) as WebControlPermission;
            p.ControlIdentifier = controlIdentifier;
            p.RelativePath = relativePath;
            Save(p);
            return p;
        }
    }

    public class ExecutePermissionController : PermissionController<ExecutePermission>
    {
        public ExecutePermissionController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public void Create(string name, string code, string description, Enum action, string keyIdentifier, string className)
        {
            CreateOrUpdate(name, code, description, action, keyIdentifier, className);
        }

        public void Edit(int id, string name, string code, string description, Enum action, string keyIdentifier, string className)
        {
            CreateOrUpdate(name, code, description, action, keyIdentifier, className, id);
        }

        private void CreateOrUpdate(string name, string code, string description, Enum action, string keyIdentifier, string className)
        {
            CreateOrUpdate(name, code, description, action, keyIdentifier, className, 0);
        }

        public ExecutePermission CreateOrUpdate(string name, string code, string description, Enum action, string keyIdentifier, string className, int id)
        {
            ExecutePermission ep = base.CreateOrUpdate(name, code, description, action, id) as ExecutePermission;
            ep.KeyIdentifier= keyIdentifier;
            ep.ClassName = className;
            Save(ep);
            return ep;
        }
    }
}
