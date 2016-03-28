<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAccountRoleManager.ascx.cs" Inherits="Web.S01.UCAccountRoleManager" %>
<%@ Register Src="~/UserControls/UCRoleDDL.ascx" TagPrefix="uc1" TagName="UCRoleDDL" %>
<%@ Register Src="~/UserControls/UCRoleUnitDDL.ascx" TagPrefix="uc1" TagName="UCRoleUnitDDL" %>
<style>
    .lvRow > td {
        vertical-align:top;
        padding: 7px 0;
        white-space:nowrap;
    }
    .lvRowHover:hover {
        background-color: #f1efee;
    }
</style>
<table style="width:100%" id="UCAccountRoleManager">
    <asp:ListView ID="main_lv" runat="server"
        ItemType="Model.S01.UCAccountRoleManagerInfo.Main"
        OnItemDataBound="main_lv_ItemDataBound"
        OnItemDeleting="main_lv_ItemDeleting"
        OnItemCommand="main_lv_ItemCommand"
        OnPreRender="main_lv_PreRender">
        <ItemTemplate>
            <tr class="lvRow lvRowHover" style="border-bottom:1px solid #AAA">
                <td style="width:1px;padding-left:3px">
                    <asp:Button ID="delete_btn" runat="server" CommandName="Delete" style="margin-right: 4px; margin-top:2px" CssClass="btnGrv delete" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" />
                </td>
                <td style="padding-right:10px;width:50px;">
                    <asp:Label ID="idx_lbl" runat="server" style="min-width:15px;text-align:right" Text='<%# Container.DisplayIndex+1 + "." %>'></asp:Label>
                    角色：<uc1:UCRoleDDL runat="server" ID="ucRoleDDL" AutoPostBack="true" OnSelectedIndexChanged="ucRoleDDL_SelectedIndexChanged" />
                </t>
                <td style="padding-right:10px;width:50px;">
                    <asp:Panel ID="unit_pl" runat="server">
                        單位：<uc1:UCRoleUnitDDL runat="server" ID="ucRoleUnitDDL" AutoPostBack="true" ItemTextHideCode="true" OnSelectedIndexChanged="ucRoleUnitDDL_SelectedIndexChanged"/>
                    </asp:Panel>
                </td>
                <td>
                    <asp:Panel ID="position_pl" runat="server">
                        <div style="display:table-cell;vertical-align:top">職位：</div>
                        <div style="display:table-cell;vertical-align:top">
                            <asp:CheckBoxList ID="rpid_cbl" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="3"></asp:CheckBoxList>
                        </div>
                    </asp:Panel>
                </td>
                <td style="width:20px;text-align:right;padding-right:3px;">
                    <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="btnGrv add" style="margin-top:2px" Text="新增身分" ToolTip="新增身分" UseSubmitBehavior="False" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</table>