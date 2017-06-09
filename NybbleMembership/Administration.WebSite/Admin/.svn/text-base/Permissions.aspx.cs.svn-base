using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NybbleMembership.Core.Domain;
using NybbleMembership.Business;

namespace Administration.WebSite.Admin
{
    public partial class Permissions : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadGrid();
            }
        }

        public void LoadGrid()
        {
            grdPermissions.DataSource = ControllerManager.Permission.List();
            grdPermissions.DataBind();
        }

        protected void grdPermissions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPermissions.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void DoAction(int id, string action)
        {
            if (action == "Erase")
                ControllerManager.Permission.Erase(id);
            else
                if (action == "Select")
                    Response.Redirect("addpermission.aspx?id=" + id);
                
            LoadGrid();

        }

        protected void grdPermissions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (grdPermissions.DataKeys != null && e.CommandName != "Page")
            {
                int id = Convert.ToInt32(grdPermissions.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
                DoAction(id, e.CommandName);
            }
        }
    }
}
