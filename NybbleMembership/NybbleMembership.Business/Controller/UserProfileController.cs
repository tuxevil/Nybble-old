using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NybbleMembership.Core.Domain;
using ProjectBase.Data;

namespace NybbleMembership.Business.Controller
{
    public class UserProfileController :  AbstractNHibernateDao<UserProfile, Guid>
    {
        public UserProfileController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public void Create(Guid id, string firstName, string lastName)
        {
            UserProfile up = new UserProfile(id);
            up.FirstName = firstName;
            up.LastName = lastName;
            up.UserMember = ControllerManager.UserMember.GetById(id);

            ControllerManager.UserProfile.Save(up);
        }
    }

}
