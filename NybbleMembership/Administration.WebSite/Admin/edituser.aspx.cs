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
    public partial class edituser : Page
    {
        #region Properties

        private Guid UserId
        {   get
            {
                if (ViewState["UserId"] == null)
                {
                    Guid id = Guid.Empty;
                    ViewState["UserId"] = id;
                }

                return (Guid)ViewState["UserId"];
            }
            set
            {
                Guid id = value;
                ViewState["UserId"] = id;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                UserId = new Guid(Request.QueryString["id"]);
                
                lblTitle.Text = "Edicion de Usuario";
                if (UserId != Guid.Empty)
                {
                    UserMember um = ControllerManager.UserMember.GetById(UserId);

                    if (um != null)
                    {
                        if (um.UserProfile != null)
                        {
                            txtName.Text = um.UserProfile.FirstName;
                            txtLastName.Text = um.UserProfile.LastName;
                        }
                        txtMail.Text = um.Email;
                        lblUserName.Text = um.UserName;
                    }
                }
            }
        }
       
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            ControllerManager.UserMember.Modify(UserId, txtName.Text, txtLastName.Text, txtMail.Text);
            Response.Redirect("Users.aspx");
        }
    }
}
