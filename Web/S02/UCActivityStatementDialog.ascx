<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCActivityStatementDialog.ascx.cs" Inherits="Web.S02.UCActivityStatementDialog" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/S02/UCActivityStatement.ascx" TagPrefix="uc1" TagName="UCActivityStatement" %>

<asp:ModalPopupExtender ID="popupWindow_mpe" runat="server" 
    PopupControlID="popupWindow_pl" 
    TargetControlID="popupWindow_cancel_btn" 
    BackgroundCssClass="popupWindowOverlay"></asp:ModalPopupExtender>
<asp:Panel ID="popupWindow_pl" style="display:none;overflow:visible;width:800px;" class="popupWindow" runat="server">
    <div class="popupWindowHeader">
        <div class="title">
            <asp:Label ID="popupWindow_tilte_lbl" runat="server" Text=""></asp:Label>
        </div>
        <asp:Button ID="popupWindow_cancel_btn" CssClass="close" runat="server" Text="關閉" ToolTip="關閉" UseSubmitBehavior="False" OnClick="popupWindow_cancel_btn_Click" />
    </div>
    <div class="popupWindowContent">
        <asp:UpdatePanel runat="server" ID="popupWindow_content_upl" style="display: inline-block; min-width:100%">
            <ContentTemplate>
                <uc1:UCActivityStatement runat="server" id="ucActivityStatement" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Panel>