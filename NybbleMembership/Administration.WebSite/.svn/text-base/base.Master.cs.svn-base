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
using NybbleMembership.Core.Domain;
using NybbleMembership.Business;

namespace Administration.WebSite
{
    public partial class _base : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               Assembly a = Assembly.GetExecutingAssembly();
                AssemblyName name = a.GetName();
                lblVersion.Text = "v" + name.Version.Major + "." + name.Version.Minor + "." + name.Version.Build;

                MembershipUser m = Membership.GetUser();
                if (m != null)
                {
                   if(!ControllerManager.Rol.IsAdministrator(m.ProviderUserKey, NybbleMembership.Core.Configuration.SiteCode))
                       Response.Redirect("~/login.aspx");
                }
                else
                    Response.Redirect("~/login.aspx");

            }
        }

        protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
        {
            Response.Redirect("~/login.aspx");
        }
    }
}
