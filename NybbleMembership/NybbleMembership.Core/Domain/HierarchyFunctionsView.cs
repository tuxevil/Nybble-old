using System;
using System.Web.Security;
using System.Collections.Generic;
using System.Text;
using ProjectBase.Data;

namespace NybbleMembership.Core.Domain
{
    public class HierarchyFunctionsView : AuditableEntity<int>
    {
        private IList<Function> parents;
        private IList<Function> childs;
        
        
        public virtual IList<Function> ParentFunction
        {
            get
            {
                return parents;
            }
            set 
            {
                parents = value;
            }
        }

        public virtual IList<Function> ChildFunction
        {
            get
            {
                return childs;
            }
            set
            {
                childs = value;
            }
        }
    }
}
