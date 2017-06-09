using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace NybbleMembership.TestWebSite
{
    public class ControllerManager_Alt
    {
        public static NumerosController Numeros
        {
            get { return new NumerosController("~/WebNHibernateTest.config"); }
        }
    }
}
