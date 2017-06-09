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
    public partial class addUser : Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {   
                //LoadAll();
            }
        }

        //private void LoadAll()
        //{
        //    lsbRoles.DataSource = ControllerManager.Rol.List();
        //    lsbRoles.DataTextField = "Name";
        //    lsbRoles.DataValueField = "ID";
        //    lsbRoles.DataBind();
        //    lsbRoles.SelectedIndex = 0;

        //}

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {
            
                MembershipUser mu = Membership.GetUser(CreateUserWizard1.UserName);
            
                ControllerManager.UserProfile.Create((Guid)mu.ProviderUserKey, (createUser.ContentTemplateContainer.FindControl("txtFirstName") as TextBox).Text, (createUser.ContentTemplateContainer.FindControl("txtLastName") as TextBox).Text);
        }

        protected void CreateUserWizard1_ContinueButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("Users.aspx");
        }






    }
}
