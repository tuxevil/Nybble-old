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
    public partial class Roles : Page
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
            
            grdRoles.DataSource = ControllerManager.Rol.List();
            grdRoles.DataBind();
        }

        public void Erase(int id)
        {
            if (!ControllerManager.Rol.Erase(id))
                lblError.Visible = true;
            LoadGrid();
        }

        protected void grdRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRoles.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void grdRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (grdRoles.DataKeys != null && e.CommandName != "Page")
            {
                int id = Convert.ToInt32(grdRoles.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
                
                if (e.CommandName == "Select")
                    Response.Redirect("addRol.aspx?id=" + id);
                if (e.CommandName == "Erase")
                    Erase(id);

            }
        }

        


    }
}
