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

namespace NybbleMembership.TestWebSite
{
    public class Numeros : AuditableEntity<int>
    {
        private int num;

        public virtual int Num
        {
            get { return num; }
            set { num = value; }
        }
    }
}