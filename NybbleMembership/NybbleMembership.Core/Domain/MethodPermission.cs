using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NybbleMembership.Core.Domain
{
    public class MethodPermission : Permission
    {
        private string className;
        private string methodName;

        public virtual string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        public virtual string MethodName
        {
            get { return methodName; }
            set { methodName = value; }
        }

        protected override bool AllowExternalManagement
        {
            get
            {
                return true;
            }
        }

        public override Type[] GetClassTypes()
        {
            
            int cant = 1;
            Type[] allowtypes = new Type[cant];
            Type method = typeof(MethodBase);
            allowtypes[0] = method;
            
            return allowtypes;
        }

        public override bool CheckInstance(object o, Enum action)
        {
            MethodBase mb = (MethodBase) o;
            string name = mb.Name;
            string clase = mb.DeclaringType.ToString();
            clase = clase.Substring(clase.LastIndexOf(".") + 1);

            if(name == this.methodName && clase == this.className)
                return true;

            return false;
        }

    }
}
