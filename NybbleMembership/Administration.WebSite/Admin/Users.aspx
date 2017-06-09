<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/base.Master" CodeBehind="Users.aspx.cs" Inherits="Administration.WebSite.Admin.Users" Title="Administración de Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cplMain" Runat="Server">

    <div class="page_header">
        <h1>
            <asp:Literal runat="server" ID="lblTitle">Usuarios</asp:Literal>
        </h1>
    </div>
 
    <div class="form">
        <fieldset>
            <asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdUsers_DataBound" DataKeyNames="Id" OnRowCommand="grdUsers_RowCommand" OnPageIndexChanging="grdUsers_PageIndexChanging" AllowPaging="true" pa
                        CssClass="maingrid" PageSize="20">
                        <Columns>
                            <asp:ButtonField ButtonType="Image" ImageUrl="/img/edit.jpg" CommandName="Select"/>
                            <asp:BoundField DataField="UserName" HeaderText="Usuario"/>
                            <asp:BoundField HeaderText="Nombre"/>
                            <asp:BoundField HeaderText="Apellido"/>
                            <asp:BoundField DataField="email" HeaderText="Email"/>
                            <asp:CheckBoxField DataField="isApproved" ReadOnly="true" ItemStyle-HorizontalAlign="Center" HeaderText="Aprobado" />
                            <asp:CheckBoxField DataField="isLockedOut" ReadOnly="true" ItemStyle-HorizontalAlign="Center" HeaderText="Bloqueado" />
                            <asp:ButtonField Text="Desbloquear" CommandName="Unlock"/>
                            <asp:ButtonField Text="[x]" CommandName="Erase" HeaderText="Borrar"/>
                            <asp:ButtonField Text="Resetear Password" CommandName="ResetPass"/>
                        </Columns>
                 </asp:GridView>
        
        
        </fieldset>
        <center>
            <asp:HyperLink ID="lnkNew" runat="server" NavigateUrl="addUser.aspx">Nuevo</asp:HyperLink>
            <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="../Default.aspx">Volver</asp:HyperLink>
        </center>
        <asp:Label ID="lblPass" runat="server" Text="La nueva contraseña ha sido enviada al e-mail del usuario." Visible="false" ForeColor="green"></asp:Label>
</div>
</asp:Content>
