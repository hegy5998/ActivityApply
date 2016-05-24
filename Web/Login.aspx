<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Base.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.Login" EnableSessionState="True" %>

<%@ Import Namespace="Util" %>
<asp:Content ID="Content1" ContentPlaceHolderID="baseHead_cph" runat="server">
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/Lib/jsencrypt.min.js") %>"></script>
    <link href="Css/Login.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {

<%--            $("#resetBtn").click(function () {
                $("#<%=act_id_txt.ClientID%>").val("");
                $("#act_pwd_txt").val("");
                $("#<%=confirm_txt.ClientID%>").val("");
                $("#<%=act_id_txt.ClientID%>").focus();
            });--%>

            var captchaNum = 0;
            $("#captchaRefresh").click(function () {
                $(".captcha").first().remove();
                var $newCaptcha = $('<img alt="" class="captcha" src="<%=ResolveUrl("~/CommonPages/Captcha.aspx") %>" />');
                $newCaptcha.attr("src", $newCaptcha.attr("src") + "?n=" + captchaNum);
                captchaNum++;
                $("#captchaBox").append($newCaptcha);
            });

            <%--$("#<%=act_id_txt.ClientID%>").focus();--%>
            $("#<%=broseWebTime_hf.ClientID%>").val("<%=DbTime %>");
        });
        function encryptPwd() {
            var crypt = new JSEncrypt();
            crypt.setKey("<%=rsaPublicKey %>");
            $("#<%=act_pwd_hf.ClientID%>").val(crypt.encrypt($("#act_pwd_txt").val()));
            $("#<%=act_id_hf.ClientID%>").val($("#act_id_txt").val());
            $("#<%=confirm_txt_pf.ClientID%>").val($("#confirm_txt").val());
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="baseContent_cph" runat="server">
    <div style="background-image: url(Css/Images/LoginBg.jpg);width:100%;height:100%">
        <div id="login">
            <div style="background-color: white; width: 413px; height: 363px; margin-left: 130px; margin-top: 50px; border-radius: 13px;">
                <div class="modal-header" style="border-radius: 13px 13px 0px 0px; text-align: center;">
                    <h4 class="modal-title" style="font-size: x-large;">報名系統登入</h4>
                </div>
                <asp:Panel ID="dbFail_pl" runat="server">
                    <div style="text-align: center; position: absolute; top: 222px; font-size: 16px; color: #f00; width: 413px; font-weight: bold; line-height: 25px">
                        系統資料庫維護中，請稍候再使用。
           
                    </div>
                </asp:Panel>
                <asp:Panel ID="normal_pl" runat="server">
                    <div class="loginErrorMsg">
                        <asp:Label ID="loginErrorMsg_lbl" runat="server" Text="[帳號]或[密碼]錯誤!" Visible="false"></asp:Label>
                    </div>
                    <table class="fv loginContent">
                        <tr>
                            <td>
                                <asp:HiddenField ID="act_id_hf" runat="server" />
                                <input type="text" id="act_id_txt" class="form-control" placeholder="帳號"/>
                            </td>
                        </tr>
                        <tr style="height: 10px;"></tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="act_pwd_hf" runat="server" />
                                <input id="act_pwd_txt" type="password" class="form-control" style="display: inherit; color: #ABABAB;" placeholder="密碼" /></td>
                        </tr>
                        <tr style="height: 10px;"></tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="confirm_txt_pf" runat="server" />
                                <input type="text" id="confirm_txt" class="form-control" maxlength="4" autocomplete="off" style="display: inherit; width: 50%;" placeholder="驗證碼" />
                                <span id="captchaBox">
                                    <img alt="" class="captcha" src="<%=ResolveUrl("~/CommonPages/Captcha.aspx") %>" />
                                </span>
                                <span class="refeshBtn" title="更新驗證碼圖片" id="captchaRefresh" style=""></span>
                                <asp:HiddenField ID="broseWebTime_hf" runat="server" />
                            </td>
                        </tr>

                    </table>
                    <div style="margin: 222px 24px 0px 24px;">
                        <asp:Button ID="signIn_btn" runat="server" Text="登入" CssClass="btn btn-lg" OnClick="signIn_btn_Click" EnableViewState="False" OnClientClick="encryptPwd();" Style="width: 100%; height: 44px; background-color: #768094; border-color: #768094; color: white;" />
                        <%--<input type="button" id="resetBtn" class="btn btn-small btn-default" value="重新輸入" />--%>
                    </div>
                </asp:Panel>
            </div>
            <div id="footer">
                <div>版權所有：行政院農業委員會農糧署　本網站建議使用Firefox, Chrome, IE 8以上之瀏覽器，最佳瀏覽解析度為1024x768以上</div>
                <div>中興辦公區:54044 南投縣南投市光華路8號&nbsp;&nbsp;TEL:049-2332380&nbsp;&nbsp;&nbsp;&nbsp;臺北辦公區:10050 臺北市杭州南路1段15號&nbsp;&nbsp;TEL:02-23937231</div>
                <div>本系統平台諮詢窗口：04-24517250轉2801 張小姐 <span class="highlight">chungyy@fcuoa.fcu.edu.tw</span> 或洽各辦事處 <a href="OfficeTel.html" target="_blank">[聯絡電話查詢]</a></div>
            </div>
        </div>
    </div>
</asp:Content>
