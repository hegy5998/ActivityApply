<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeBehind="activity.aspx.cs" Inherits="Register.activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead" runat="server">
    <style type="text/css">
        /* 欄位標題 */
        .control-label {
            font-weight: bold;
        }
        /*場次標題*/
        .session-control-label {
            font-weight: bold;
            font-size: xx-large;
        }
        /*場次內容*/
        .session-control-label-context {
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sideCon" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainCon" runat="server">
    <section id="main-content">
        <section class="wrapper site-min-height">
            <div class="row mt">
                <div class="row mt">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 desc">
                        <div class="project-wrapper" style="margin-bottom: 16px;">
                            <div class="project">
                                <div class="photo-wrapper">
                                    <div class="photo">
                                        <a data-toggle="modal" href="#myModal">
                                            <img class="img-responsive" src="assets/img/fcu.jpg" alt="" /></a>
                                    </div>
                                    <div class="overlay"></div>
                                </div>
                            </div>
                        </div>
                        <div class="showback">
                            <h3>短網址</h3>
                            <h5>http://goo.com</h5>
                        </div>
                        <div class="photo">
                            <a class="thumbnail">
                                <img class="imp-responsive" src="assets/img/qrcodetest.png" alt="" /></a>
                        </div>
                    </div>
                    <!-- 活動頁面右半邊 -->
                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-12 dexc">
                        <!-- 活動資訊 -->
                        <div class="showback">
                            <label class="control-label">主辦單位</label>
                            <br />
                            <label id="unit"></label>
                            <hr />
                            <label class="control-label">聯絡資訊</label>
                            <br />
                            <label id="contact_name"></label>
                            <br />
                            <label id="contact_phone"></label>
                            <hr />
                            <label class="control-label">活動簡介</label>
                            <br />
                            <!-- 設定活動簡介大小，超出變成卷軸 -->
                            <div style="width:auto;height:400px;overflow-x:auto;overflow-y:auto;background-color:white;">
                                <asp:Label ID="Act_desc_lbl" runat="server" Text="Label" ></asp:Label>
                            </div>

                            <hr />
                            <label class="control-label">附加檔案</label>
                            <h5>我是附加檔案</h5>
                            <hr />
                            <label class="control-label">相關連結</label>
                            <br />
                            <a id="relate_link" href="#" target="_blank">點我</a>
                            <hr />
                        </div>
                        <div class="row" id="add_Session_div">
                            <!-- 加入場次地方 -->
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </section>

    <script>
        $(document).ready(function () {
            //產生活動資訊
            getActivityList();
            //產生場次
            getSessionList();
        })

        //#region 設定麵包削尋覽列
        function setSessionBread() {
            //將滅包削內容清空
            $("#add_breach").children().remove();
            //添加首頁
            $("#add_breach").append('<li><a href="index.aspx">首頁</a></li>');
            var act_class = $.url().param("act_class");
            //判斷目前目錄並添加
            var act_class_title;
            for (var count = 0 ; count < $("#add_sub > li > a").length ; count++) {
                var act_num = $("#add_sub > li > a")[count].href.split("act_class=")[$("#add_sub > li > a")[count].href.split("act_class=").length - 1]
                if (act_num == act_class) {
                    act_class_title = $("#add_sub > li > a")[count].innerHTML;
                }
            }
            $("#add_breach").append('<li><a href="activity_List.aspx?act_class=' + act_class + '">' + act_class_title + '</a></li>');
            $("#add_breach").append('<li><a href="activity.aspx?act_class=' + $.url().param("act_class") + '&act_idn=' + $.url().param("act_idn") + '">' + $.url().param("act_title") + '</a></li>');
        }
        //#endregion

        // #region 產生活動資訊
        function getActivityList() {
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為getActivityList的function
                url: 'activity.aspx/getActivityList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //成功時
                success: function (result) {
                    // 設定活動資訊
                    setActivity(result.d);
                },
                //失敗時
                error: function () {
                    alert("失敗!!!!");
                    return false;
                }
            });
        }
        // #endregion

        // #region 產生場次
        function getSessionList() {
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為getSessionList的function
                url: 'activity.aspx/getSessionList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //成功時
                success: function (result) {
                    // 加入場次
                    addSession(result.d);
                },
                //失敗時
                error: function () {
                    return false;
                }
            });
        }
        // #endregion

        //#region 設定活動資訊內容
        function setActivity(ActivityInfo) {
            //轉換活動資訊的JSON字串成JSON物件
            var ActivityInfo = JSON.parse(ActivityInfo);
            var act_title = ActivityInfo[0].Act_title;

            //設定主辦單位
            $("#unit").text(ActivityInfo[0].Act_unit);
            //設定聯絡人
            $("#contact_name").text("聯絡人:" + ActivityInfo[0].Act_contact_name);
            //設定聯絡人電話
            $("#contact_phone").text("聯絡電話:" + ActivityInfo[0].Act_contact_phone);
            //$("#desc").append(decodeURI(ActivityInfo[0].Act_desc));
            //設定相關連結，如果沒有則不顯示
            if (ActivityInfo[0].Act_relate_link != null)
                $("#relate_link").attr("href", ActivityInfo[0].Act_relate_link);
            else
                $("#relate_link").remove();
        }
        //#endregion

        //#region 加入場次
        function addSession(SessionInfo) {
            //轉換場次的JSON字串成JSON物件
            var SessionInfo = JSON.parse(SessionInfo);
            //產生場次
            for (var count = 0 ; count < SessionInfo.length ; count++) {
                $("#add_Session_div").append('<div class="showback">\
                                             <label class="session-control-label" id="as_title_">'+ SessionInfo[count].As_title + '</label>\
                                             <br />\
                                             <label class="session-control-label-context" id="as_position_">活動地點：' + SessionInfo[count].As_position + '</label>\
                                             <br />\
                                             <label class="session-control-label-context" id="as_data_">活動日期：' + dateReviver(SessionInfo[count].As_date_start) + ' ~ ' + dateReviver(SessionInfo[count].As_date_end) + '</label>\
                                             <br />\
                                             <label class="session-control-label-context" id="as_apply_">報名日期：' + dateReviver(SessionInfo[count].As_apply_start) + ' ~ ' + dateReviver(SessionInfo[count].As_apply_end) + '</label>\
                                             <br />\
                                             <label class="session-control-label-context" id="as_numlimit_">剩餘/限制人數：10/' + SessionInfo[count].As_num_limit + '人</label>\
                                             <br />\
                                             <a href="Sign_Up.aspx?act_idn=' + SessionInfo[count].As_act + '&as_idn=' + SessionInfo[count].As_idn + '&act_class=' + $.url().param("act_class") + '&act_title=' + $.url().param("act_title") + '" class="btn btn-theme btn-lg" role="button">我要報名</a>\
                                         </div>');
            }
        }
        //#endregion

        //後臺轉換DateTime格式時會把他轉成字串，使用JSON.parse會把它當成子串解析，需要在做轉換與切割成我們要的格式
        function dateReviver(datavalue) {
            if (datavalue != null) {
                var datavalue = datavalue.split("T");
                return datavalue[0] + " " + datavalue[1].substring(0, 5);
            }
            else
                return "";
        };

    </script>
    <!-- Modal -->
    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade text-center">
        <div class="modal-dialog" style="display: inline-block; width: auto;">
            <div class="modal-content">
                <div class="modal-body">
                    <img class="img-responsive" src="assets/img/fcu.jpg" alt="" />
                </div>
            </div>
        </div>
    </div>
    <!-- modal -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageUnitEnd" runat="server">
</asp:Content>

