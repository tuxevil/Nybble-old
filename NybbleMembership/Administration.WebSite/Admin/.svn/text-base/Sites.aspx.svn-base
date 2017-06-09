<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/base.Master" CodeBehind="Sites.aspx.cs" Inherits="Administration.WebSite.Admin.Sites" Title="Administración de Sitios" %>
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
             <asp:GridView ID="grdSites" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" AllowPaging="true" OnPageIndexChanging="grdSites_PageIndexChanging" OnRowCommand="grdSites_RowCommand" 
                        CssClass="maingrid" PageSize="20">
                        <Columns>
                            <asp:ButtonField ButtonType="Image" ImageUrl="/img/edit.jpg" CommandName="Select"/>
                            <asp:BoundField DataField="Code" HeaderText="Código"/>
                            <asp:BoundField DataField="Name" HeaderText="Nombre" />
                            <asp:BoundField DataField="AppName" HeaderText="Nombre de Aplicación"/>
                            <asp:ButtonField Text="[x]" CommandName="Erase"/>
                        </Columns>
                 </asp:GridView>
     </fieldset>
        <center>
           <asp:HyperLink ID="lnkNew" runat="server" NavigateUrl="addSite.aspx">Nuevo</asp:HyperLink>
           <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="../Default.aspx">Volver</asp:HyperLink>
        </center>
            <asp:Label ID="lblError" runat="server" Text="Pro favor, borre los roles y funciones asociados al sitio." Visible="false" ForeColor="red"></asp:Label>
</div>
</asp:Content>