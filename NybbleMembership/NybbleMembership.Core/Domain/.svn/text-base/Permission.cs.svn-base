using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ProjectBase.Data;
using ProjectBase.Utils;
using ProjectBase.Utils.Cache;

namespace NybbleMembership.Core.Domain
{
    public class Permission : AuditableEntity<int>
    {
        private string name;
        private string description;
        private string code;
        private IList<Function> functions = new List<Function>();
        private Enum action;
        private IList<UserMember> users = new List<UserMember>();

        protected virtual bool AllowExternalManagement
        {
            get { return false;  }
        }

        public virtual bool CanCreate
        {
            get { return AllowExternalManagement; }
        }

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

        public virtual string Code
        {
            get { return code; }
            set { code = value; }
        }

        public virtual IList<Function> Functions
        {
            get
            {
                if (functions == null)
                    functions = new List<Function>();
                return functions;
            }
            set { functions = value; }
        }

        public virtual Enum PermissionAction
        {
            get { return action; }
            set { action = value; }
        }

        public virtual Type[] GetClassTypes()
        {
            return null;
        }

        public virtual bool CheckInstance(object o, Enum action)
        {
            return false;
        }

        public virtual bool Check(object o, Enum action)
        {
            if (!CanCheck(o))
                throw new TypeInitializationException(o.GetType().FullName, new Exception(string.Format("The permission {0} can not be applied to this object.",this.GetType().FullName)));

            return CheckInstance(o, action);
        }

        public virtual bool CanCheck(object o)
        {
            return this.MayCheck(o);
        }


        /// <summary>
        /// Validate if the current object can be checked by the current class.
        /// It also caches the result to accelerate processing on several similar objects types.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        protected virtual bool MayCheck(object o)
        {
            bool result = false;

                Type[] types = GetClassTypes();

                if (types != null)
                {
                    foreach (Type t in types)
                        if (t.IsInstanceOfType(o))
                        {
                            result = true;
                            break;
                        }
                }

            return result;            
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

        public Permission() { }

        public Permission(int id)
        {
            this.id = id;
            
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}", this.id, this.code);
        }
    }
}
