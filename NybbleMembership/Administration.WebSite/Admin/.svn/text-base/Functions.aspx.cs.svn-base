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
    public partial class Functions : Page
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
            grdFunctions.DataSource = ControllerManager.Function.List();
            grdFunctions.DataBind();
        }

        protected void grdFunctions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFunctions.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void DoAction(int id, string action)
        {
            if (action == "Erase")
                Erase(id);
            else
                if (action == "Select")
                    Response.Redirect("addFunction.aspx?id=" + id);
       }

        protected void Erase (int id)
        {

            if (!ControllerManager.Function.Erase(id))
                lblError.Visible = true;

            LoadGrid();
        }

        protected void grdFunctions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (grdFunctions.DataKeys != null && e.CommandName != "Page")
            {
                int id = Convert.ToInt32(grdFunctions.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
                DoAction(id, e.CommandName);
            }
        }
    }
}
