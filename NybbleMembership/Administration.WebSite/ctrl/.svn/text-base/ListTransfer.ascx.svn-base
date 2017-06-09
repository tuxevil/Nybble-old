<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_ListTransfer" Codebehind="ListTransfer.ascx.cs" %>
<%@ Register TagPrefix="fluent" Namespace="Fluent"  Assembly="Fluent.ListTransfer" %>

<fluent:ListTransfer Runat="server" ID="lstTransfer" 
    ListControlTo="lstDestination" 
    ListControlFrom="lstSource" 
    EnableClientSide="True" 
    />

<div class="listselector">

<asp:ListBox ID="lstSource" Runat="server" SelectionMode="Multiple" Width="45%" CssClass="listbox" />

<ul>
<li><a href="#" onclick="<%= lstTransfer.ClientMoveAll %>"><img border="0" src="/img/rightAll.png"></a></li>
<li><a href="#" onclick="<%= lstTransfer.ClientMoveSelected %>" ><img border="0" src="/img/right.png"></a></li>
<li><a href="#" onclick="<%= lstTransfer.ClientMoveBackSelected %>"><img border="0" src="/img/left.png"></a></li>
<li><a href="#" onclick="<%= lstTransfer.ClientMoveBackAll %>"><img border="0" src="/img/leftAll.png"></a></li>
</ul>

<asp:ListBox ID="lstDestination" Runat="server" SelectionMode="Multiple" Width="45%" CssClass="listbox" />
    
</div>