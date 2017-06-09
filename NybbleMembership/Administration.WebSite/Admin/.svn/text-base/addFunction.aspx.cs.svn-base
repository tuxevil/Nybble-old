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
    public partial class addFunction : Page
    {
        #region Properties

        private int FunctionId
        {   get
            {
                if (ViewState["FunctionId"] == null)
                {
                    int id = 0;
                    ViewState["FunctionId"] = id;
                }

                return (int)ViewState["FunctionId"];
            }
            set
            {
                int id = value;
                ViewState["FunctionId"] = id;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                FunctionId = Convert.ToInt32(Request.QueryString["id"]);
                LoadAll(FunctionId);
            }
        }

        private void LoadAll(int id)
        {
            LoadPermission();
            
            ddlSites.DataSource = ControllerManager.Site.List();
            ddlSites.DataTextField = "Name";
            ddlSites.DataValueField = "ID";
            ddlSites.DataBind();

            dllParent.DataSource = ControllerManager.Function.List();
            dllParent.DataTextField = "Name";
            dllParent.DataValueField = "ID";
            dllParent.DataBind();
            dllParent.Items.Insert(0, new ListItem("--Padre--", "0"));

            lblTitle.Text = "Creación de Función";
            if (id != 0)
            {
                Function f = ControllerManager.Function.GetById(id);
                
                txtName.Text = f.Name;
                txtDescription.Text = f.Description;
                lblTitle.Text = "Edición Función: " + f.Name;
                ddlSites.SelectedValue = ddlSites.Items.FindByValue(f.Site.ID.ToString()).Value;
                if (f.Parent != null)
                    dllParent.SelectedValue = dllParent.Items.FindByValue(f.Parent.ID.ToString()).Value;
            }
        }

        protected void LoadPermission()
        {
            ListTransfer1.Clear();

            IList<Permission> lstP = ControllerManager.Permission.List();
            bool wasAdded = false;
            Function f = null;
            IList<Permission> lstAddedP = null;
            if (FunctionId != 0)
            { 
                f = ControllerManager.Function.GetById(FunctionId);
                if (f.Permissions != null)
                {
                    lstAddedP = f.Permissions;
                }
            }
            foreach (Permission p in lstP)
            {
                if (f != null)
                {
                    foreach (Permission addedP in lstAddedP)
                    {
                        wasAdded = false;
                        if (addedP.ID == p.ID)
                        {
                            wasAdded = true;
                            ListTransfer1.AddDestinationItem(new ListItem(p.Name, p.ID.ToString()));
                            break;
                        }
                    }
                    if (!wasAdded)
                        ListTransfer1.AddItem(new ListItem(p.Name, p.ID.ToString()));
                }
                else
                    ListTransfer1.AddItem(new ListItem(p.Name, p.ID.ToString()));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            IList<Permission> lstPermissions= new List<Permission>();

            foreach(ListItem lipt in ListTransfer1.DestinationItems)
                lstPermissions.Add(new Permission(Convert.ToInt32(lipt.Value)));

                if (FunctionId != 0)
                {
                    try
                    {
                        ControllerManager.Function.Edit(FunctionId, txtName.Text, txtDescription.Text,
                                                   Convert.ToInt32(ddlSites.SelectedValue), Convert.ToInt32(dllParent.SelectedValue), lstPermissions);
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }
                else
                    ControllerManager.Function.Create(txtName.Text, txtDescription.Text,
                                                   Convert.ToInt32(ddlSites.SelectedValue), Convert.ToInt32(dllParent.SelectedValue), lstPermissions);
                Response.Redirect("Functions.aspx");
            
        }
    }
}
