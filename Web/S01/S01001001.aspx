<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S01001001.aspx.cs" Inherits="Web.S01.S01001001" %>

<%@ Register Src="~/UserControls/UCGridViewPager.ascx" TagPrefix="uc1" TagName="UCGridViewPager" %>
<%@ Register Src="~/S01/UCAccountManagerDialog.ascx" TagPrefix="uc1" TagName="UCAccountManagerDialog" %>


<asp:Content ID="Content1" ContentPlaceHolderID="mainHead_cph" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentSubFunction_cph" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent_cph" runat="server">
    <asp:GridView ID="main_gv" runat="server"
        ItemType="Model.S01.S010010Info.Main"
        DataKeyNames="act_id"
        OnRowCommand="main_gv_RowCommand"
        OnRowDeleting="main_gv_RowDeleting"
        OnRowDataBound="main_gv_RowDataBound"
        OnSelectedIndexChanging="main_gv_SelectedIndexChanging"
        OnSorting="main_gv_Sorting">
        <Columns>
            <asp:TemplateField ShowHeader="False" HeaderText="操作">            
                <HeaderTemplate>
                    <span style="position: relative;">
                        <i class="fa fa-plus btn btn-info btn-xs" aria-hidden="true"></i>
                        <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="movedown" Text="新增" ToolTip="新增" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                    </span>
                </HeaderTemplate>
                <ItemTemplate>
                    <span style="position: relative;">
                        <i class="fa fa-pencil btn btn-primary btn-xs" aria-hidden="true"></i>
                        <asp:Button ID="view_btn" runat="server" CommandName="Select" CssClass="movedown" Text="編輯" ToolTip="編輯" UseSubmitBehavior="False" />
                    </span>&nbsp;
                    <span style="position: relative;">
                        <i class="fa fa-trash-o btn btn-danger btn-xs" aria-hidden="true"></i>
                        <asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="movedown" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                    </span>
                    <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="view_btn" />
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Center" />
                <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="帳號" SortExpression="Act_id">
                <HeaderStyle Width="110px" />
                <ItemTemplate>
                    <asp:Label ID="act_id_lbl" runat="server" Text='<%# Item.Act_id %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="rowTrigger center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="名稱" SortExpression="Act_name">
                <ItemTemplate>
                    <%# Item.Act_name %>
                </ItemTemplate>
                <ItemStyle CssClass="rowTrigger" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="帳號狀態" SortExpression="Act_status">
                <HeaderStyle Width="110px" />
                <ItemTemplate>
                    <%# Item.Act_status == "Y" ? "正常" : "<span class='alert'>停用</span>"%>
                </ItemTemplate>
                <ItemStyle CssClass="rowTrigger center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <uc1:UCGridViewPager runat="server" ID="ucGridViewPager" />
    <uc1:UCAccountManagerDialog runat="server" ID="ucAccountManagerDialog" />
</asp:Content>
