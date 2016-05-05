<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCSystemModule.ascx.cs" Inherits="Web.UserControls.UCSystemModule" %>
<asp:Panel ID="system_pl" runat="server" style="display: inline-block;*display: inline;zoom: 1;">
    系統：<asp:DropDownList ID="system_ddl" runat="server" OnSelectedIndexChanged="system_ddl_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
</asp:Panel>
<asp:Panel ID="module_pl" runat="server" style="display: inline-block;*display: inline;zoom: 1;">
    &nbsp;模組：<asp:DropDownList ID="module_ddl" runat="server" AutoPostBack="True" OnSelectedIndexChanged="module_ddl_SelectedIndexChanged"></asp:DropDownList>
</asp:Panel>


