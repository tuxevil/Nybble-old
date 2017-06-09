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
    public partial class addRol : Page
    {
        #region Properties

        private int RolId
        {   get
            {   if (ViewState["RolId"] == null)
                {
                    int id = 0;
                    ViewState["RolId"] = id;
                }

                return (int)ViewState["RolId"];
            }
            set
            {
                int id = value;
                ViewState["RolId"] = id;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                RolId = Convert.ToInt32(Request.QueryString["id"]);
                LoadAll(RolId);
            }
        }

        private void LoadAll(int id)
        {
            ddlSites.DataSource = ControllerManager.Site.List();
            ddlSites.DataTextField = "Name";
            ddlSites.DataValueField = "ID";
            ddlSites.DataBind();

            lblTitle.Text = "Creación de Rol";
            if (id != 0)
            {
                Rol r = ControllerManager.Rol.GetById(id);

                txtName.Text = r.Name;
                txtDescription.Text = r.Description;
                ddlSites.SelectedValue = ddlSites.Items.FindByValue(r.Site.ID.ToString()).Value;
                chkAdmin.Checked = r.IsAdministrator;
                lblTitle.Text = "Edición Rol: " + r.Name;

            }
            LoadLists(id);
        }

        protected void LoadLists(int id)
        {
            ltFunctions.Clear();
            ltUsers.Clear();

            Site s = ControllerManager.Site.GetById(Convert.ToInt32(ddlSites.SelectedValue));

            IList<Function> lstF = ControllerManager.Function.ListBySite(s);
            IList<UserMember> lstUM = ControllerManager.UserMember.ListBySite(s);
            bool wasAdded = false;
            Rol r = null;
            IList<Function> lstAddedF = null;
            IList<UserMember> lstAddedUM = null;
            if (id != 0)
            {
                r = ControllerManager.Rol.GetById(id);
                if (r.Functions != null)
                {
                    lstAddedF = r.Functions;
                }
                if (r.Users != null)
                {
                    lstAddedUM = r.Users;
                }
            }
            foreach (Function fn in lstF)
            {
                if (r != null)
                {
                    foreach (Function addedF in lstAddedF)
                    {
                        wasAdded = false;
                        if (addedF.ID == fn.ID)
                        {
                            wasAdded = true;
                            ltFunctions.AddDestinationItem(new ListItem(fn.Name, fn.ID.ToString()));
                            break;
                        }
                    }
                    if (!wasAdded)
                        ltFunctions.AddItem(new ListItem(fn.Name, fn.ID.ToString()));
                }
                else
                    ltFunctions.AddItem(new ListItem(fn.Name, fn.ID.ToString()));
            }

            wasAdded = false;
            foreach (UserMember u in lstUM)
            {
                if (r != null)
                {
                    foreach (UserMember addedUM in lstAddedUM)
                    {
                        wasAdded = false;
                        if (addedUM.ID == u.ID)
                        {
                            wasAdded = true;
                            ltUsers.AddDestinationItem(new ListItem(u.UserName, u.ID.ToString()));
                            break;
                        }
                    }
                    if (!wasAdded)
                        ltUsers.AddItem(new ListItem(u.UserName, u.ID.ToString()));
                }
                else
                    ltUsers.AddItem(new ListItem(u.UserName, u.ID.ToString()));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            IList<UserMember> lstUsers = new List<UserMember>();

            foreach (ListItem liUM in ltUsers.DestinationItems)
                lstUsers.Add(new UserMember(new Guid(liUM.Value)));

            IList<Function> lstFunctions = new List<Function>();

            foreach (ListItem liF in ltFunctions.DestinationItems)
                lstFunctions.Add(new Function(Convert.ToInt32(liF.Value)));

                if (RolId != 0)
                {
                    try
                    {
                        ControllerManager.Rol.Edit(RolId, txtName.Text, txtDescription.Text,
                                                   Convert.ToInt32(ddlSites.SelectedValue), chkAdmin.Checked, lstUsers,
                                                   lstFunctions);
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }
                else
                    ControllerManager.Rol.Create(txtName.Text, txtDescription.Text,
                                                 Convert.ToInt32(ddlSites.SelectedValue), chkAdmin.Checked, lstUsers,
                                                 lstFunctions);
              Response.Redirect("roles.aspx");
           
        }

        protected void ddlSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLists(RolId);
        }
    }
}
