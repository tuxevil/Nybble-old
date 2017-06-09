<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/base.Master" CodeBehind="edituser.aspx.cs" Inherits="Administration.WebSite.Admin.edituser" Title="Edición de Usuario"%>
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
        <div><label>Usuario</label>
                <asp:Label ID="lblUserName" runat="server" Width="200px" MaxLength="256"></asp:Label>
            </div>
            <div><label>Nombre</label>
                <asp:TextBox ID="txtName" runat="server" Width="200px" MaxLength="64"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="*" ValidationGroup="valFields" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="Dynamic" ControlToValidate="txtName" ValidationGroup="valFields" runat="server" ValidationExpression="^(a-z|A-Z|0-9)*[^#$%^&()'<>!|°¬@=?¡¿{}¨]*$" ErrorMessage="Por favor, no ingrese caracteres especiales."></asp:RegularExpressionValidator>
            </div>
            <div><label>Apellido</label>
                <asp:TextBox ID="txtLastName" runat="server" Width="200px" MaxLength="64"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ValidationGroup="valFields" ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" ControlToValidate="txtLastName" ValidationGroup="valFields" runat="server" ValidationExpression="^(a-z|A-Z|0-9)*[^#$%^&()'<>!|°¬@=?¡¿{}¨]*$" ErrorMessage="Por favor, no ingrese caracteres especiales."></asp:RegularExpressionValidator>
            </div>
            <div><label>E-Mail</label>
                <asp:TextBox ID="txtMail" runat="server" MaxLength="50" TabIndex="3"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revMail" Display="Dynamic" runat="server" ErrorMessage="Ingrese correctamente el mail." ControlToValidate="txtMail" ValidationGroup="valFields" ValidationExpression="[\w-]+@([\w-]+\.)+[\w-]+"></asp:RegularExpressionValidator>
            </div>
        </fieldset>
      
        <center>
           <asp:Button ID="btnSave" runat="server" Text="Guardar" ValidationGroup="valFields" CssClass="button" OnClick="btnSave_Click" />&nbsp
           <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="Users.aspx">Volver</asp:HyperLink>
        </center>
    </div>
</asp:Content>
