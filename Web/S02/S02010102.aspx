<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Base.Master" AutoEventWireup="true" CodeBehind="S02010102.aspx.cs" Inherits="Web.S02.S02010102" %>
<%@ Register Src="~/UserControls/UCSystemModule.ascx" TagPrefix="uc1" TagName="UCSystemModule" %>
<%@ Import Namespace="Util" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="baseHead_cph" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="baseContent_cph" runat="server">
    <h2>逢甲大學活動報名系統</h2>

    <a href="#">匯出</a>
    <br /><br />

    <div class="row">
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

        <%--多選跳出視窗--%>
        <asp:ModalPopupExtender ID="multi_mpe" runat="server" PopupControlID="multi_pl" TargetControlID="control_OK_btn" BackgroundCssClass="popupWindowOverlay" OkControlID="control_cancel_btn"></asp:ModalPopupExtender>

        <!-- 多選 START -->
        <asp:Panel ID="multi_pl" class="popupWindow" runat="server" Visible="false">
            <asp:ScriptManager ID="ScriptManager" runat="server">
            </asp:ScriptManager>

            <!-- 多選標頭 START -->
            <div class="popupWindowHeader">
                <!-- 標題 -->
                <div class="title">
                    <asp:UpdatePanel ID="control_title_upl" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="control_tilte_lbl" runat="server" Text="請選擇"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <!-- 關閉按鈕 -->
                <asp:Button ID="control_cancel_btn" CssClass="close" runat="server" Text="關閉" ToolTip="關閉" OnClick="control_cancel_btn_Click" UseSubmitBehavior="False" />
                <asp:Button ID="control_OK_btn" CssClass="open"  runat="server" Text="開啟" ToolTip="開啟" UseSubmitBehavior="False" />
            </div>
            <!-- 標頭 END -->

            <!-- 內容 START -->
            <div class="popupWindowContent">
                <asp:Panel ID="multioption_pl" runat="server">
                    <asp:HiddenField ID="col_idn_hf" runat="server" />
                    <asp:HiddenField ID="row_idn_hf" runat="server" />
                    <asp:HiddenField ID="new_hf" runat="server" />
                    <asp:Button runat="server" CssClass="btn-large" Text="確認" UseSubmitBehavior="false" OnClick="checkmulti_btn_Click" /><br />
                </asp:Panel>
            </div>
            <!-- 協作者內容 END -->
        </asp:Panel>
        <!-- 協作者 END -->
    </div>
</asp:Content>
