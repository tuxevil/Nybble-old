using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NybbleMembership.Core.Domain;
using ProjectBase.Data;

namespace NybbleMembership.Business.Controller
{
    public class UserMemberController :  AbstractNHibernateDao<UserMember, Guid>
    {
       public UserMemberController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }
       
        public IList<UserMember> List()
        {
            return GetAll();
            
        }

        //public void Create (Guid id, IList<Rol> roles)
        //{
        //   UserMember s = ControllerManager.UserMember.GetById(id);

        //    s.Roles.Clear();
        //    foreach (Rol r in roles)
        //        s.Roles.Add(r);
           
        //    Save(s);
        //}

        public void Erase(Guid id)
        {
            Delete(new UserMember(id));
            NHibernateSession.Flush();
        }

        public IList<UserMember> ListBySite(Site site)
        {
            ICriteria crit = GetCriteria();
            ICriteria sites = crit.CreateCriteria("Sites");
            sites.Add(Expression.Eq("Name", site.Name));
            return crit.List<UserMember>();
        }

        public IList<UserMember> GetUsersByRole(string role, string site)
        {
            ICriteria crit = GetCriteria();
            ICriteria sites = crit.CreateCriteria("Sites");
            sites.Add(Expression.Eq("Code", site));
            ICriteria roles = crit.CreateCriteria("Roles");
            roles.Add(Expression.Eq("Name", role));

            return crit.List<UserMember>();
        }

        public IList<UserMember> GetAdministrators()
        {
            ICriteria crit = GetCriteria();
            ICriteria rol = crit.CreateCriteria("Roles");
            rol.Add(Expression.Eq("IsAdministrator", true));

            return crit.List<UserMember>();
        }

        public IList<UserMember> ListBySite(string site)
        {
            return ListBySite(ControllerManager.Site.Get(site));
        }

        public void Modify(Guid id, string firstName, string lastName, string mail)
        {
            UserMember um = GetById(id);

            um.UserProfile.FirstName = firstName;
            um.UserProfile.LastName = lastName;
            um.Email = mail;

            Save(um);
            
        }
        

    }
}
