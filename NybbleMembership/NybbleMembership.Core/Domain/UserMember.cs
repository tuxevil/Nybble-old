using System;
using System.Collections.Generic;
using System.Text;
using ProjectBase.Data;
using System.Web.Security;

namespace NybbleMembership.Core.Domain
{
    public class UserMember : DomainObject<Guid>
    {
        private string email;
        private bool isApproved;
        private bool isLockedOut;
        private IList<Rol> roles;
        private DateTime createDate;
        private IList<Site> sites;
        private IList<Permission> permissions;
        private string userName;
        private UserProfile userProfile;

        public virtual string Email
        {
            get { return email; }
            set { email = value; }
        }

        public virtual bool IsApproved
        {
            get { return isApproved; }
            set { isApproved = value; }
        }

        public virtual IList<Rol> Roles
        {
            get
            {
                if (roles== null)
                    roles = new List<Rol>();
                return roles;
            }
            set { roles = value; }
        }

        public virtual IList<Site> Sites
        {
            get
            {
                if (sites== null)
                    sites= new List<Site>();
                return sites;
            }
            set { sites = value; }
        }

        public virtual IList<Permission> Permissions
        {
            get
            {
                if (permissions == null)
                    permissions = new List<Permission>();
                return permissions;
            }
            set { permissions = value; }
        }

        public virtual bool IsLockedOut
        {
            get { return isLockedOut; }
            set { isLockedOut = value; }
        }

        public virtual DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        public UserMember() { }

        public UserMember(Guid id)
        {
            this.id = id;
            
        }

        public virtual string UserName
        {
            get { return Membership.GetUser(ID).UserName; }
        }

        public virtual UserProfile UserProfile
        {
            get { return userProfile; }
            set { userProfile = value; }
        }
    }
}
