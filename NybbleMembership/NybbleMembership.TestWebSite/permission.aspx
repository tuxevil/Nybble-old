<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="permission.aspx.cs" Inherits="NybbleMembership.TestWebSite.permission" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="DropDownList3" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AutoPostBack ="true" runat="server">
        </asp:DropDownList><br />
        <asp:GridView ID="GridView3" runat="server"></asp:GridView>
        <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple"></asp:ListBox><br />
        <asp:Button ID="btnLista" runat="server" OnClick="btnLista_Click" Text="Obtener Lista" /><br />
        &nbsp;<br />
        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList><br />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        <br />
        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:GridView ID="GridView2" runat="server">
        </asp:GridView>
    
    
        <asp:Label runat="server" Text="clase" ID="clase"></asp:Label><asp:TextBox runat="server" ID="txtclass"></asp:TextBox>
        <asp:Label runat="server" Text="identifier" ID="identifier"></asp:Label><asp:TextBox runat="server" ID="txtidentifier"></asp:TextBox>
        <asp:Label runat="server" Text="usuario" ID="user"></asp:Label><asp:TextBox runat="server" ID="txtuser"></asp:TextBox>
        <asp:Button ID="btnborrar" runat="server" OnClick="btnborrar_Click" Text="borrar permiso" /><br />
        
        
       </div>
    </form>
</body>
</html>
