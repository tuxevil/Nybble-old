using System;
using System.Collections.Generic;
using System.Text;
using ProjectBase.Data;

namespace NybbleMembership.Core.Domain
{
    public class Function : AuditableEntity <int>
    {
        private string name;
        private string description;
        private Function parent;
        private IList<Rol> roles;
        private IList<Permission> permissions;
        private Site site;

        public virtual string Name
        {
            get{return name;}
            
            set{name = value;}
        }

        public virtual string Description
        {
            get { return description; }

            set { description = value; }
        }

        public virtual Function Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public virtual IList<Rol> Roles
        {
            get
            {
                if (roles == null)
                    roles = new List<Rol>();
                return roles;
            }
            set { roles = value; }
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

        public virtual Site Site
        {
            get { return site; }
            set { site = value; }
        }

        public virtual string SiteName
        {
            get { return (Site.Name + " [" + Site.ID + "]"); }
        }

        public virtual string ParentName
        {
            get
            {
                if(Parent != null)
                    return (Parent.Name) ;
                
                return string.Empty;
            }
        }

        public Function() { }

        public Function(int id)
        {
            this.id = id;
            
        }
    }
}
