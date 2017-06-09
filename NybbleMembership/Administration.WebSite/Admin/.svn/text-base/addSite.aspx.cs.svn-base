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
    public partial class addSite : Page
    {
        #region Properties

        private int SiteId
        {   get
            {   if (ViewState["SiteId"] == null)
                {
                    int id = 0;
                    ViewState["SiteId"] = id;
                }

                return (int)ViewState["SiteId"];
            }
            set
            {
                int id = value;
                ViewState["SiteId"] = id;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                SiteId = Convert.ToInt32(Request.QueryString["id"]);
                LoadAll(SiteId);
            }
        }

        private void LoadAll(int id)
        {
            lblTitle.Text = "Creación de Sitio";
            if (id != 0)
            {
                Site s = ControllerManager.Site.GetById(id);

                txtName.Text = s.Name;
                txtCode.Text = s.Code;
                txtCode.Enabled = false;
                lblTitle.Text = "Edición Sitio: " + s.Name;
            }
            LoadUsers(id);
        }
        private void LoadUsers(int id)
        {
            ltUsers.Clear();

            IList<UserMember> lstUM = ControllerManager.UserMember.List();
            bool wasAdded = false;
            Site s = null;
            IList<UserMember> lstAddedUM = null;
            if (id != 0)
            {
                s = ControllerManager.Site.GetById(id);
                if (s.Users != null)
                {
                    lstAddedUM = s.Users;
                }
            }
            foreach (UserMember u in lstUM)
            {
                if (s != null)
                {
                    foreach (UserMember addedUM in lstAddedUM)
                    {
                        wasAdded = false;
                        if (addedUM.ID == u.ID)
                        {
                            wasAdded = true;
                            ltUsers.AddDestinationItem(new ListItem(u.Email, u.ID.ToString()));
                            break;
                        }
                    }
                    if (!wasAdded)
                        ltUsers.AddItem(new ListItem(u.Email, u.ID.ToString()));
                }
                else
                    ltUsers.AddItem(new ListItem(u.Email, u.ID.ToString()));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            IList<UserMember> lstUsers = new List<UserMember>();

            foreach (ListItem liUM in ltUsers.DestinationItems)
                lstUsers.Add(new UserMember(new Guid(liUM.Value)));
                if (SiteId != 0)
                {
                    try
                    {
                        ControllerManager.Site.Edit(SiteId, txtName.Text, txtCode.Text, lstUsers);
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }
                else
                    ControllerManager.Site.Create(txtName.Text, txtCode.Text, lstUsers);

                Response.Redirect("Sites.aspx");
            
        }
    }
}
