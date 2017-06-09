<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/base.Master" CodeBehind="addpermission.aspx.cs" Inherits="Administration.WebSite.Admin.addpermission" Title="Administración de Permisos"%>
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
            <label runat="server" id="lblPermission" visible="false">Permiso</label>
            <asp:DropDownList ID="ddlPermission" Enabled="true" Width="205px" Visible="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPermission_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="Page">Permiso de Página</asp:ListItem>
                <asp:ListItem Value="Method">Permiso de Método</asp:ListItem>
                <asp:ListItem Value="Entity">Permiso de Entidad</asp:ListItem>
                <asp:ListItem Value="Web">Permiso Web</asp:ListItem>
                <asp:ListItem Value="Execute">Permiso de Ejecución</asp:ListItem>
            </asp:DropDownList>
        </div>

        <div>
            <label>Nombre</label>
            <asp:TextBox ID="txtName" runat="server" Width="200px" MaxLength="256"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="*" ControlToValidate="txtName"></asp:RequiredFieldValidator>
         </div>  
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ValidationExpression="^(a-z|A-Z|0-9)*[^#$%^&()'<>!|°¬@=?¡¿{}¨]*$" ControlToValidate="txtName" ErrorMessage="Por favor, no ingrese caracteres especiales."></asp:RegularExpressionValidator>
        <div>
            <label>Código</label>
            <asp:TextBox ID="txtCode" runat="server" Width="200px" MaxLength="256"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="*" ControlToValidate="txtCode"></asp:RequiredFieldValidator>
        </div>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ValidationExpression="^(a-z|A-Z|0-9)*[^#$%^&()'<>!|°¬@=?¡¿{}¨]*$" ControlToValidate="txtCode" ErrorMessage="Por favor, no ingrese caracteres especiales."></asp:RegularExpressionValidator>
       <div>
            <label>Descripción</label>
            <asp:TextBox ID="txtDescription" Width="200px" runat="server" MaxLength="256"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="*" ControlToValidate="txtCode"></asp:RequiredFieldValidator>
        </div>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" runat="server" ValidationExpression="^(a-z|A-Z|0-9)*[^#$%^&()'<>!|°¬@=?¡¿{}¨]*$" ControlToValidate="txtDescription" ErrorMessage="Por favor, no ingrese caracteres especiales."></asp:RegularExpressionValidator>
        <div>
            <label>Acción</label>
            <asp:DropDownList ID="ddlAction" Width="205px" runat="server"></asp:DropDownList>
       </div>
       <div>
            <label id="lblPageName" runat="server" visible="false">Nombre de Página</label>
            <label id="lblMethodName" runat="server" visible="false">Nombre de Método</label>
            <label id="lblIdentifier" runat="server" visible="false">Identificador</label> 
            <label id="lblRelative" runat="server" visible="false">Ruta Relativa</label>     
            <asp:TextBox ID="txtPermission1" Width="200px" runat="server" MaxLength="256"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPage" runat="server" ErrorMessage="*" ControlToValidate="txtPermission1"></asp:RequiredFieldValidator>
        </div>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="Dynamic" ControlToValidate="txtPermission1" runat="server" ValidationExpression="^(a-z|A-Z|0-9)*[^#$%^&()'<>!|°¬@=?¡¿{}¨]*$" ErrorMessage="Por favor, no ingrese caracteres especiales."></asp:RegularExpressionValidator>
        <div>
            <label id="lblFolderName" runat="server" visible="false">Nombre de Carpeta</label>
            <label id="lblClassName" runat="server" visible="false">Nombre de Clase</label>
            <label id="lblControlIdentifier" runat="server" visible="false">Identificación de Control</label> 
            <asp:TextBox ID="txtPermission2" Width="200px" runat="server" MaxLength="256"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvFolder" runat="server" ErrorMessage="*" ControlToValidate="txtPermission2"></asp:RequiredFieldValidator>
        </div>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic" ControlToValidate="txtPermission2" runat="server" ValidationExpression="^(a-z|A-Z|0-9)*[^#$%^&()'<>!|°¬@=?¡¿{}¨]*$" ErrorMessage="Por favor, no ingrese caracteres especiales."></asp:RegularExpressionValidator>
    </fieldset>
    <center>
       <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="button" OnClick="btnSave_Click" />
       <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="Permissions.aspx">Volver</asp:HyperLink>
    </center>
</div>
</asp:Content>
