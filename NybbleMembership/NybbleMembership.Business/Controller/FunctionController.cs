using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NybbleMembership.Core.Domain;
using ProjectBase.Data;

namespace NybbleMembership.Business.Controller
{
    public class FunctionController :  AbstractNHibernateDao<Function, int>
    {
        public FunctionController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<Function> List()
        {
            return GetAll();
        }

        public bool Erase(int id)
        {

            if (CanErase(id))
            {
                Function f = GetById(id);

                IList<Function> sons = List();
                foreach (Function fs in sons)
                    if (fs.Parent == f)
                        Delete(fs);
                Delete(f);

                NHibernateSession.Flush();
                return true;
            }
            return false;
        }

        private bool CanErase(int id)
        {
            Function f = ControllerManager.Function.GetById(id);

            if (f.Permissions.Count > 0)
                return false;

            return true;
        }

        private bool Exist(string name)
        {
            ICriteria crit = GetCriteria();
            crit.Add(Expression.Eq("Name", name));
            crit.SetMaxResults(1);
            return crit.UniqueResult() != null;
        }

        public IList<Function> ListBySite(Site site)
        {
            ICriteria crit = GetCriteria();
            crit.Add(Expression.Eq("Site", site));
            return crit.List<Function>();
        }

        #region Create & Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="siteId"></param>
        /// <param name="parentId"></param>
        /// <param name="permissions"></param>
        public void Create(string name, string description, int siteId, int parentId, IList<Permission> permissions)
        {
            CreateOrUpdate(name, description,siteId,parentId, permissions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="siteId"></param>
        /// <param name="parentId"></param>
        /// <param name="permissions"></param>
        public void Edit(int id, string name, string description, int siteId, int parentId, IList<Permission> permissions)
        {
            CreateOrUpdate(name, description, siteId, parentId, permissions, id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="siteId"></param>
        /// <param name="parentId"></param>
        /// <param name="permissions"></param>
        private void CreateOrUpdate(string name, string description, int siteId, int parentId, IList<Permission> permissions)
        {
            CreateOrUpdate(name, description, siteId, parentId, permissions, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="siteId"></param>
        /// <param name="parentId"></param>
        /// <param name="permissions"></param>
        /// <param name="id"></param>
        private void CreateOrUpdate(string name, string description, int siteId, int parentId, IList<Permission> permissions, int id)
        {
            Function f;
            if (id == 0)
            {
                if (Exist(name))
                    throw new Exception("Duplicated Name for this Function");

                f = new Function();
            }
            else
                f = ControllerManager.Function.GetById(id);

            f.Name = name;
            f.Description = description;
            f.Site = new Site(siteId);
            if(parentId != 0)
              f.Parent = new Function(parentId);

            f.Permissions.Clear();
            foreach (Permission p in permissions)
                f.Permissions.Add(p);


            Save(f);
        }
        #endregion
    }
}
