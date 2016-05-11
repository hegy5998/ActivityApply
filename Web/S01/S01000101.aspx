<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S01000101.aspx.cs" Inherits="Web.S01.S01000101" %>

<%@ Register Src="~/UserControls/UCGridViewPager.ascx" TagPrefix="uc1" TagName="UCGridViewPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead_cph" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentSubFunction_cph" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent_cph" runat="server">
    <div style="padding:0 3px 3px; margin-bottom: 8px">
        主機IP：<span class="highlight" style="margin-right:15px"><%=Util.CommonHelper.GetServerIP() %></span>
        系統參數上次載入時間：<span class="highlight"><%=Util.CommonHelper.SysConfigUpdtime.ToString("yyyy/MM/dd HH:mm:ss") %></span>
        <span style="margin-left: 8px"><asp:Button ID="refresh_btn" runat="server" CssClass="btn btn-default" Text="重新取得系統參數" OnClick="refresh_btn_Click" /></span>
    </div>
    <asp:ObjectDataSource ID="main_ods" runat="server" SelectMethod="GetData" TypeName="BusinessLayer.S01.S010001BL" UpdateMethod="UpdateData" DataObjectTypeName="Model.Sys_configInfo" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <asp:GridView ID="main_gv" runat="server" AutoGenerateColumns="False" DataSourceID="main_ods" OnRowUpdated="main_gv_RowUpdated" OnRowUpdating="main_gv_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="操作" ShowHeader="False">
                <EditItemTemplate>
                    <span style="position: relative;">
                        <i class="fa fa-floppy-o btn btn-success btn-xs" aria-hidden="true"></i>
                        <asp:Button ID="save_btn" runat="server" CommandName="Update" CssClass="movedown" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                    </span>
                        &nbsp;
                    <span style="position: relative;">
                        <i class="fa fa-undo btn btn-warning btn-xs" aria-hidden="true"></i>
                    <asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="movedown" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                 </span>
                        </EditItemTemplate>
                <HeaderTemplate>
                    <asp:Label ID="Label4" runat="server" OnPreRender="ManageControlAuth" Text="操作"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <span style="position: relative;">
                        <i class="fa fa-pencil btn btn-primary btn-xs" aria-hidden="true"></i>
                        <asp:Button ID="edit_btn" runat="server" CommandName="Edit" CssClass="movedown" Text="編輯" ToolTip="編輯" UseSubmitBehavior="False" OnPreRender="ManageControlAuth"/>
                    </span>                    
                    <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                </ItemTemplate>
                <HeaderStyle Width="70px" CssClass="nowrap" />
                <ItemStyle HorizontalAlign="Center" CssClass="nowrap"/>
            </asp:TemplateField>
            <asp:BoundField DataField="sys_name" HeaderText="參數名稱" ReadOnly="True" >
            <HeaderStyle Width="300px"/>
            <ItemStyle CssClass="rowTrigger" />
            </asp:BoundField>
            <asp:BoundField DataField="sys_note" HeaderText="參數說明" ReadOnly="True" >
            <HeaderStyle Width="300px" />
            <ItemStyle CssClass="rowTrigger" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="參數值">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("sys_value") %>' Width="90%"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("sys_value") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle />
                <ItemStyle CssClass="autoNewline rowTrigger" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <uc1:UCGridViewPager runat="server" ID="ucGridViewPager" />
</asp:Content>
