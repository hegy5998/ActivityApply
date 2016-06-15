<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCProcessSubFuncAuthManager.ascx.cs" Inherits="Web.S01.UCProcessSubFuncAuthManager" %>
<%@ Register Src="~/UserControls/UCGridViewPager.ascx" TagPrefix="uc1" TagName="UCGridViewPager" %>
<%@ Register Src="~/UserControls/UCRoleDDL.ascx" TagPrefix="uc1" TagName="UCRoleDDL" %>
<%@ Register Src="~/UserControls/UCRoleUnitDDL.ascx" TagPrefix="uc1" TagName="UCRoleUnitDDL" %>
<%@ Register Src="~/UserControls/UCRoleUnitPositionDDL.ascx" TagPrefix="uc1" TagName="UCRoleUnitPositionDDL" %>
<%@ Register Src="~/S01/UCProcessSubFuncAuthManagerAuthView.ascx" TagPrefix="uc1" TagName="UCProcessSubFuncAuthManagerAuthView" %>


<asp:Panel ID="pl" runat="server" Visible="false">
    <div class="opBox">
        系統：<span class="highlight" style="margin-right: 10px"><asp:Label ID="sys_id_lbl" runat="server" Text="" Style="margin-right: 3px"></asp:Label><asp:Label ID="sys_name_lbl" runat="server" Text=""></asp:Label></span>
        模組：<span class="highlight" style="margin-right: 10px"><asp:Label ID="sys_mid_lbl" runat="server" Text="" Style="margin-right: 3px"></asp:Label><asp:Label ID="sys_mname_lbl" runat="server" Text=""></asp:Label></span>
        作業：<span class="highlight" style="margin-right: 10px"><asp:Label ID="sys_pid_lbl" runat="server" Text="" Style="margin-right: 3px"></asp:Label><asp:Label ID="sys_pname_lbl" runat="server" Text=""></asp:Label></span>
    </div>
    <asp:MultiView ID="mv" runat="server">
        <asp:View ID="main_view" runat="server">
            <asp:GridView ID="main_gv" runat="server" AutoGenerateColumns="False" Style="min-width: 300px"
                ItemType="Model.S01.UCProcessSubFuncAuthManagerInfo.Main"
                DataKeyNames="sys_pid,sys_cid"
                OnRowDataBound="main_gv_RowDataBound"
                OnRowCommand="main_gv_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <span style="position: relative;">
                                <i class="fa fa-pencil btn btn-primary btn-xs" aria-hidden="true"></i>
                                <asp:Button ID="edit_btn" runat="server" CommandName="Set" CssClass="movedown" Text="設定權限" ToolTip="設定權限" UseSubmitBehavior="False" />
                            </span>
                            <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                        <HeaderTemplate>
                            <asp:Label ID="Label4" runat="server" Text="操作"></asp:Label>
                        </HeaderTemplate>
                        <HeaderStyle Width="40px" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="授權數">
                        <ItemTemplate>
                            <%# Item.Auth_count.GetValueOrDefault(0) %>
                        </ItemTemplate>
                        <ItemStyle CssClass="rowTrigger center" Wrap="False" />
                        <HeaderStyle Width="40px" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="子功能代碼">
                        <ItemTemplate>
                            <%# Item.Sys_cid %>
                        </ItemTemplate>
                        <ItemStyle CssClass="rowTrigger" Wrap="False" />
                        <HeaderStyle Width="40px" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="子功能描述">
                        <ItemTemplate>
                            <asp:Label ID="sys_cnote_lbl" runat="server" Text="<%# Item.Sys_cnote %>"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="rowTrigger" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <uc1:UCGridViewPager runat="server" ID="ucGridViewPager" />
        </asp:View>
        <asp:View ID="auth_view" runat="server">
            <asp:Button ID="back_btn" runat="server" CssClass="btn btn-default" Text="返回" UseSubmitBehavior="false" OnClick="back_btn_Click" Style="margin-right: 10px" />
            子功能：<asp:Label ID="sys_cid_lbl" runat="server" CssClass="highlight" Text="" Style="margin-right: 10px"></asp:Label>
            描述：<asp:Label ID="sys_cnote_lbl" runat="server" CssClass="highlight" Text=""></asp:Label>
            <div style="margin-top: 10px">
                <uc1:UCProcessSubFuncAuthManagerAuthView runat="server" ID="ucProcessSubFuncAuthManagerAuthView" />
            </div>
        </asp:View>
    </asp:MultiView>

</asp:Panel>
