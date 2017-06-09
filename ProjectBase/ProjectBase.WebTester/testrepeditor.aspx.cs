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
    public partial class testrepeditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ArrayList lst = new ArrayList();
                lst.Add(1);
                lst.Add(2);
                lst.Add(3);
                lst.Add(4);
                lst.Add(5);

                this.rptDistributorContacts.DataSource = lst;
                this.rptDistributorContacts.DataBind();
            }
        }

        protected void rptDistributorContacts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                editor dceEdit = (e.Item.FindControl("DistributorContactEditor1") as editor);
                dceEdit.Email = "Caposs";
                dceEdit.Number = Convert.ToInt32(e.Item.DataItem);
                dceEdit.DataBind();
            }
        }
    }
}
