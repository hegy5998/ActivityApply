<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCProcessSubFuncAuthManagerAuthView.ascx.cs" Inherits="Web.S01.UCProcessSubFuncAuthManagerAuthView" %>
<%@ Register Src="~/UserControls/UCGridViewPager.ascx" TagPrefix="uc1" TagName="UCGridViewPager" %>
<%@ Register Src="~/UserControls/UCRoleDDL.ascx" TagPrefix="uc1" TagName="UCRoleDDL" %>
<%@ Register Src="~/UserControls/UCRoleUnitDDL.ascx" TagPrefix="uc1" TagName="UCRoleUnitDDL" %>
<%@ Register Src="~/UserControls/UCRoleUnitPositionDDL.ascx" TagPrefix="uc1" TagName="UCRoleUnitPositionDDL" %>


<asp:Panel ID="pl" runat="server" Visible="false">
    <asp:GridView ID="main_gv" runat="server"
        ItemType="Model.S01.UCProcessSubFuncAuthManagerInfo.Auth"
        DataKeyNames="sys_pid,sys_cid,sys_rid,sys_uid,sys_rpid"
        OnRowCommand="main_gv_RowCommand" OnRowCancelingEdit="main_gv_RowCancelingEdit" OnRowDataBound="main_gv_RowDataBound" OnRowDeleting="main_gv_RowDeleting" OnRowEditing="main_gv_RowEditing" OnRowUpdating="main_gv_RowUpdating" OnSorting="main_gv_Sorting">
        <Columns>
            <asp:TemplateField ShowHeader="False" HeaderText="操作">
                <HeaderTemplate>
                    <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="btnGrv add" Text="新增" ToolTip="新增" UseSubmitBehavior="False" />
                </HeaderTemplate>
                <HeaderStyle Width="70px" />
                <ItemTemplate>
                    <asp:Button ID="edit_btn" runat="server" CommandName="Edit" CssClass="btnGrv edit" Text="編輯" ToolTip="編輯" UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="btnGrv delete" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" />
                    <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                <FooterTemplate>
                    <asp:Button ID="save_btn" runat="server" CommandName="AddSave" CssClass="btnGrv update" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                </FooterTemplate>
                <FooterStyle HorizontalAlign="Center" />
                <EditItemTemplate>
                    &nbsp;<asp:Button ID="save_btn" runat="server" CommandName="Update" CssClass="btnGrv update" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="角色" SortExpression="Sys_rid">
                <ItemTemplate>
                    <asp:Label ID="sys_rid_lbl" runat="server" Text="<%# Item.Sys_rid %>"></asp:Label> <asp:Label ID="sys_rname_lbl" runat="server" Text="<%# Item.Sys_rname %>"></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="rowTrigger" Wrap="false" />
                <FooterTemplate>
                    <uc1:UCRoleDDL runat="server" id="ucRoleDDL" OnSelectedIndexChanged="ucRoleDDL_SelectedIndexChanged" AutoPostBack="true"/>
                </FooterTemplate>
                <EditItemTemplate>
                    <uc1:UCRoleDDL runat="server" id="ucRoleDDL" OnSelectedIndexChanged="ucRoleDDL_SelectedIndexChanged" AutoPostBack="true"/>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="單位" SortExpression="Sys_uid">
                <ItemTemplate>
                    <asp:Label ID="sys_uid_lbl" runat="server" Text="<%# Item.Sys_uid %>"></asp:Label> <asp:Label ID="sys_uname_lbl" runat="server" Text="<%# Item.Sys_uname %>"></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="rowTrigger" Wrap="false" />
                <FooterTemplate>
                    <uc1:UCRoleUnitDDL runat="server" ID="ucRoleUnitDDL" OnSelectedIndexChanged="ucRoleUnitDDL_SelectedIndexChanged" AutoPostBack="true" ShowItemAll="true" />
                </FooterTemplate>
                <EditItemTemplate>
                    <uc1:UCRoleUnitDDL runat="server" ID="ucRoleUnitDDL" OnSelectedIndexChanged="ucRoleUnitDDL_SelectedIndexChanged" AutoPostBack="true" ShowItemAll="true" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="職位" SortExpression="Sys_rpid">
                <ItemTemplate>
                    <asp:Label ID="sys_rpid_lbl" runat="server" Text="<%# Item.Sys_rpid %>"></asp:Label> <asp:Label ID="sys_rpname_lbl" runat="server" Text="<%# Item.Sys_rpname %>"></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="rowTrigger" Wrap="false" />
                <FooterTemplate>
                    <uc1:UCRoleUnitPositionDDL runat="server" id="ucRoleUnitPositionDDL" OnSelectedIndexChanged="ucRoleUnitPositionDDL_SelectedIndexChanged" AutoPostBack="true" ShowItemAll="true" />
                </FooterTemplate>
                <EditItemTemplate>
                    <uc1:UCRoleUnitPositionDDL runat="server" id="ucRoleUnitPositionDDL" OnSelectedIndexChanged="ucRoleUnitPositionDDL_SelectedIndexChanged" AutoPostBack="true" ShowItemAll="true" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="權限說明" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="note_lbl" runat="server" Text=""></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="rowTrigger" Wrap="false" />
                <FooterTemplate>
                    <asp:Label ID="note_lbl" runat="server" Text=""></asp:Label>
                </FooterTemplate>
                <FooterStyle CssClass="" Wrap="false" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <uc1:UCGridViewPager runat="server" ID="ucGridViewPager" />
</asp:Panel>