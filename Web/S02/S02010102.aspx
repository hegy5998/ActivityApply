<%@ Page Title="報名資料" Language="C#" MasterPageFile="~/MasterPages/Base.Master" AutoEventWireup="true" CodeBehind="S02010102.aspx.cs" Inherits="Web.S02.S02010102" %>
<%@ Register Src="~/UserControls/UCSystemModule.ascx" TagPrefix="uc1" TagName="UCSystemModule" %>
<%@ Import Namespace="Util" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="baseHead_cph" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="baseContent_cph" runat="server">
    <h2>逢甲大學活動報名系統</h2>

    <asp:Button ID="download_btn" runat="server" class="btn-link" Text="下載" ToolTip="下載" UseSubmitBehavior="False" OnClick="download_btn_Click"/>
    <br /><br />

    <div class="row">
        <asp:GridView ID="main_gv" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowCreated="main_gv_RowCreated" OnRowDeleting="main_gv_RowDeleting" OnRowCommand="main_gv_RowCommand" OnRowEditing="main_gv_RowEditing" OnRowCancelingEdit="main_gv_RowCancelingEdit" OnRowDataBound="main_gv_RowDataBound" OnRowUpdating="main_gv_RowUpdating" ViewStateMode="Enabled" OnSorting="main_gv_Sorting">
            <Columns>
                <%--操作--%>
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
                        <asp:HiddenField ID="old_aa_idn_hf" runat="server" Value='<%# Eval("aa_idn") %>' />
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
                            <asp:Button ID="add_btn" runat="server" CommandName="Add" CssClass="movedown" Text="新增" ToolTip="新增" UseSubmitBehavior="False" />
                        </span>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <span style="position: relative;">
                            <i class="fa fa-pencil btn btn-primary btn-xs" aria-hidden="true"></i>
                            <asp:Button ID="edit_btn" runat="server" CommandName="Edit" CssClass="movedown" Text="編輯" ToolTip="編輯" UseSubmitBehavior="False" />
                        </span>
                        &nbsp;
                    <span style="position: relative;">
                        <i class="fa fa-trash-o btn btn-danger btn-xs" aria-hidden="true"></i>
                        <asp:Button ID="delete_btn" runat="server" CommandName="Delete" CssClass="movedown" OnClientClick="if (!confirm(&quot;確定要刪除嗎?&quot;)) return false" Text="刪除" ToolTip="刪除" UseSubmitBehavior="False" />
                    </span>
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
                            <asp:Label ID="control_tilte_lbl" runat="server" Text="多選題"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <!-- 關閉按鈕 -->
                <asp:Button ID="control_cancel_btn" CssClass="close" runat="server" Text="關閉" ToolTip="關閉" OnClick="control_cancel_btn_Click" UseSubmitBehavior="False" />
                <asp:Button ID="control_OK_btn" CssClass="open" runat="server" Text="開啟" ToolTip="開啟" UseSubmitBehavior="False" />
            </div>
            <!-- 標頭 END -->

            <!-- 內容 START -->
            <div class="popupWindowContent">
                <asp:Panel ID="multioption_pl" runat="server">
                    <asp:HiddenField ID="col_idn_hf" runat="server" />
                    <asp:HiddenField ID="row_idn_hf" runat="server" />
                    <asp:HiddenField ID="new_hf" runat="server" />                    
                </asp:Panel>
                <asp:Button runat="server" CssClass="btn btn-default" Text="確認" UseSubmitBehavior="false" OnClick="checkmulti_btn_Click" />
            </div>
            <!-- 多選內容 END -->
        </asp:Panel>
        <!-- 多選 END -->

        <%--新增密碼跳出視窗--%>
        <asp:ModalPopupExtender ID="emailpassword_mpe" runat="server" PopupControlID="emailpassword_pl" TargetControlID="email_OK_btn" BackgroundCssClass="popupWindowOverlay" OkControlID="email_cancel_btn"></asp:ModalPopupExtender>

        <!-- 新增密碼 START -->
        <asp:Panel ID="emailpassword_pl" class="popupWindow" runat="server" Visible="false">
            <!-- 新增密碼標頭 START -->
            <div class="popupWindowHeader">
                <!-- 標題 -->
                <div class="title">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="Label1" runat="server" Text="密碼設定"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <!-- 關閉按鈕 -->
                <asp:Button ID="email_cancel_btn" CssClass="close" runat="server" Text="關閉" ToolTip="關閉" OnClick="control_cancel_btn_Click" UseSubmitBehavior="False" />
                <asp:Button ID="email_OK_btn" CssClass="open" runat="server" Text="開啟" ToolTip="開啟" UseSubmitBehavior="False" />
            </div>
            <!-- 標頭 END -->

            <!-- 內容 START -->
            <div class="popupWindowContent">
                <asp:Panel ID="Panel2" runat="server">
                    <asp:HiddenField ID="email_hf" runat="server" />
                    <table id="pwd_tb" class="table">
                        <tr>
                            <th>
                                <asp:Label ID="password_lbl" runat="server" Text="密碼"></asp:Label>
                            </th>
                            <td>

                                <asp:TextBox TextMode="Password" ID="password_txt" runat="server"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Label ID="passwordcheck_lbl" runat="server" Text="確認密碼"></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox TextMode="Password" ID="passwordcheck_txt" runat="server"></asp:TextBox><br />
                            </td>
                        </tr> 
                        <tr>
                            <th></th>
                            <td style="text-align:left;">
                                <asp:Button ID="password_btn" runat="server" CssClass="btn btn-default" Text="確認" UseSubmitBehavior="false" OnClick="checkpassword_btn_Click"/><br />
                            </td>
                        </tr>
                    </table>
                    
                </asp:Panel>
            </div>
            <!-- 協作者內容 END -->
        </asp:Panel>
        <!-- 協作者 END -->
    </div>
</asp:Content>
