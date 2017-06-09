using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Administration.WebSite
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Leo2().Capos();
        }
    }

    public class Leo2 : Leo
    {
        
    }

    public abstract class Leo
    {
        public virtual string Capos()
        {
            MethodBase mb = MethodBase.GetCurrentMethod();
            return mb.DeclaringType.FullName;
            return mb.Name;
        }
    }
}
