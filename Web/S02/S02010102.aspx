<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Base.Master" AutoEventWireup="true" CodeBehind="S02010102.aspx.cs" Inherits="Web.S02.S02010102" %>

<asp:Content ID="Content1" ContentPlaceHolderID="baseHead_cph" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="baseContent_cph" runat="server">
    <h2>逢甲大學活動報名系統</h2>

    <%--<div> 
        <asp:Label ID="act_idn_hf" runat="server" />
        <asp:Label ID="as_idn_hf" runat="server" />
    </div>--%>

    <a href="#">匯出</a>
    <br /><br />

    <div class="row">
        <%--<asp:GridView ID="main_gv" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowCommand="main_gv_RowCommand" OnRowDeleting="main_gv_RowDeleting" OnSorting="main_gv_Sorting" OnRowEditing="main_gv_RowClose">--%>
        <asp:GridView ID="main_gv" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowCreated="main_gv_RowCreated" OnRowDeleting="main_gv_RowDeleting" OnRowCommand="main_gv_RowCommand" OnRowEditing="main_gv_RowEditing" OnRowCancelingEdit="main_gv_RowCancelingEdit" OnRowDataBound="main_gv_RowDataBound" OnRowUpdating="main_gv_RowUpdating" ViewStateMode="Enabled">
            <Columns>
                <%--操作--%>
                <asp:TemplateField ShowHeader="False" HeaderText="操作">
                    <EditItemTemplate>
                        &nbsp;<asp:Button ID="save_btn" runat="server" CommandName="Update" CssClass="btnGrv update" Text="" ToolTip="存檔" UseSubmitBehavior="False" />
                        &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="" ToolTip="取消" UseSubmitBehavior="False" />
                        <asp:HiddenField ID="old_aa_idn_hf" runat="server" Value='<%# Eval("aa_idn") %>' />
                    </EditItemTemplate>

                    <FooterTemplate>
                        <asp:Button ID="save_btn" runat="server" CommandName="AddSave" CssClass="btnGrv update" Text="" ToolTip="存檔" UseSubmitBehavior="False" />
                        &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="" ToolTip="取消" UseSubmitBehavior="False" />
                    </FooterTemplate>

                    <HeaderTemplate>
                        <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="btnGrv add" Text="" ToolTip="新增" UseSubmitBehavior="False" />
                    </HeaderTemplate>

                    <ItemTemplate>
                        <asp:Button ID="edit_btn" runat="server" CommandName="Edit" CssClass="btnGrv edit" Text="" ToolTip="編輯" UseSubmitBehavior="False" />
                        &nbsp;<asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="btnGrv delete" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" Text="" ToolTip="刪除" UseSubmitBehavior="False" />
                        <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                    </ItemTemplate>

                    <FooterStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    
    <script type="text/javascript">
        window.onbeforeunload = function () {
            window.event.returnValue = "尚未儲存資料";
            if (window.event.reason == false) {
                window.event.cancelBubble = true;
            }
        }
    </script>
</asp:Content>
