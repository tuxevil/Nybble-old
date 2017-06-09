using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ProjectBase.Utils.Email;
using ProjectBase.Utils.MailSender;

namespace ProjectBase.WebTester
{
    public partial class MailTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            byte[] response;
            String filePath = Server.MapPath("/temp/") + "1.pdf";
            response = File.ReadAllBytes(filePath);
            
            WebMailing wm = new WebMailing();
            wm.SendMail("sebastian.real@nybblegroup.com","title", "subject", "body", false);
            wm.SendMail("sebastian.real@nybblegroup.com", "title", "subject", "body", false, "cotizacion.pdf", response);

            //MailSender.Mail.Send(Convert.ToInt32(DropDownList1.SelectedValue), TextBox1.Text, TextBox2.Text, TextBox3.Text);
        }
    }
}
