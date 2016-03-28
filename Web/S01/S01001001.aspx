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
                <EditItemTemplate>
                    &nbsp;<asp:Button ID="save_btn" runat="server" CommandName="Update" CssClass="btnGrv update" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="save_btn" runat="server" CommandName="AddSave" CssClass="btnGrv update" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                </FooterTemplate>
                <HeaderTemplate>
                    <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="btnGrv add" Text="新增" ToolTip="新增" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Button ID="view_btn" runat="server" CommandName="Select" CssClass="btnGrv edit" Text="編輯" ToolTip="編輯" UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="btnGrv delete" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                    <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="view_btn" />
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Center" />
                <HeaderStyle Width="70px" />
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
