using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Administration.WebSite
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_LoginError1(object sender, EventArgs e)
        {
            Flash.Attributes["class"] = "flash_alert";
            lblInfo.Text = "El usuario/contraseña ingresado es incorrecto";
        }

        protected void Login1_LoggingIn(object sender, LoginCancelEventArgs e)
        {

        }

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {
            //MembershipHelper.UpdateAuthenticationTicketForUser(Login1.UserName);
            //Response.Redirect(FormsAuthentication.GetRedirectUrl(Login1.UserName, true));
        }

    }
}
