<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S02010501.aspx.cs" Inherits="Web.S02.S02010501" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead_cph" runat="server">
    <%@ Register Src="~/UserControls/UCGridViewPager.ascx" TagPrefix="uc1" TagName="UCGridViewPager" %>
    <%@ Register Src="~/S02/UCActivityStatementDialog.ascx" TagPrefix="uc1" TagName="UCActivityStatementDialog" %>
    <style type="text/css">
        .grv{
            width:1000px;
        }
        .grvPageRow {
            width:1000px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentSubFunction_cph" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent_cph" runat="server">
    <asp:GridView ID="main_gv" runat="server"
        ItemType="Model.Activity_statementInfo"
        DataKeyNames="ast_id"
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
                        <asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="movedown" Text="刪除" ToolTip="刪除" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                    </span>
                    <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="view_btn" />
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Center" />
                <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="聲明標題" SortExpression="Ast_title">
                <HeaderStyle Width="200px" />
                <ItemTemplate>
                    <asp:Label ID="ast_title_lbl" runat="server" Text='<%# Item.Ast_title %>'></asp:Label>
                    <asp:HiddenField ID="ast_id_hf" runat="server" Value='<%# Item.Ast_id %>' />
                </ItemTemplate>
                <ItemStyle CssClass="rowTrigger" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="敘述" SortExpression="Ast_desc">
                <ItemTemplate>
                    <%# Item.Ast_desc %>
                </ItemTemplate>
                <HeaderStyle Width="300px" />
                <ItemStyle CssClass="rowTrigger" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="保存年" SortExpression="Ast_year">                
                <ItemTemplate>
                    <%# Item.Ast_year %>
                </ItemTemplate>
                <HeaderStyle Width="40px" />
                <ItemStyle CssClass="rowTrigger center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="保存月" SortExpression="Ast_month">                
                <ItemTemplate>
                    <%# Item.Ast_month %>
                </ItemTemplate>
                <HeaderStyle Width="40px" />
                <ItemStyle CssClass="rowTrigger center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="共用" SortExpression="Ast_public">
                <HeaderStyle Width="40px" />
                <ItemTemplate>
                    <asp:HiddenField ID="ast_public_hf" runat="server" Value="<%# Item.Ast_public %>"></asp:HiddenField>
                    <asp:Label runat="server" ID="ast_public_lbl" CssClass="fa fa-check-square-o"></asp:Label>                    
                </ItemTemplate>
                <ItemStyle CssClass="rowTrigger center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <uc1:UCGridViewPager runat="server" ID="ucGridViewPager" />
    <uc1:UCActivityStatementDialog runat="server" ID="ucActivityStatementDialog" />
    <script type="text/javascript">
        for (var i = 2; i <= $('table tr').length; i++){
            if ($('table tr:nth-child(' + i + ') input[id*=ast_public_hf]').val() == "1")
                $('table tr:nth-child(' + i + ') i[id*=ast_public_icon]').css("display", "block");
        }
    </script>
</asp:Content>
