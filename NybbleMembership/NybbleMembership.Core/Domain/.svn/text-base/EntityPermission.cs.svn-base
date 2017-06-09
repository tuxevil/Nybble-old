using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.UI;
using ProjectBase.Data;

namespace NybbleMembership.Core.Domain
{
    public class EntityPermission : Permission
    {
        public EntityPermission()
        {
            
        }

        public EntityPermission(string className, string identifier)
        {
            this.className = className;
            this.identifier = identifier;
        }

        private string className;
        private string identifier;
        
        public virtual string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        public virtual string Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        public override Type[] GetClassTypes()
        {
            Type[] allowtypes = new Type[2];
            allowtypes[0] = typeof(Entity<int>);
            allowtypes[1] = typeof(Entity<string>);
            return allowtypes;
        }

        public override bool CheckInstance(object o, Enum action)
        {
            if (o is Entity<string>)
                return (o.GetType().Name.ToLower() == className.ToLower() && ((Entity<string>)o).ID == identifier);
            else
                return (o.GetType().Name.ToLower() == className.ToLower() && ((Entity<int>)o).ID.ToString() == identifier);
        }
    }
}
