using System;
using System.Web.Security;
using System.Collections.Generic;
using System.Text;
using ProjectBase.Data;

namespace NybbleMembership.Core.Domain
{
    public class Rol : AuditableEntity<int>
    {
        private Site site;
        private IList<Function> function;
        private string name;
        private string description;
        private IList<UserMember> users;
        private bool isAdministrator;
        

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        public virtual Site Site
        {
            get { return site; }
            set { site = value; }
        }

        public virtual string SiteName
        {
            get { return (Site.Name + " [" + Site.ID + "]") ; }
        }

        public virtual IList<UserMember> Users
        {
            get
            {
                if (users == null)
                    users = new List<UserMember>();
                return users;
            }
            set { users = value; }
        }
        
        public virtual IList<Function> Functions
        {
            get
            {
                if (function == null)
                    function = new List<Function>();

                return function;
            }
            set { function = value; }
        }

        public virtual bool IsAdministrator
        {
            get { return isAdministrator; }
            set { isAdministrator = value; }
        }

        public Rol() { }

        public Rol(int id)
        {
            this.id = id;
            
        }
    }
}
