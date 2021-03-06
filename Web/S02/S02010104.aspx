﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Base.Master" AutoEventWireup="true" CodeBehind="S02010104.aspx.cs" Inherits="Web.S02.S02010104" %>

<asp:Content ID="Content1" ContentPlaceHolderID="baseHead_cph" runat="server">
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
        /*不能報名按鈕樣式*/
        .btn_info_dis {
            background-color: gray;
            border-color: gray;
            color: white;
            display: inline-block;
            padding: 6px 12px;
            font-size: 14px;
            border: 1px solid transparent;
            border-radius: 4px;
            font-family: '微軟正黑體',sans-serif;
        }

        form {
            font-family: '微軟正黑體',sans-serif;
        }

        p {
            margin: 0 0 2px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="baseContent_cph" runat="server">
    <div id="back" style="background-color: white; width: 100%;height:100%">
        <div id="title_hi" class="row">
            <div class="col-lg-12">
                <h1 id="title_txt" class="page-header text-info"></h1>
            </div>
        </div>
        <div class="row mt" id="dispaly_div" style="display: none; margin-top: 20px;">
            <div class="col-lg-3">
                <div class="project-wrapper">
                    <div class="project">
                        <div class="photo-wrapper" style="background-image: url(../Scripts/Lib/assets/img/zoom.png);">
                            <div class="photo">
                                <a data-toggle="modal" href="#myModal">
                                    <img id="act_image" class="img-responsive" src="#" style="min-width: 100%; min-height: 100%;" />
                                </a>
                            </div>
                            <div class="overlay" style="height: 20px;"></div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default" style="margin-top: 20px">
                    <div class="panel-heading">短網址</div>
                    <div class="panel-body" style="padding: 20px;">
                        <a id="short_link" href="#" target="_blank" style="font-size: large; padding: 10px;"></a>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">QR Code</div>
                    <div class="panel-body" style="text-align: center;">
                        <img id="QRcode" class="imp-responsive" src="#" alt="" />
                    </div>
                </div>

            </div>
            <!-- 活動頁面右半邊 -->
            <div class="col-lg-9">
                <!-- 活動資訊 -->
                <div class="panel panel-default">
                    <div class="panel-heading">活動詳情</div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div id="add_activity_desc"></div>
                        <div id="desc_div" style="width: auto; height: auto; overflow-x: auto; overflow-y: auto; background-color: white; border-style: ridge; display: none;">
                            <asp:Label ID="Act_desc_lbl" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                </div>
                <div id="add_Session_div" style="margin-bottom: 75px;">
                    <!-- 加入場次地方 -->
                </div>
            </div>
        </div>
    </div>
    <!-- Modal -->
    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade text-center">
        <div class="modal-dialog" style="display: inline-block; width: auto;">
            <div class="modal-content">
                <div class="modal-body">
                    <img id="act_image_modal" class="img-responsive" src="#" />
                </div>
            </div>
        </div>
    </div>
    <!-- modal -->

    <!-- 個資聲明_Modal -->
    <div aria-hidden="true" aria-labelledby="myModalLabel" data-backdrop="static" role="dialog" tabindex="-1" id="person_data_Modal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">個資聲明</h4>
                </div>
                <div id="statement_div" class="modal-body" style="width: auto; height: 300px; overflow-x: auto; overflow-y: auto; background-color: white;">
                    <asp:Label ID="person_data" runat="server"></asp:Label>
                </div>
                <div class="modal-footer">
                    <%--                    <div class="row" style="float:left;margin-top: 5px;">
                        <input type="checkbox" id="agree_statement"/>
                        <label>我已閱讀並同意。</label>
                    </div>
                    --%>
                    <a class="btn btn-default" onclick="personal_cnacle()">不同意</a>
                    <a href="#" id="personal_ok_btn" class="btn btn-info" style="">同意</a>
                </div>
            </div>
        </div>
    </div>
    <!-- modal -->

    <script>
        $(document).ready(function () {
            //產生活動資訊
            getActivityList();
            //等資活動訊載入完才一次顯示資料
            $("#dispaly_div").css({ 'display': '' });
            //產生場次
            getSessionList();

            
            //判斷活動簡介內容高度超過300px變成卷軸式
            var obheight = 300;//超過容器高度自動捲軸
            var mc = $("#desc_div").height();
            if (mc > obheight) $("#desc_div").height(obheight + 'px');
            if($("#form1").height() < ($("#dispaly_div").height() + $("#title_hi").height()))
                $("#back").height(($("#dispaly_div").height() + $("#title_hi").height()) + 'px');
            else
                $("#back").height($("#form1").height() + 'px');
        })

        // #region 產生活動資訊
        function getActivityList() {
            $.ajax({
                type: 'post',
                traditional: true,
                //傳送資料到後台為getActivityList的function
                url: 'S02010104.aspx/getActivityList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
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
                url: 'S02010104.aspx/getSessionList',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
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
            $("#title_txt").text(ActivityInfo[0].Act_title);
            //設定活動標題
            if (ActivityInfo[0].Act_title != "") {
                $("#add_activity_desc").append('<label class="control-label">活動名稱</label>' +
                        '<br />' +
                        '<label>' + ActivityInfo[0].Act_title + '</label>');
            }
            //設定主辦單位
            if (ActivityInfo[0].Act_unit != "") {
                $("#add_activity_desc").append('<hr />');
                $("#add_activity_desc").append('<label class="control-label">主辦單位</label>' +
                        '<br />' +
                        '<label>' + ActivityInfo[0].Act_unit + '</label><br />');
            }
            //設定聯絡人
            if (ActivityInfo[0].Act_contact_name != "" || ActivityInfo[0].Act_contact_phone != "") {
                $("#add_activity_desc").append('<hr />');
                $("#add_activity_desc").append('<label class="control-label">聯絡資訊</label>' +
                        '<br />');
                if (ActivityInfo[0].Act_contact_name != "")
                    $("#add_activity_desc").append('<label>聯絡人:' + ActivityInfo[0].Act_contact_name + '</label><br />');
            }
            //設定聯絡人電話
            if (ActivityInfo[0].Act_contact_phone != "") {
                $("#add_activity_desc").append('<label>聯絡電話:' + ActivityInfo[0].Act_contact_phone + '</label><br />');
            }
            //設定相關連結
            if (ActivityInfo[0].Act_relate_link != "") {
                $("#add_activity_desc").append('<hr />');
                $("#add_activity_desc").append('<label class="control-label">相關連結</label>' +
                        '<br />' +
                        '<a id="relate_link" href="' + ActivityInfo[0].Act_relate_link + '" target="_blank">活動資訊</a>');
            }
            //設定相關連結
            if (ActivityInfo[0].Act_relate_file != "") {
                $("#add_activity_desc").append('<hr />');
                $("#add_activity_desc").append('<label class="control-label">附加檔案</label>' +
                        '<br />' +
                        '<a id="relate_File" href="' + ActivityInfo[0].Act_relate_file + '" target="_blank">下載</a>');
            }
            //設定活動簡介
            if (ActivityInfo[0].Act_desc != "") {
                $("#desc_div").css({ "display": "" });
                $("#add_activity_desc").append('<hr />');
                $("#add_activity_desc").append('<label class="control-label">活動簡介</label>');
            }

            //設定短網址連結
            $("#short_link").attr("href", ActivityInfo[0].Act_short_link);
            $("#short_link").html(ActivityInfo[0].Act_short_link);
            //設定QRcode圖片
            $("#QRcode").attr("src", ActivityInfo[0].Act_short_link + ".qr");


            //設定相關連結，如果沒有則不顯示
            if (ActivityInfo[0].Act_relate_link == "") {
                $("#relate_link").remove();
            }
            //設定附加檔案
            if (ActivityInfo[0].Act_relate_file == "") {
                $("#relate_File").remove();
            }
            //設定活動圖片
            if (ActivityInfo[0].Act_image != "") {
                $("#act_image").attr("src", ActivityInfo[0].Act_image);
                $("#act_image_modal").attr("src", ActivityInfo[0].Act_image);
            }


        }
        //#endregion

        //#region 加入場次
        function addSession(SessionInfo) {
            //轉換場次的JSON字串成JSON物件
            var SessionInfo = JSON.parse(SessionInfo);
            //產生場次
            for (var count = 0 ; count < SessionInfo.length ; count++) {
                var apply_num;
                var as_remark_vis = "";
                var as_relate_link_vis = "";
                if ((SessionInfo[count].as_num_limit - SessionInfo[count].apply_num) < 0)
                    apply_num = 0;
                else
                    apply_num = SessionInfo[count].as_num_limit - SessionInfo[count].apply_num;
                if (SessionInfo[count].as_remark == "")
                    as_remark_vis = "display:none;";
                if (SessionInfo[count].as_relate_link == "")
                    as_relate_link_vis = "display:none;";
                var url = new String('S02010105.aspx?sys_id=S02&sys_pid=S02010105&act_idn=' + SessionInfo[count].as_act + '&as_idn=' + SessionInfo[count].as_idn);
                $("#add_Session_div").append('<div class="panel panel-default">\
                                                <div class="panel-heading">第 ' + (count + 1) + ' 場  /  共 ' + SessionInfo.length + ' 場</div>\
                                                    <div class="panel-body">\
                                                        <h3 style="color:#069;">'+ SessionInfo[count].as_title + '</h3>\
                                                        <div class="tooltip-demo"><p>活動地點：' + SessionInfo[count].as_position + '</p>\
                                                            <p>活動日期：' + dateReviver(SessionInfo[count].as_date_start) + ' ~ ' + dateReviver(SessionInfo[count].as_date_end) + '</p>\
                                                            <p>報名日期：' + dateReviver(SessionInfo[count].as_apply_start) + ' ~ ' + dateReviver(SessionInfo[count].as_apply_end) + '</p>\
                                                            <p>報名/限制人數：' + SessionInfo[count].apply_num + '/' + SessionInfo[count].as_num_limit + '人</p>\
                                                            <p style="' + as_remark_vis + ' margin:0px;">備註：' + SessionInfo[count].as_remark + '</p>\
                                                            <p style="' + as_relate_link_vis + '">相關連結：<a  href="' + SessionInfo[count].as_relate_link + '" target="_blank">場次資訊</a></p>\
                                                            <a id="apply_btn_' + count + '" onclick=openss("' + url + '") class="btn btn-info" data-toggle="tooltip" data-placement="right" style="color: white;">我要報名</a>\
                                                        </div>');

                if (apply_num <= 0) {
                    $("#apply_btn_" + count).html("名額已滿");
                    $("#apply_btn_" + count).removeClass("btn btn-info");
                    $("#apply_btn_" + count).addClass("btn_info_dis");
                }
                //將時間字串轉成DateTime格式
                var apply_end = new Date(dateReviver(SessionInfo[count].as_apply_end));
                var date_end = new Date(dateReviver(SessionInfo[count].as_date_end));
                var apply_start = new Date(dateReviver(SessionInfo[count].as_apply_start));
                //取得目前時間
                var NowDate = new Date();
                //判斷報名結束時間是否結束
                if (apply_end < NowDate) {
                    $("#apply_btn_" + count).html("報名日期截止");
                    $("#apply_btn_" + count).removeClass("btn btn-info");
                    $("#apply_btn_" + count).addClass("btn_info_dis");
                }
                //判斷活動是否結束
                if (date_end < NowDate) {
                    $("#apply_btn_" + count).html("活動已結束");
                    $("#apply_btn_" + count).removeClass("btn btn-info");
                    $("#apply_btn_" + count).addClass("btn_info_dis");
                }
                //判斷是否開始報名
                if (apply_start > NowDate) {
                    $("#apply_btn_" + count).html("尚未開放報名");
                    $("#apply_btn_" + count).removeClass("btn btn-info");
                    $("#apply_btn_" + count).addClass("btn_info_dis");
                }
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
        }

        function openss(url) {
            $("#personal_ok_btn").attr('href', url);
            $("#person_data_Modal").modal('show');
        }

        function personal_cnacle() {
            $("#person_data_Modal").modal('hide');
        }

    </script>
</asp:Content>

