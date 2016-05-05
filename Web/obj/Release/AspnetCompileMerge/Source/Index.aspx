<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Base.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Web.Index" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/UCRoleChg.ascx" TagPrefix="ucsm" TagName="UCRoleChg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="baseHead_cph" runat="server">
    <link href="Css/Index.css" rel="stylesheet" />
    <style type="text/css">
        .fvRow > th
        {
            width: 95px;
        }
        .popupWindow, .popupWindowContent
        {
            height: auto;
        }
        #systemOtherMenu 
        {
            padding-top: 1px;
        }
        #systemOtherMenu .item 
        {
            height: 21px;
            line-height:21px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            // Config jQuery Dialog
            $.extend($.ui.dialog.prototype.options, {
                modal: true,
                resizable: false,
                draggable: false,
                closeText: "關閉",
                autoOpen: false,
                width: 'auto'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="baseContent_cph" runat="server">
    <asp:ToolkitScriptManager ID="tsm" runat="server" AsyncPostBackTimeout="300"></asp:ToolkitScriptManager>
    <div id="systemMenu">
        <div id="topBox">
            <div id="systemUserInfo" style="position:absolute;top:-27px; left:79px;font-family:'微軟正黑體',sans-serif;font-size:14px;">
                帳號：<span class="highlight" style="margin-right:20px"><%=Util.CommonHelper.GetLoginUser().Act_id %></span>
                名稱：<span class="highlight" style="margin-right:20px"><%=Util.CommonHelper.GetLoginUser().Act_name %></span>
                身份：<span class="highlight"><ucsm:UCRoleChg runat="server" id="UCRoleChg" /></span></div>
            <div id="systemAnnounce">
                <div class="title">最新消息<span style="float:right;padding-right:5px;<%=(Util.CommonHelper.GetSysConfig().ONLINE_USER_COUNTER_ENABLE == "Y") ? "" : "display:none" %>">線上人數：<%= Util.OnlineUserCounter.GetOnlineUserCount() %> 人</span></div>
                <div class="content">
                    <%=news_html %>
                </div>
            </div>
            <div id="systemOtherMenu">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <a class="item">使用手冊下載</a>
                        <a class="item">系統常見問題</a>
                        <a class="item">服務信箱</a>
                        <a class="item">個人密碼修改</a>
                        <a class="item" href="<%=ResolveUrl("~/Logout.aspx") %>">登出</a>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="systemList">
            <%=systemList_html %>
        </div>
    </div>
    <div id="footer">
        <div>版權所有：行政院農業委員會農糧署　本網站建議使用Firefox, Chrome, IE 8以上之瀏覽器，最佳瀏覽解析度為1024x768以上</div>
        <div>中興辦公區:54044南投縣南投市光華路8號&nbsp;&nbsp;TEL:049-2332380&nbsp;&nbsp;&nbsp;&nbsp;臺北辦公區:10050臺北市杭州南路1段15號&nbsp;&nbsp;TEL:02-23937231 或洽各辦事處[聯絡電話查詢]</div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="300">
        <ProgressTemplate>
            <div class="loadingMsgWrapper">
                <span class="loadingMsg">載入中...</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="0">
        <ProgressTemplate>
            <div class="loadingOverlay">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
 