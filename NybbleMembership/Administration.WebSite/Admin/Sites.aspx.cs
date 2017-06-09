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
    public partial class Sites : Page
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
            grdSites.DataSource = ControllerManager.Site.List();
            grdSites.DataBind();
        }

        public void Erase (int id)
        {
            if (!ControllerManager.Site.Erase(id))
                lblError.Visible = true;
            
            LoadGrid();
        }

        protected void grdSites_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSites.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void grdSites_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (grdSites.DataKeys != null)
            {
                int id = Convert.ToInt32(grdSites.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

                if (e.CommandName == "Select")
                    Response.Redirect("addSite.aspx?id=" + id);
                if (e.CommandName == "Erase")
                    Erase(id);

            }
           
        }

    }
}
