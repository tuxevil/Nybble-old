using System;
using System.Collections.Generic;
using System.Text;
using ProjectBase.Data;

namespace NybbleMembership.Core.Domain
{
    public class Site : AuditableEntity<int>
    {
        private string name;
        private string code;
        private string appName;
        private IList<UserMember> users;


        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual string Code
        {
            get { return code; }
            set { code = value; }
        }

        public virtual string AppName
        {
            get { return appName; }
            set { appName = value; }
        }

        public virtual IList<UserMember> Users
        {
            get
            {
                if (users== null)
                    users = new List<UserMember>();
                return users;
            }
            set { users = value; }
        }

        public Site() { }

        public Site(int id)
        {
            this.id = id;
            
        }
    }
}
