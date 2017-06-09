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
    public class SiteController : AbstractNHibernateDao<Site, int>
    {
        public SiteController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<Site> List()
        {
            return GetAll();
        }

        private bool Exist(string name, string code)
        {
            ICriteria crit = GetCriteria();
            crit.Add(Expression.Eq("Code", code));
            crit.SetMaxResults(1);
            return crit.UniqueResult() != null;
        }

        public Site Get(string code)
        {
            ICriteria crit = GetCriteria();
            crit.Add(Expression.Eq("Code", code));
            crit.SetMaxResults(1);
            return crit.UniqueResult<Site>();
        }

        public IList<Site> CanManage(object userId)
        {
            string hql =
                "SELECT DISTINCT R.Site FROM Rol R JOIN R.Users U WHERE R.IsAdministrator = :IsAdministrator AND U.ID = :UserID";
            
            return NHibernateSession.CreateQuery(hql).SetBoolean("IsAdministrator", true).SetGuid("UserID", (Guid)userId).List<Site>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="userMembers"></param>
        /// <exception cref="DuplicatedRecord" />
        public void Create(string name, string code, IList<UserMember> userMembers)
        {
            CreateOrUpdate(name, code, userMembers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="userMembers"></param>
        public void Edit(int id, string name, string code, IList<UserMember> userMembers)
        {
            CreateOrUpdate(name, code, userMembers, id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="userMembers"></param>
        private void CreateOrUpdate(string name, string code, IList<UserMember> userMembers)
        {
            CreateOrUpdate(name, code, userMembers, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="userMembers"></param>
        private void CreateOrUpdate(string name, string code, IList<UserMember> userMembers, int id)
        {
            Site s;
            if (id == 0)
            {
                if (Exist(name, code))
                    throw new Exception("Duplicated Code or Name for this Site");

                s = new Site();
            }
            else
                s = ControllerManager.Site.GetById(id);
            
            s.Name = name;
            s.Code = code;
            s.AppName = Membership.ApplicationName;

            s.Users.Clear();
            foreach (UserMember um in userMembers)
                s.Users.Add(um);
            
            Save(s);
        }

        public bool Erase(int id)
        {
            if (CanErase(id))
            {
                Delete(new Site(id));
                NHibernateSession.Flush();
                return true;
            }
            return false;
        }

        private bool CanErase(int id)
        {

            string hql = "SELECT R.Id from mem_Rol R where R.SiteId = :SiteId ";
            IQuery q = NHibernateSession.CreateSQLQuery(hql);
            q.SetInt32("SiteId", id);

            string hql2 = "SELECT F.Id from mem_Function F where F.SiteId = :SiteId ";
            IQuery q2 = NHibernateSession.CreateSQLQuery(hql2);
            q2.SetInt32("SiteId", id);


            if (q.List<int>().Count > 0 || q2.List<int>().Count > 0)
                return false;

            return true;
        }
    }
}