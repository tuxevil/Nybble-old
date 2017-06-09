using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.UI;
using ProjectBase.Data;

namespace NybbleMembership.Core.Domain
{
    public class ExecutePermission : Permission
    {
        public ExecutePermission()
        {

        }

        public ExecutePermission(string className, string keyIdentifier)
        {
            this.className = className;
            this.keyIdentifier = keyIdentifier;
        }

        private string className;
        private string keyIdentifier;

        public virtual string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        public virtual string KeyIdentifier
        {
            get { return keyIdentifier; }
            set { keyIdentifier = value; }
        }

        public override Type[] GetClassTypes()
        {
            Type[] allowtypes = new Type[1];
            allowtypes[0] = typeof(ExecutePermissionValidator);
            return allowtypes;
        }

        public override bool CheckInstance(object o, Enum action)
        {
            ExecutePermissionValidator epv = o as ExecutePermissionValidator;
            if (epv.ClassType.Name.ToLower() == className.ToLower() && epv.KeyIdentifier.ToLower() == keyIdentifier.ToLower())
                return true;

            return false;
        }
    }
}
