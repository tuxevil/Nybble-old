using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace ProjectBase.WebTester
{
    public partial class createeventsource : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Create the source, if it does not already exist.
            if (!EventLog.SourceExists("StockForecast"))
            {
                EventLog.CreateEventSource("StockForecast", "StockForecast");
            }

        }
    }
}
