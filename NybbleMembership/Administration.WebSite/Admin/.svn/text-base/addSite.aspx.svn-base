<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/base.Master" CodeBehind="addSite.aspx.cs" Inherits="Administration.WebSite.Admin.addSite" Title="Administración de Sitio"%>
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
                <asp:TextBox ID="txtName" runat="server" MaxLength="256"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="*" ControlToValidate="txtName"></asp:RequiredFieldValidator>
            </div>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="Dynamic" ControlToValidate="txtName" runat="server" ValidationExpression="^(a-z|A-Z|0-9)*[^#$%^&()'<>!|°¬@=?¡¿{}¨]*$" ErrorMessage="Por favor, no ingrese caracteres especiales."></asp:RegularExpressionValidator>
            <div>
                <label>Código</label>
                <asp:TextBox ID="txtCode" runat="server" MaxLength="256"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="*" ControlToValidate="txtCode"></asp:RequiredFieldValidator>
            </div>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ControlToValidate="txtCode" runat="server" ValidationExpression="^(a-z|A-Z|0-9)*[^#$%^&()'<>!|°¬@=?¡¿{}¨]*$" ErrorMessage="Por favor, no ingrese caracteres especiales."></asp:RegularExpressionValidator>
       <div>
            <table cellspacing=0 cellpadding=0 border=0 width="100%">
                <tr>
                    <td width="10%"><label>Usuarios</label></td>
                    <td><uc1:ListTransfer id="ltUsers" runat="server"></uc1:ListTransfer></td>
                </tr>
            </table>
        </div>
       </fieldset>
        <center>
            <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="button" OnClick="btnSave_Click" />
           <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="Sites.aspx">Volver</asp:HyperLink>
        </center>
</div>

</asp:Content>
