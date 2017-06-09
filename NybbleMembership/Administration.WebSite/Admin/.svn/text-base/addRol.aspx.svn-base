<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/base.Master" CodeBehind="addRol.aspx.cs" Inherits="Administration.WebSite.Admin.addRol" Title="Administración de Roles"%>
<%@ Register Src="../ctrl/ListTransfer.ascx" TagName="ListTransfer" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cplMain" Runat="Server">
    <div class="page_header">
        <h1>
            <asp:Literal runat="server" ID="lblTitle"></asp:Literal>
        </h1>
    </div>

    <div class="form">
      <fieldset>
        <div>
            <label>Nombre</label>
            <asp:TextBox ID="txtName" runat="server" Width="200px" MaxLength="256"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="*" ControlToValidate="txtName"></asp:RequiredFieldValidator>
        </div>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="Dynamic" ControlToValidate="txtName" runat="server" ValidationExpression="^(a-z|A-Z|0-9)*[^#$%^&()'<>!|°¬@=?¡¿{}¨]*$" ErrorMessage="Por favor, no ingrese caracteres especiales."></asp:RegularExpressionValidator>
        <div>
            <label>Sitio</label>
            <asp:DropDownList ID="ddlSites" Width="205px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSites_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div>
            <label>Descripción</label>
            <asp:TextBox ID="txtDescription" runat="server" Width="200px" MaxLength="256"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="*" ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
        </div>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ControlToValidate="txtDescription" runat="server" ValidationExpression="^(a-z|A-Z|0-9)*[^#$%^&()'<>!|°¬@=?¡¿{}¨]*$" ErrorMessage="Por favor, no ingrese caracteres especiales."></asp:RegularExpressionValidator>
        <div>
            <label>Administrador</label>
            <asp:CheckBox ID="chkAdmin" Checked="false" runat="server" Width="110px" />
        </div>
        
        <div>
        <table cellspacing=0 cellpadding=0 border=0 width="100%">
                <tr>
                    <td width="10%"><label>Usuarios</label></td>
                    <td><uc1:ListTransfer id="ltUsers" runat="server"></uc1:ListTransfer></td>
                </tr>
        </table>
        </div>
        <div>
        <table cellspacing=0 cellpadding=0 border=0 width="100%">
                <tr>
                    <td width="10%"><label>Funciones</label></td>
                    <td><uc1:ListTransfer id="ltFunctions" runat="server"></uc1:ListTransfer></td>
                </tr>
        </table>
        </div>
      </fieldset>  
  
    <center><asp:Button ID="btnSave" runat="server" CssClass="button" Text="Guardar" OnClick="btnSave_Click" />
       <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="Roles.aspx">Volver</asp:HyperLink>
    </center>
</div>
</asp:Content>
