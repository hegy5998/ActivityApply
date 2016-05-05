<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAccountManagerDialog.ascx.cs" Inherits="Web.S01.UCAccountManagerDialog" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/S01/UCAccountManager.ascx" TagPrefix="uc1" TagName="UCAccountManager" %>

<asp:ModalPopupExtender ID="popupWindow_mpe" runat="server" 
    PopupControlID="popupWindow_pl" 
    TargetControlID="popupWindow_cancel_btn" 
    BackgroundCssClass="popupWindowOverlay"></asp:ModalPopupExtender>
<asp:Panel ID="popupWindow_pl" style="display:none;overflow:visible;width:800px;height:500px" class="popupWindow" runat="server">
    <div class="popupWindowHeader">
        <div class="title">
            <asp:Label ID="popupWindow_tilte_lbl" runat="server" Text=""></asp:Label>
        </div>
        <asp:Button ID="popupWindow_cancel_btn" CssClass="close" runat="server" Text="關閉" ToolTip="關閉" UseSubmitBehavior="False" OnClick="popupWindow_cancel_btn_Click" />
    </div>
    <div class="popupWindowContent">
        <asp:UpdatePanel runat="server" ID="popupWindow_content_upl" style="display: inline-block; min-width:100%">
            <ContentTemplate>
                <uc1:UCAccountManager runat="server" id="ucAccountManager" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Panel>