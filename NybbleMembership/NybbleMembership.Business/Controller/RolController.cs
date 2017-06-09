using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using NHibernate;
using NHibernate.Criterion;
using NybbleMembership.Core.Domain;
using ProjectBase.Data;

namespace NybbleMembership.Business.Controller
{
    public class RolController :  AbstractNHibernateDao<Rol, int>
    {
        public RolController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<Rol> List()
        {
            return GetAll();
        }


        public IList<Rol> List(string siteCode)
        {
            ICriteria crit = GetCriteria();
            crit.Add(Expression.Eq("Site", ControllerManager.Site.Get(siteCode)));
            return crit.List<Rol>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="siteId"></param>
        /// <returns></returns>
        private bool Exist(string name, int siteId)
        {
            ICriteria crit = GetCriteria();
            crit.Add(Expression.Eq("Name", name));
            crit.Add(Expression.Eq("Site", new Site(siteId)));
            crit.SetMaxResults(1);
            return crit.UniqueResult() != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="siteId"></param>
        /// <param name="isAdmin"></param>
        /// <param name="userMembers"></param>
        /// <exception cref="DuplicatedRecord" />
        public void Create(string name, string description, int siteId, bool isAdmin, IList<UserMember> userMembers, IList<Function> functions)
        {
            CreateOrUpdate(name, description, siteId, isAdmin, userMembers, functions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="siteId"></param>
        /// <param name="isAdmin"></param>
        /// <param name="userMembers"></param>
        public void Edit(int id, string name, string description, int siteId, bool isAdmin, IList<UserMember> userMembers, IList<Function> functions)
        {
            CreateOrUpdate(name, description, siteId, isAdmin, userMembers, functions, id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="siteId"></param>
        /// <param name="isAdmin"></param>
        /// <param name="userMembers"></param>
        private void CreateOrUpdate(string name, string description, int siteId, bool isAdmin, IList<UserMember> userMembers, IList<Function> functions)
        {
            CreateOrUpdate(name, description, siteId, isAdmin, userMembers, functions, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="siteId"></param>
        /// <param name="isAdmin"></param>
        /// <param name="userMembers"></param>
        private void CreateOrUpdate(string name, string description, int siteId, bool isAdmin, IList<UserMember> userMembers,IList<Function> functions, int id)
        {
            Rol r;
            if (id == 0)
            {
                if (Exist(name, siteId))
                    throw new Exception("Duplicated Name for this Rol");

                r = new Rol();
            }
            else
                r = ControllerManager.Rol.GetById(id);
            
            r.Name = name;
            r.Description = description;
            r.IsAdministrator = isAdmin;

            r.Site = new Site(siteId);

            r.Users.Clear();
            foreach( UserMember um in userMembers )
                r.Users.Add(um);

            r.Functions.Clear();
            foreach (Function f in functions)
                r.Functions.Add(f);
           
            Save(r);
         }

        public bool Erase (int id)
        {
            if (CanErase(id))
            {
                
                Delete(ControllerManager.Rol.GetById(id));
                NHibernateSession.Flush();
                return true;
            }
            return false;
        }

        private bool CanErase(int id)
        {
            Rol r = ControllerManager.Rol.GetById(id);
            
            if (r.Functions.Count > 0)
                return false;

            return true;
        }

        private Rol Get(string roleName, string siteCode)
        {
            Site s = ControllerManager.Site.Get(siteCode);
            if (s == null)
                throw new NullReferenceException(string.Format("Site '{0}'not found.", siteCode));

            ICriteria crit = GetCriteria();
            crit.Add(Expression.Eq("Name", roleName));
            crit.Add(Expression.Eq("Site", s));
            crit.SetMaxResults(1);
            return crit.UniqueResult<Rol>();
        }

        public IList<Rol> GetByUser(object userId, string siteCode)
        {
            Site s = ControllerManager.Site.Get(siteCode);
            if (s == null)
                throw new NullReferenceException(string.Format("Site '{0}'not found.", siteCode));

            ICriteria crit = GetCriteria();
            crit.Add(Expression.Eq("Site", s));
            crit.CreateCriteria("Users").Add(Expression.Eq("ID", userId));
            crit.SetResultTransformer(NHibernate.Transform.Transformers.DistinctRootEntity);
            return crit.List<Rol>();
        }

        public bool AddUserToRole(object userId, string roleName, string siteCode)
        {
            Rol r = Get(roleName, siteCode);
            if (r == null)
                throw new NullReferenceException(string.Format("Rol '{0}'not found for site '{1}'.", roleName, siteCode));

            UserMember um = new UserMember((Guid)userId);

            if (r.Users.Contains(um))
                return false;

            r.Users.Add(um);
            Save(r);

            return true;
        }

        public bool RemoveUserFromRole(object userId, string roleName, string siteCode)
        {
            Rol r = Get(roleName, siteCode);
            if (r == null)
                throw new NullReferenceException(string.Format("Rol '{0}'not found for site '{1}'.", roleName, siteCode));

            UserMember um = new UserMember((Guid)userId);

            if (!r.Users.Contains(um))
                return false;

            r.Users.Remove(new UserMember((Guid)userId));
            Save(r);

            return true;
        }

        //public IList<UserMember> GetUsersByRole(string roleName, string siteCode)
        //{
        //    Rol r = Get(roleName, siteCode);
        //    if (r == null)
        //        throw new NullReferenceException(string.Format("Rol '{0}'not found for site '{1}'.", roleName, siteCode));

        //    return r.Users;
        //}

        public bool IsAdministrator(object userId, string siteCode)
        {
            Site s = ControllerManager.Site.Get(siteCode);
            if (s == null)
                throw new NullReferenceException(string.Format("Site '{0}'not found.", siteCode));

            ICriteria crit = GetCriteria();
            crit.Add(Expression.Eq("Site", s));
            crit.Add(Expression.Eq("IsAdministrator", true));
            crit.CreateCriteria("Users").Add(Expression.Eq("ID", userId));
            crit.SetResultTransformer(NHibernate.Transform.Transformers.DistinctRootEntity);
            return crit.List<Rol>().Count > 0;
        }
    }
}
