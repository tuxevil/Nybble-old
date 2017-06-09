using System;
using System.Collections.Generic;
using System.Text;

namespace NybbleMembership.Core.Domain
{
    public class ExecutePermissionValidator
    {
        private Type classType;
        public Type ClassType
        {
            get { return classType; }
            set { classType = value; }
        }

        private string keyIdentifier;
        public string KeyIdentifier
        {
            get { return keyIdentifier; }
            set { keyIdentifier = value; }
        }

        public override string ToString()
        {
            return ClassType.Name + KeyIdentifier;
        }
    }
}
