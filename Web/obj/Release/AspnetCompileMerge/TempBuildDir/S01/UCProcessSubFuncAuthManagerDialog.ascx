<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCProcessSubFuncAuthManagerDialog.ascx.cs" Inherits="Web.S01.UCProcessSubFuncAuthManagerDialog" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/S01/UCProcessSubFuncAuthManager.ascx" TagPrefix="uc1" TagName="UCProcessSubFuncAuthManager" %>

<asp:ModalPopupExtender ID="popupWindow_mpe" runat="server" 
    PopupControlID="popupWindow_pl" 
    TargetControlID="popupWindow_cancel_btn" 
    BackgroundCssClass="popupWindowOverlay"></asp:ModalPopupExtender>
<asp:Panel ID="popupWindow_pl" style="display:none;overflow:visible;" class="popupWindow" runat="server">
    <div class="popupWindowHeader">
        <div class="title">
            <asp:Label ID="popupWindow_tilte_lbl" runat="server" Text=""></asp:Label>
        </div>
        <asp:Button ID="popupWindow_cancel_btn" CssClass="close" runat="server" Text="關閉" ToolTip="關閉" UseSubmitBehavior="False" />
    </div>
    <div class="popupWindowContent">
        <asp:UpdatePanel runat="server" ID="popupWindow_content_upl" style="display: inline-block; min-width:100%">
            <ContentTemplate>
                <uc1:UCProcessSubFuncAuthManager runat="server" id="ucProcessSubFuncAuthManager" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Panel>