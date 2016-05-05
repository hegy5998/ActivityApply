<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Base.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.Login" EnableSessionState="True" %>
<%@ Import Namespace="Util" %>
<asp:Content ID="Content1" ContentPlaceHolderID="baseHead_cph" runat="server">
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Lib/jsencrypt.min.js") %>"></script>
    <link href="Css/Login.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {

            $("#resetBtn").click(function () {
                $("#<%=act_id_txt.ClientID%>").val("");
                $("#act_pwd_txt").val("");
                $("#<%=confirm_txt.ClientID%>").val("");
                $("#<%=act_id_txt.ClientID%>").focus();
            });

            var captchaNum = 0;
            $("#captchaRefresh").click(function () {
                $(".captcha").first().remove();
                var $newCaptcha = $('<img alt="" class="captcha" src="<%=ResolveUrl("~/CommonPages/Captcha.aspx") %>" />');
                $newCaptcha.attr("src", $newCaptcha.attr("src") + "?n=" + captchaNum);
                captchaNum++;
                $("#captchaBox").append($newCaptcha);
            });

            $("#<%=act_id_txt.ClientID%>").focus();
            $("#<%=broseWebTime_hf.ClientID%>").val("<%=DbTime %>");
        });
        function encryptPwd() {
            var crypt = new JSEncrypt();
            crypt.setKey("<%=rsaPublicKey %>");
            $("#<%=act_pwd_hf.ClientID%>").val(crypt.encrypt($("#act_pwd_txt").val()));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="baseContent_cph" runat="server">
    <div id="login">
        <asp:Panel ID="dbFail_pl" runat="server">
            <div style="text-align: center; position: absolute; top: 222px; font-size: 16px; color: #f00; width:100%; font-weight: bold; line-height:25px">
                系統資料庫維護中，請稍候再使用。
            </div>
        </asp:Panel>
        <asp:Panel ID="normal_pl" runat="server">
            <div class="loginErrorMsg"><asp:Label ID="loginErrorMsg_lbl" runat="server" Text="[帳號]或[密碼]錯誤!" Visible="False"></asp:Label></div>
            <table class="loginContent">
                <tr>
                    <td class="fieldName">帳號：</td>
                    <td><asp:TextBox ID="act_id_txt" CssClass="inputText" runat="server" Text="S001"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="fieldName">密碼：</td>
                    <td>
                        <asp:HiddenField ID="act_pwd_hf" runat="server" />
                        <input id="act_pwd_txt" type="password" class="inputText" /></td>
                </tr>
                <tr>
                    <td class="fieldName">驗證碼：</td>
                    <td>
                        <asp:TextBox ID="confirm_txt" CssClass="inputText" runat="server" Width="50px" MaxLength="4" autocomplete="off"></asp:TextBox>
                        <span id="captchaBox">
                            <img alt="" class="captcha" src="<%=ResolveUrl("~/CommonPages/Captcha.aspx") %>" />
                        </span>
                        <span class="refeshBtn" title="更新驗證碼圖片" id="captchaRefresh" style=""></span>
                        <asp:HiddenField ID="broseWebTime_hf" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td class="fieldName"></td>
                    <td style="padding-top: 10px">
                        <asp:Button ID="signIn_btn" runat="server" Text="登入" CssClass="btn btn-small btn-success" OnClick="signIn_btn_Click" EnableViewState="False" OnClientClick="encryptPwd();" />
                        <input type="button" id="resetBtn" value="重新輸入" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div id="footer">
            <div>版權所有：行政院農業委員會農糧署　本網站建議使用Firefox, Chrome, IE 8以上之瀏覽器，最佳瀏覽解析度為1024x768以上</div>
            <div>中興辦公區:54044 南投縣南投市光華路8號&nbsp;&nbsp;TEL:049-2332380&nbsp;&nbsp;&nbsp;&nbsp;臺北辦公區:10050 臺北市杭州南路1段15號&nbsp;&nbsp;TEL:02-23937231</div>
            <div>本系統平台諮詢窗口：04-24517250轉2801 張小姐 <span class="highlight">chungyy@fcuoa.fcu.edu.tw</span> 或洽各辦事處 <a href="OfficeTel.html" target="_blank">[聯絡電話查詢]</a></div>
        </div>
    </div>
</asp:Content>
