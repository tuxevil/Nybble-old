<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/base.Master" CodeBehind="Roles.aspx.cs" Inherits="Administration.WebSite.Admin.Roles" Title="Administración de Roles"%>
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
            <asp:GridView ID="grdRoles" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" AllowPaging="true" OnPageIndexChanging="grdRoles_PageIndexChanging" OnRowCommand="grdRoles_RowCommand"
                    CssClass="maingrid" PageSize="20">
                    <Columns>
                        <asp:ButtonField ButtonType="Image" ImageUrl="/img/edit.jpg" CommandName="Select"/>
                        <asp:BoundField DataField="Name" HeaderText="Nombre" />
                        <asp:BoundField DataField="Description" HeaderText="Descripción"/>
                        <asp:BoundField DataField="SiteName" HeaderText="Sitio \ Código"/>
                        <asp:CheckBoxField DataField="IsAdministrator" ReadOnly="true" ItemStyle-HorizontalAlign="Center"  HeaderText="Administrador"/>
                        <asp:ButtonField Text="[x]" CommandName="Erase"/>
                    </Columns>
             </asp:GridView>
     
         </fieldset>
            <center>
                <asp:HyperLink ID="lnkNew" runat="server" NavigateUrl="addRol.aspx">Nuevo</asp:HyperLink>
                <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="../Default.aspx">Volver</asp:HyperLink>
            </center>
            <asp:Label ID="lblError" runat="server" Text="Por favor, borre las funciones asociadas al rol." Visible="false" ForeColor="red"></asp:Label>
</div>
</asp:Content>