<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="S01000701.aspx.cs" Inherits="Web.S01.S01000701" %>

<%@ Register Src="~/UserControls/UCGridViewPager.ascx" TagPrefix="uc1" TagName="UCGridViewPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead_cph" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentSubFunction_cph" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent_cph" runat="server">
    <asp:GridView ID="main_gv" runat="server" AutoGenerateColumns="False" OnRowCommand="main_gv_RowCommand" OnRowEditing="main_gv_RowEditing" OnRowUpdating="main_gv_RowUpdating" OnRowCancelingEdit="main_gv_RowCancelingEdit" ViewStateMode="Enabled" OnRowDeleting="main_gv_RowDeleting" OnRowDataBound="main_gv_RowDataBound" OnSorting="main_gv_Sorting">
        <Columns>
            <asp:TemplateField ShowHeader="False" HeaderText="操作">
                <EditItemTemplate>
                    &nbsp;<asp:Button ID="save_btn" runat="server" CommandName="Update" CssClass="btnGrv update" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                    <asp:HiddenField ID="sys_uid_hf" runat="server" Value='<%# Eval("Sys_uid") %>' />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="save_btn" runat="server" CommandName="AddSave" CssClass="btnGrv update" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                </FooterTemplate>
                <HeaderTemplate>
                    <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="btnGrv add" Text="新增" ToolTip="新增" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Button ID="edit_btn" runat="server" CommandName="Edit" CssClass="btnGrv edit" Text="編輯" ToolTip="編輯" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                    &nbsp;<asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="btnGrv delete" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                    <asp:HiddenField ID="sys_uid_hf" runat="server" Value='<%# Eval("Sys_uid") %>' />
                    <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Center" />
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="單位代碼" SortExpression="Sys_uid">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Sys_uid") %>' Width="100px" MaxLength="30"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="sys_uid_txt" runat="server" Text='<%# Bind("Sys_uid") %>' Width="100px" MaxLength="30"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="sys_uid_lbl" runat="server" Text='<%# Bind("Sys_uid") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="110px" />
                <ItemStyle CssClass="rowTrigger" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="單位名稱" SortExpression="Sys_uname">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Sys_uname") %>' Width="95%" MaxLength="20"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="sys_uname_txt" runat="server" Text='<%# Bind("Sys_uname") %>' Width="95%" MaxLength="20"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="sys_uname_lbl" runat="server" Text='<%# Bind("Sys_uname") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="rowTrigger" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <uc1:UCGridViewPager runat="server" ID="ucGridViewPager" />
</asp:Content>
