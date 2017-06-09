using System;
using System.Collections.Generic;
using System.Text;

namespace NybbleMembership
{
    public class NotValidPermissionException : Exception
    {
    }

    public class NotAllowedPermissionException : Exception
    {
        public NotAllowedPermissionException(string message) : base(message)
        {
        }
    }
}
