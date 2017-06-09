<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="editor.ascx.cs" Inherits="ProjectBase.WebTester.editor" %>
<asp:UpdatePanel ID="pnlLeo" runat="server">
<ContentTemplate>
<cus:DataField runat="server" ID="txtMail" Type="Email" Label="Email" MaxLength="50" IsRequired="true" TabIndex="8"/>
<cus:SubmitButton runat="server" ID="btnTest" Text="test" />
</ContentTemplate>
</asp:UpdatePanel>
