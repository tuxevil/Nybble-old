<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/base.Master" CodeBehind="Functions.aspx.cs" Inherits="Administration.WebSite.Admin.Functions" Title="Administración de Funciones" %>
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
                  <asp:GridView ID="grdFunctions" runat="server" PageSize="20" AutoGenerateColumns="False" DataKeyNames="Id" OnPageIndexChanging="grdFunctions_PageIndexChanging" AllowPaging="true" 
                        CssClass="maingrid" OnRowCommand="grdFunctions_RowCommand" >
                        <Columns>
                            <asp:ButtonField ButtonType="Image" ImageUrl="/img/edit.jpg" CommandName="Select"/>
                            <asp:BoundField DataField="Name" HeaderText="Nombre"/>
                            <asp:BoundField DataField="Description" HeaderText="Descripcion"/>
                            <asp:BoundField DataField="ParentName" HeaderText="Padre"/>
                            <asp:BoundField DataField="SiteName" HeaderText="Sitio"/>
                            <asp:ButtonField Text="[x]" CommandName="Erase"/>
                        </Columns>
                        
                 </asp:GridView>
        </fieldset>
        <center>
             <asp:HyperLink ID="lnkNew" runat="server" NavigateUrl="addFunction.aspx">Nuevo</asp:HyperLink>
             <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="../Default.aspx">Volver</asp:HyperLink>
        </center>
           <asp:Label ID="lblError" runat="server" Text="Por favor, borre los permisos asociadas a la función." Visible="false" ForeColor="red"></asp:Label>
    </div>
 </asp:Content>    