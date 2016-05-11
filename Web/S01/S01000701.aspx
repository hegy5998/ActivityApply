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
                    <span style="position: relative;">
                        <i class="fa fa-floppy-o btn btn-success btn-xs" aria-hidden="true"></i>
                        <asp:Button ID="save_btn" runat="server" CommandName="Update" CssClass="movedown" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                    </span>
                    &nbsp;                   
                    <span style="position: relative;">
                        <i class="fa fa-undo btn btn-warning btn-xs" aria-hidden="true"></i>
                        <asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="movedown" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                    </span>
                    <asp:HiddenField ID="sys_uid_hf" runat="server" Value='<%# Eval("Sys_uid") %>' />
                </EditItemTemplate>
                <FooterTemplate>
                    <span style="position: relative;">
                        <i class="fa fa-floppy-o btn btn-success btn-xs" aria-hidden="true"></i>
                        <asp:Button ID="save_btn" runat="server" CommandName="AddSave" CssClass="movedown" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                    </span>
                    &nbsp;                   
                    <span style="position: relative;">
                        <i class="fa fa-undo btn btn-warning btn-xs" aria-hidden="true"></i>
                        <asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="movedown" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                    </span>
                </FooterTemplate>
                <HeaderTemplate>
                    <span style="position: relative;">
                        <i class="fa fa-plus btn btn-info btn-xs" aria-hidden="true"></i>
                        <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="movedown" Text="新增" ToolTip="新增" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                    </span>
                </HeaderTemplate>
                <ItemTemplate>
                    <span style="position: relative;">
                        <i class="fa fa-pencil btn btn-primary btn-xs" aria-hidden="true"></i>
                        <asp:Button ID="edit_btn" runat="server" CommandName="Edit" CssClass="movedown" Text="編輯" ToolTip="編輯" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                    </span>
                    &nbsp;
                    <span style="position: relative;">
                        <i class="fa fa-trash-o btn btn-danger btn-xs" aria-hidden="true"></i>
                        <asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="movedown" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" OnPreRender="ManageControlAuth" />
                    </span>
                    <asp:HiddenField ID="sys_uid_hf" runat="server" Value='<%# Eval("Sys_uid") %>' />
                    <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Center" />
                <HeaderStyle Width="80px" />
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
