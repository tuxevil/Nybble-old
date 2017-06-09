<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NybbleMembership.TestWebSite._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          &nbsp;<asp:LoginName ID="LoginName1" runat="server" />
        <asp:LoginStatus ID="LoginStatus1" runat="server" />
        <br />
        <br />
        &nbsp; &nbsp; &nbsp;
        <table>
            <tr>
                <td>
    </td>
                <td>
        <asp:TextBox ID="TextBox1" runat="server" Width="50px"></asp:TextBox></td>
                <td>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>+</asp:ListItem>
            <asp:ListItem>-</asp:ListItem>
            <asp:ListItem Value="*">*</asp:ListItem>
            <asp:ListItem>/</asp:ListItem>
        </asp:DropDownList></td>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server">
                    </asp:DropDownList></td>
                <td style="width: 3px">
        =</td>
                <td>
        <asp:TextBox ID="txtresult" runat="server" Width="50px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 21px">
    <asp:Button ID="Button3" runat="server" Text="Calcular" OnClick="Button3_Click" /></td>
                <td style="height: 21px">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Copiar" /></td>
                <td style="height: 21px">
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Copiar" /></td>
                <td style="height: 21px">
                    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Copiar" /></td>
                <td style="width: 3px; height: 21px">
                </td>
                <td style="height: 21px">
                    <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Copiar" /></td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server"></asp:Label></td>
                <td>
                    <asp:Label ID="Label2" runat="server"></asp:Label></td>
                <td>
                    <asp:Label ID="Label3" runat="server"></asp:Label></td>
                <td style="width: 3px">
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="6">
                    &nbsp;&nbsp;
                </td>
            </tr>
        </table>
    
   
    </div>
    </form>
</body>
</html>
