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

namespace ProjectBase.WebTester
{
    public partial class editor : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string Email;
        public int Number;

        public override void DataBind()
        {
            this.txtMail.Text = Email;
            this.txtMail.Enabled = true;
            this.txtMail.IsRequired = true;
            this.txtMail.ValidationGroup = "Pepe" + Number;

            this.btnTest.ValidationGroup = "Pepe" + Number;
        }
    }
}