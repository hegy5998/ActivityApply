<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S01000901.aspx.cs" Inherits="Web.S01.S01000901" %>
<%@ Import Namespace="Util" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/UCSystemModule.ascx" TagPrefix="uc1" TagName="UCSystemModule" %>
<%@ Register Src="~/UserControls/UCGridViewPager.ascx" TagPrefix="uc1" TagName="UCGridViewPager" %>
<%@ Register Src="~/S01/UCProcessAuthManager.ascx" TagPrefix="uc1" TagName="UCProcessAuthManager" %>
<%@ Register Src="~/S01/UCProcessSubFuncAuthManagerDialog.ascx" TagPrefix="uc1" TagName="UCProcessSubFuncAuthManagerDialog" %>


<asp:Content ID="Content1" ContentPlaceHolderID="mainHead_cph" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentSubFunction_cph" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent_cph" runat="server">
    <div style="display:table;">
        <div style="width:300px;display:table-cell;vertical-align:top;padding-right: 20px">
            <div class="opBox nowrap">
                <uc1:UCSystemModule runat="server" ID="ucSystemModule" EnableModule="True" EnableShowAllModule="True" EnableShowAllSystem="False" />
            </div>
            <asp:GridView ID="main_gv" runat="server" AutoGenerateColumns="False" style="min-width: 300px"
                ItemType="Model.S01.S010009Info.Main"
                OnRowDataBound="main_gv_RowDataBound" 
                OnRowCommand="main_gv_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:Button ID="edit_btn" runat="server" CommandName="Set" CommandArgument='<%# Item.Sys_pid %>' CssClass="btnGrv edit" Text="設定權限" ToolTip="設定權限" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                            <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                        <HeaderTemplate>
                            <asp:Label ID="Label4" runat="server" OnPreRender="ManageControlAuth" Text="操作"></asp:Label>
                        </HeaderTemplate>
                        <HeaderStyle Width="40px"  Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="模組">
                        <ItemTemplate>
                            <%# Item.Sys_mid %>&nbsp;<%# Item.Sys_mname %>
                        </ItemTemplate>
                        <HeaderStyle Width="200px" />
                        <ItemStyle CssClass="rowTrigger"  Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="作業">
                        <ItemTemplate>
                            <%# Item.Sys_pid %>&nbsp;<%# Item.Sys_pname %>
                        </ItemTemplate>
                        <ItemStyle CssClass="rowTrigger"  Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="授權數">
                        <ItemTemplate>
                            <%# Item.Auth_count.GetValueOrDefault(0) %>
                        </ItemTemplate>
                        <ItemStyle CssClass="rowTrigger center"  Wrap="False" />
                        <HeaderStyle Width="40px"  Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="子功能" ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="subFunc_btn" runat="server" CommandArgument="<%# Item.Sys_pid %>" CssClass="btnGrv set" Text="設定" ToolTip="設定" OnClick="subFunc_btn_Click" UseSubmitBehavior="False" Visible="false" />
                        </ItemTemplate>
                        <HeaderStyle Width="60px" Wrap="False"  />
                        <ItemStyle HorizontalAlign="Center"  Wrap="False" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <uc1:UCGridViewPager runat="server" ID="ucGridViewPager" />
        </div>
        <div style="display:table-cell;vertical-align:top;">
            <uc1:UCProcessAuthManager runat="server" ID="ucProcessAuthManager" />
        </div>
    </div>
    <uc1:UCProcessSubFuncAuthManagerDialog runat="server" ID="ucProcessSubFuncAuthManagerDialog" />
</asp:Content>
