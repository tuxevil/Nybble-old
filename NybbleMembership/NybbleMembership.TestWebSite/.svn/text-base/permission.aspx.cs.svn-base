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
using NybbleMembership.Core;

namespace NybbleMembership.TestWebSite
{
    public partial class permission : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                Numeros num = new Numeros();
                DropDownList3.DataSource = PermissionManager.GetEntitysForAction(num.GetType(), PermissionAction.Create);
                DropDownList3.DataBind();

                DropDownList2.DataSource = PermissionManager.GetEntitysForAction(num.GetType(), PermissionAction.Create);
                DropDownList2.DataBind();
                
                ListBox1.DataSource = MembershipManager.ListBySite();
                ListBox1.DataTextField = "UserName";
                ListBox1.DataValueField = "ID";
                ListBox1.DataBind();

                DropDownList1.DataSource = MembershipManager.ListBySite();
                DropDownList1.DataTextField = "UserName";
                DropDownList1.DataValueField = "ID";
                DropDownList1.DataBind();
            }
            

        }

        protected void btnLista_Click(object sender, EventArgs e)
        {
            Numeros num = ControllerManager_Alt.Numeros.GetById(Convert.ToInt32(DropDownList3.SelectedValue));

            foreach (ListItem lip in ListBox1.Items)
                if (lip.Selected)
                {
                    PermissionManager.AddEntityPermision(num.GetType(), DropDownList3.SelectedItem.Text, lip.Text);
                }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Numeros num = new Numeros();
            IList list;
            if(!ControllerManager.EntityPermission.ListIdentifiers(num.GetType(), Membership.GetUser(DropDownList1.SelectedItem.Text).ProviderUserKey, (PermissionAction)3, out list))
            {
                if (list == null)
                    return;
                GridView1.DataSource = list;
                GridView1.DataBind();    
            }
            else
            {
                GridView1.DataSource = PermissionManager.GetEntitysForAction(num.GetType(), PermissionAction.Create);
                GridView1.DataBind(); 
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Numeros num = new Numeros();
            IList list = PermissionManager.GetUsersForEntity(num, DropDownList2.SelectedItem.Text, PermissionAction.Create);
            if(list == null)
                return;
            GridView1.DataSource = list;
            GridView1.DataBind();
        }

        protected void btnborrar_Click(object sender, EventArgs e)
        {
            //PermissionManager.RemovePermission(txtclass.Text, txtidentifier.Text, txtuser.Text);
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Numeros num = ControllerManager_Alt.Numeros.GetById(Convert.ToInt32(DropDownList3.SelectedValue));
            IList list = PermissionManager.GetPermissionIdentifiers(num.GetType(), PermissionAction.Create);
            if (list != null)
            {
                GridView3.DataSource = list;
                GridView3.DataBind();
            }
        }   
    }
}
