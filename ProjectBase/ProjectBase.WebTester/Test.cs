using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ProjectBase.Data;

namespace ProjectBase.WebTester
{
    public class Test : AuditableEntity<int>
    {
        public Test(int id) { this.id = id;  }
        public Test() { }

        private int version;
        private string testProperty;

        [DomainSignature]
        public virtual string TestProperty
        {
            get { return testProperty; }
            set { testProperty = value; }
        }

        public virtual int Version
        {
            get { return version; }
            set { version = value; }
        }
    }
}
