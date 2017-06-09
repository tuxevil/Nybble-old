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
using NybbleMembership.Business;
using System.Reflection;
using System.Web.Caching;

namespace NybbleMembership.TestWebSite
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            PermissionManager.Validate(Page);

            DropDownList2.DataSource = new NumerosController("~/WebNHibernateTest.config").GetNumbers();
            DropDownList2.DataBind();

            DropDownList2.SelectedValue = "5";

            txtresult.Enabled = PermissionManager.Check(txtresult);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.Check(sender))
                return;

            Numeros num = new Numeros();
            if(PermissionManager.Check(num))
            {
                num.Num = Convert.ToInt32(Label1.Text);
                Label4.Text = num.Num.ToString();
            }

            double primero = Convert.ToDouble(Label1.Text);
            double segundo = Convert.ToDouble(Label3.Text);
            double resultado = 0;
            switch (Label2.Text)
            {
                case "+":
                    resultado = primero + segundo;
                    break;
                case "-":
                    resultado = primero - segundo;
                    break;
                case "*":
                    resultado = primero * segundo;
                    break;
                case "/":
                    resultado = primero / segundo;
                    break;
            }
            if (!string.IsNullOrEmpty(txtresult.Text))
                Label4.Text = txtresult.Text;
            else
                Label4.Text = resultado.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //if (PermissionManager.Check(TextBox1))
            {
                Label1.Text = TextBox1.Text;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //if (PermissionManager.Check(DropDownList1))
            {
                Label2.Text = DropDownList1.SelectedValue;
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //if (PermissionManager.Check(TextBox2))
            {
                Label3.Text = DropDownList2.SelectedValue;
                //Label3.Visible = PermissionManager.Check(Label3);
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            if (PermissionManager.Check(txtresult))
            {
                Label4.Text = txtresult.Text;
                //Label4.Visible = PermissionManager.Check(Label4);
            }
        }
    }
}
