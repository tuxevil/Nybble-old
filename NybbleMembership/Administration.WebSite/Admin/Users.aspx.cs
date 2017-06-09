using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NybbleMembership.Core.Domain;
using NybbleMembership.Business;
using ProjectBase.Utils.Email;
using System.IO;

namespace Administration.WebSite.Admin

{
    public partial class Users : Page
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
            grdUsers.DataSource = ControllerManager.UserMember.List();
            grdUsers.DataBind();
        }

        protected void grdUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUsers.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void DoAction(Guid id, string action)
        {
            MembershipUser mu = Membership.GetUser(id);

            
                if (action == "Erase")
                    ControllerManager.UserMember.Erase(id);
               else
                    if (action == "ResetPass")
                    {
                      string password =  mu.ResetPassword();
                        SendPasswordMail(mu, password);
                        lblPass.Visible = true;
                    }
                    else
                        if (action == "Unlock")
                            mu.UnlockUser();
                        else
                            if (action == "Select")
                                Response.Redirect("edituser.aspx?id=" + id);

            LoadGrid();
                
        }

        private void SendPasswordMail(MembershipUser mu, string password)
        {
            WebMailing w = new WebMailing();
            string body = File.ReadAllText(Path.Combine(HttpContext.Current.Server.MapPath(NybbleMembership.Core.Configuration.MailTemplatePath), "newpassword.htm"));
            body = body.Replace("[NEW_PASSWORD]", password);
            body = body.Replace("[USUARIO]", mu.UserName);
            w.SendMail(mu.Email,"Cambio de Contraseña", "Cambio de Contraseña", body, false);


            //SmtpClient mailclient = new SmtpClient();
            
            //MailMessage mm = new MailMessage();
            //mm.To.Add(mu.Email);
            //mm.Subject = "Nueva Contraseña";
            //mm.Body ="Su nueva contraseña es: " + password;

            //mailclient.Send(mm);
        }

        protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (grdUsers.DataKeys != null && e.CommandName != "Page")
            {
                Guid id = new Guid((grdUsers.DataKeys[Convert.ToInt32(e.CommandArgument)].Value).ToString());
                DoAction(id, e.CommandName);
            }

        }

        protected void grdUsers_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.DataItem != null)
            {
                UserMember u = e.Row.DataItem as UserMember;
                e.Row.Cells[2].Text = u.UserProfile.FirstName;
                e.Row.Cells[3].Text = u.UserProfile.LastName;
            }
        }

    }
}
