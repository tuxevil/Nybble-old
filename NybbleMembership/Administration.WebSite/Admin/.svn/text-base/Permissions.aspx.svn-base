<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/base.Master" CodeBehind="Permissions.aspx.cs" Inherits="Administration.WebSite.Admin.Permissions" Title="Administración de Permisos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cplMain" Runat="Server">

    <div class="page_header">
        <h1>
            <asp:Literal runat="server" ID="lblTitle">Funciones</asp:Literal>
        </h1>
    </div>
    
 <div class="form">
        <fieldset> 
            <asp:GridView ID="grdPermissions" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" DataKeyNames="Id" OnPageIndexChanging="grdPermissions_PageIndexChanging" AllowPaging="true" 
                        CssClass="maingrid" OnRowCommand="grdPermissions_RowCommand" PageSize="20" >
                        <Columns>
                            <asp:ButtonField ButtonType="Image" ImageUrl="/img/edit.jpg" CommandName="Select"/>
                            <asp:BoundField DataField="Name" HeaderText="Nombre"/>
                            <asp:BoundField DataField="Description" HeaderText="Descripcion"/>
                            <asp:BoundField DataField="Code" HeaderText="Codigo"/>
                            <asp:BoundField DataField="PermissionAction" HeaderText="Accion"/>
                            <asp:ButtonField Text="[x]" CommandName="Erase"/>
                        </Columns>
                </asp:GridView>
                 
        </fieldset>
        <center>
             <asp:HyperLink ID="lnkNew" runat="server" NavigateUrl="addpermission.aspx">Nuevo</asp:HyperLink>
             <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="../Default.aspx">Volver</asp:HyperLink>
        </center>
</div>
 </asp:Content>    