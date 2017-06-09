using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ProjectBase.MailSender
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            string error = Server.GetLastError().ToString();
            log4net.LogManager.GetLogger("MailSender").Error(Server.GetLastError().Message, Server.GetLastError().GetBaseException());
        }
    }
}