using System;
using System.Collections.Generic;
using System.Text;
using ProjectBase.Data;
using System.Web.Security;

namespace NybbleMembership.Core.Domain
{
    public class UserProfile : DomainObject<Guid>
    {
        private string firstName;
        private string lastName;
        private UserMember userMember;

        public virtual string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public virtual string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public virtual UserMember UserMember
        {
            get { return userMember; }
            set { userMember = value; }
        }

        public virtual string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public UserProfile(Guid id)
        {
            this.id = id;
        }

        public UserProfile()
        {
        }
    }
}
