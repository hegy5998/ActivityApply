<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Base.Master" AutoEventWireup="true" CodeBehind="S02010102.aspx.cs" Inherits="Web.S02.S02010102" %>

<asp:Content ID="Content1" ContentPlaceHolderID="baseHead_cph" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="baseContent_cph" runat="server">
    <h2>逢甲大學活動報名系統</h2>
    <a href="#">刪除全部</a> |
    <a href="#">匯出</a> |
    <a href="#">儲存資料</a>
    <br /><br />

    <asp:GridView ID="controlSet_gv" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" >
        <Columns>
            <%--操作--%>
            <asp:TemplateField ShowHeader="False" HeaderText="操作">
                <EditItemTemplate>
                    &nbsp;<asp:Button ID="save_btn" runat="server" CommandName="Update" CssClass="btnGrv update" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                    <asp:HiddenField ID="old_cop_act_hf" runat="server" Value='<%# Eval("cop_act") %>' />
                    <asp:HiddenField ID="old_cop_id_hf" runat="server" Value='<%# Eval("cop_id") %>' />
                </EditItemTemplate>

                <FooterTemplate>
                    <asp:Button ID="save_btn" runat="server" CommandName="AddSave" CssClass="btnGrv update" Text="存檔" ToolTip="存檔" UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="cancel_btn" runat="server" CommandName="Cancel" CssClass="btnGrv cancel" Text="取消" ToolTip="取消" UseSubmitBehavior="False" />
                </FooterTemplate>

                <HeaderTemplate>
                    <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="btnGrv add" Text="新增" ToolTip="新增" UseSubmitBehavior="False" />
                </HeaderTemplate>

                <ItemTemplate>
                    <asp:Button ID="edit_btn" runat="server" CommandName="Edit" CssClass="btnGrv edit" Text="編輯" ToolTip="編輯" UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="btnGrv delete" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" />
                    <asp:HiddenField ID="rowDefaultTriggerControlID_hf" runat="server" EnableViewState="False" Value="edit_btn" />
                    <asp:HiddenField ID="cop_act_hf" runat="server" Value='<%# Eval("cop_act") %>' />
                </ItemTemplate>

                <FooterStyle HorizontalAlign="Center" />
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Center" Wrap="False" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <script>
        window.onbeforeunload = function () {
            window.event.returnValue = "尚未儲存資料";
            if (window.event.reason == false) {
                window.event.cancelBubble = true;
            }
        }
    </script>
</asp:Content>
