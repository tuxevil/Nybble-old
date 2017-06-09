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
using NybbleMembership.Core;

namespace Administration.WebSite.Admin
{
    public partial class addpermission : Page
    {
        #region Properties

        private int PermissionId
        {   get
            {
                if (ViewState["PermissionId"] == null)
                {
                    int id = 0;
                    ViewState["PermissionId"] = id;
                }

                return (int)ViewState["PermissionId"];
            }
            set
            {
                int id = value;
                ViewState["PermissionId"] = id;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InvisibleAll();
            if(!IsPostBack)
            {
                PermissionId = Convert.ToInt32(Request.QueryString["id"]);
                LoadAll(PermissionId);
                
            }
        }

        private void LoadAll(int id)
        {
            ddlAction.DataSource = Enum.GetNames(typeof(PermissionAction));
            ddlAction.DataBind();

            lblTitle.Text = "Creación de Permiso";
            if (id != 0)
            {
               Permission p = ControllerManager.Permission.GetById(id);
               txtName.Text = p.Name;
               txtCode.Text = p.Code;
               txtDescription.Text = p.Description;
               txtCode.Enabled = false; 
               ddlAction.SelectedValue = ddlAction.Items.FindByValue(p.PermissionAction.ToString()).Value;
               

                if (p is PagePermission)
                {
                    PagePermission pp = (p as PagePermission);   
                    ddlPermission.SelectedValue = "Page";
                    MakeVisible(ddlPermission.SelectedValue);

                    txtPermission1.Text = pp.PageName;
                    txtPermission2.Text = pp.FolderName;
                }
                else
                    if (p is MethodPermission)
                    {
                        MethodPermission mp = (p as MethodPermission);
                        ddlPermission.SelectedValue = "Method";
                        MakeVisible(ddlPermission.SelectedValue);

                        txtPermission1.Text = mp.MethodName;
                        txtPermission2.Text = mp.ClassName;
                    }
                    else
                        if (p is EntityPermission)
                        {
                            EntityPermission ep = (p as EntityPermission);
                            ddlPermission.SelectedValue = "Entity";
                            MakeVisible(ddlPermission.SelectedValue);

                            txtPermission1.Text = ep.Identifier;
                            txtPermission2.Text = ep.ClassName;
                        }
                        else
                            if (p is WebControlPermission)
                            {
                                WebControlPermission wp = (p as WebControlPermission);
                                ddlPermission.SelectedValue = "Web";
                                MakeVisible(ddlPermission.SelectedValue);

                                txtPermission1.Text = wp.RelativePath;
                                txtPermission2.Text = wp.ControlIdentifier;
                            }
                            else
                                if (p is ExecutePermission)
                                {
                                    ExecutePermission exp = (p as ExecutePermission);
                                    ddlPermission.SelectedValue = "Execute";
                                    MakeVisible(ddlPermission.SelectedValue);

                                    txtPermission1.Text = exp.KeyIdentifier;
                                    txtPermission2.Text = exp.ClassName;
                                }
                lblTitle.Text = "Edición " + ddlPermission.SelectedItem;
            }
            else
            {
                lblPermission.Visible = true;
                ddlPermission.Visible = true;
                lblFolderName.Visible = true;
                lblPageName.Visible = true;
            }
        }

        protected void ddlPermission_SelectedIndexChanged(object sender, EventArgs e)
        {
            MakeVisible(ddlPermission.SelectedValue);
        }

        private void MakeVisible(string permission)
        {
            switch (permission)
            {
                case "Page":
                    rfvPage.Enabled = true;
                    lblFolderName.Visible = true;
                    lblPageName.Visible = true;
                    break;
                case "Method":
                    rfvPage.Enabled = true;
                    lblMethodName.Visible = true;
                    lblClassName.Visible = true;
                    break;
                case "Entity":
                    rfvPage.Enabled = false;
                    lblIdentifier.Visible = true;
                    lblClassName.Visible = true;
                    break;
                case "Web":
                    rfvPage.Enabled = false;
                    lblRelative.Visible = true;
                    lblControlIdentifier.Visible = true;
                    break;
                case "Execute":
                    rfvPage.Enabled = false;
                    lblIdentifier.Visible = true;
                    lblClassName.Visible = true;
                    break;
            }
        }

        private void InvisibleAll()
        {
            lblFolderName.Visible = false;
            lblPageName.Visible = false;
            lblMethodName.Visible = false;
            lblClassName.Visible = false;
            lblIdentifier.Visible = false;
            lblRelative.Visible = false;
            lblControlIdentifier.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            if (ddlPermission.SelectedValue == "Page")
            {
                if (PermissionId != 0)
                {
                    try
                    {
                        ControllerManager.PagePermission.Edit(PermissionId, txtName.Text, txtCode.Text,
                                                              txtDescription.Text,
                                                              ReturnAction(ddlAction.SelectedValue), txtPermission1.Text,
                                                              txtPermission2.Text);
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }
                else
                    ControllerManager.PagePermission.Create(txtName.Text, txtCode.Text, txtDescription.Text,
                                                            ReturnAction(ddlAction.SelectedValue), txtPermission1.Text,
                                                            txtPermission2.Text);

            }
            else
                if (ddlPermission.SelectedValue == "Method")
                {
                    if (PermissionId != 0)
                    {
                        try
                        {
                            ControllerManager.MethodPermission.Edit(PermissionId, txtName.Text, txtCode.Text,
                                                                  txtDescription.Text,
                                                                  ReturnAction(ddlAction.SelectedValue), txtPermission1.Text,
                                                                  txtPermission2.Text);
                        }
                        catch (Exception ex)
                        {
                            return;
                        }
                    }
                    else
                        ControllerManager.MethodPermission.Create(txtName.Text, txtCode.Text, txtDescription.Text,
                                                                ReturnAction(ddlAction.SelectedValue),txtPermission1.Text,
                                                                txtPermission2.Text);

                }
                else
                    if (ddlPermission.SelectedValue == "Entity")
                    {
                        if (PermissionId != 0)
                        {
                            try
                            {
                                ControllerManager.EntityPermission.Edit(PermissionId, txtName.Text, txtCode.Text,
                                                                      txtDescription.Text,
                                                                      ReturnAction(ddlAction.SelectedValue), txtPermission1.Text,
                                                                      txtPermission2.Text);
                            }
                            catch (Exception ex)
                            {
                                return;
                            }
                        }
                        else
                            ControllerManager.EntityPermission.Create(txtName.Text, txtCode.Text, txtDescription.Text,
                                                                    ReturnAction(ddlAction.SelectedValue), txtPermission1.Text,
                                                                    txtPermission2.Text);

                    }
                    else
                        if (ddlPermission.SelectedValue == "Web")
                        {
                            if (PermissionId != 0)
                            {
                                try
                                {
                                    ControllerManager.WebControlPermission.Edit(PermissionId, txtName.Text, txtCode.Text,
                                                                          txtDescription.Text,
                                                                          ReturnAction(ddlAction.SelectedValue), txtPermission1.Text,
                                                                          txtPermission2.Text);
                                }
                                catch (Exception ex)
                                {
                                    return;
                                }
                            }
                            else
                                ControllerManager.WebControlPermission.Create(txtName.Text, txtCode.Text, txtDescription.Text,
                                                                        ReturnAction(ddlAction.SelectedValue), txtPermission1.Text,
                                                                        txtPermission2.Text);
                        }
                        else
                            if (ddlPermission.SelectedValue == "Execute")
                            {
                                if (PermissionId != 0)
                                {
                                    try
                                    {
                                        ControllerManager.ExecutePermission.Edit(PermissionId, txtName.Text, txtCode.Text,
                                                                          txtDescription.Text,
                                                                          ReturnAction(ddlAction.SelectedValue), txtPermission1.Text,
                                                                          txtPermission2.Text);
                                    }
                                    catch
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    ControllerManager.ExecutePermission.Create(txtName.Text, txtCode.Text, txtDescription.Text,
                                                                        ReturnAction(ddlAction.SelectedValue), txtPermission1.Text,
                                                                        txtPermission2.Text);
                                }
                            }

            Response.Redirect("Permissions.aspx");
            
            
        }

        private PermissionAction ReturnAction(string value)
        {
            if (value == PermissionAction.View.ToString())
                return PermissionAction.View;
            else
                if (value == PermissionAction.Create.ToString())
                    return PermissionAction.Create;
                else
                    return PermissionAction.Remove;
        }
    }
}
